using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Policy;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    [Route("api/memberbackend/[action]")]
    [ApiController]
	[Authorize(Roles = "manager,staff")]
	public class MemberApIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;
        private readonly HashService hash;

        public MemberApIController(db_a989fe_thm101team6Context db, HashService hash, EncryptService encrypt)
        {
            this.db = db;
            this.hash = hash;
        }

        //取得資料
        [HttpGet]
        public async Task<IQueryable<MemberViewModel>> GetMember()
        {
            return db.Members.Where(x=>x.MemberLogin.IsActive==true).Select(x => new MemberViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Email = x.Email,
                Birthday = x.Birthday,
                Gender = x.Gender,
                LevelDiscount = x.LevelDiscount,
                LevelName = x.LevelName,
                MemberId = x.MemberId,
                MemberPoint = x.MemberPoint,
                Phone = x.Phone,
                RegisterDay = x.RegisterDay,
            });

        }

        [HttpPost]
        //新增資料
        public async Task<string> CreateMember(MemberViewModel model)
        {
            Member member = new Member
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday = model.Birthday,
                Address = model.Address,
                Gender = model.Gender,
                Email = model.Email,
                LevelName = "平民",
                Phone = model.Phone,
                RegisterDay = DateTime.Now,
            };
            var user = db.Members.FirstOrDefault(x => x.Email == member.Email);
            if (user != null)
            {
                return "郵件已註冊";
            }
            db.Members.Add(member);
            await db.SaveChangesAsync();

            string salt = Guid.NewGuid().ToString("N");
            MemberLogin login = new MemberLogin
            {
                MemberId=member.MemberId,
                Account = model.Phone,
                Password = hash.GetHash(string.Concat(model.Phone, salt).ToString()),
                Salt = salt,
                Role = "User",
                IsActive=true,
            };
            db.MemberLogins.Add(login);
            await db.SaveChangesAsync();
            return "成功";
        }

        //刪除資料
        [HttpDelete("{memberId}")]
        public async Task<string> DeleteMember(int memberId)
        {
            var login = db.MemberLogins.FirstOrDefault(x => x.MemberId == memberId);
            if (login != null)
            {
                login.IsActive=false;
            }
            db.SaveChanges();
            return "刪除成功!";
        }

        [HttpPut]
        //修改資料
        public async Task<string> EditMember(MemberViewModel model)
        {
            var member = db.Members.FirstOrDefault(x => x.MemberId == model.MemberId);
            member.LastName = model.LastName;
            member.FirstName = model.FirstName;
            member.Email=model.Email;
            member.Phone = model.Phone;
            member.Birthday = model.Birthday;
            member.Gender = model.Gender;
            member.Address = model.Address;

            db.SaveChanges();
            return "修改成功";
        }
    }
}
