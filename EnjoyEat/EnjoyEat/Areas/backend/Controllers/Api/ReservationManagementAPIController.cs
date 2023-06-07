using EnjoyEat.Controllers.API;
using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    [Route("api/ReservationManagementApi/[action]")]
    [ApiController]
    public class ReservationManagementAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public ReservationManagementAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> PostReservationManagement(ReservationManagemanetViewModel model) 
        {
            Reservation reservation = new Reservation
            {
                ReserveId = model.ReserveId,
                ReservationDate = model.ReservationDate,
                ReservationTime = model.ReservationTime,
                NumberofAdultGuest = model.NumberofAdultGuest,
                NumberofKidGuest = model.NumberofKidGuest,
            };
            db.Reservations.Add(reservation);
            db.SaveChanges();
 
            ReservationInformation reservationInfo = new ReservationInformation
            {
                ReserveId = reservation.ReserveId,
                ReservationName = model.ReservationName,
                PhoneNumber = model.PhoneNumber,
                Note = model.Note,

            };
            db.ReservationInformations.Add(reservationInfo);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost]

        public async Task<IEnumerable<ReservationManagemanetViewModel>> FilterReservation(ReservationManagemanetViewModel reservemodel) 
        {

            return db.Reservations.Where(x => x.ReserveId == reservemodel.ReserveId ||
            x.ReservationDate.Equals(reservemodel.ReservationDate) ||
            x.ReservationTime.Contains(reservemodel.ReservationTime) ||
            x.NumberofAdultGuest.Contains(reservemodel.NumberofAdultGuest) ||
            x.NumberofKidGuest.Contains(reservemodel.NumberofKidGuest) ||
            x.ReservationInformation.ReservationName.Contains(reservemodel.ReservationName) ||
            x.ReservationInformation.PhoneNumber.Contains(reservemodel.PhoneNumber) ||
            x.ReservationInformation.Note.Contains(reservemodel.Note)
            ).Select(x => new ReservationManagemanetViewModel {
                ReserveId=x.ReserveId, 
                ReservationName=x.ReservationInformation.ReservationName,
                ReservationTime=x.ReservationTime,
                ReservationDate=x.ReservationDate,
                NumberofAdultGuest=x.NumberofAdultGuest,
                NumberofKidGuest=x.NumberofKidGuest,
                PhoneNumber=x.ReservationInformation.PhoneNumber,
                Note=x.ReservationInformation.Note,
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetReservationManagement()
        {
            var reserve = db.Reservations.Include(x => x.ReservationInformation).Select(x => new ReservationManagemanetViewModel
            {
                ReserveId = x.ReserveId,
                ReservationName = x.ReservationInformation.ReservationName,
                PhoneNumber = x.ReservationInformation.PhoneNumber,
                NumberofAdultGuest = x.NumberofAdultGuest,
                NumberofKidGuest = x.NumberofKidGuest,
                ReservationDate = x.ReservationDate,
                ReservationTime = x.ReservationTime,
                Note = x.ReservationInformation.Note,
            }).OrderBy(x => x.ReservationDate).ThenBy(x => x.ReservationTime).ToList();
            return Ok(reserve);
        }

        //編輯訂位資訊
        [HttpPut("{reserveId}")]
        public async Task<IActionResult> EditReservationManagement(int reserveId, [FromBody] ReservationManagemanetViewModel model)
        {

            var source = db.Reservations.Include(x=>x.ReservationInformation).FirstOrDefault(x=>x.ReserveId == reserveId);

            source.NumberofAdultGuest = model.NumberofAdultGuest;
            source.NumberofKidGuest = model.NumberofKidGuest;
            source.ReservationDate = model.ReservationDate;
            source.ReservationTime = model.ReservationTime;
            source.ReservationInformation.Note = model.Note;
            source.ReservationInformation.ReservationName = model.ReservationName;
            source.ReservationInformation.PhoneNumber = model.PhoneNumber;
            db.SaveChanges();
            return Ok("修改成功");
        }

        [HttpDelete("{reserveId}")]

        public async Task<string> DeleteReservationManagement(int reserveId)
        {
            var reserveInfo = db.ReservationInformations.FirstOrDefault(x => x.ReserveId == reserveId);
            db.Remove(reserveInfo);
            db.SaveChanges();
            var reserve = db.Reservations.FirstOrDefault(x => x.ReserveId == reserveId);
            db.Remove(reserve);
            db.SaveChanges();
            return "刪除成功!";
        }

    }
}
