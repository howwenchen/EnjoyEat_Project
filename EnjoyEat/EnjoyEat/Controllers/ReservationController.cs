using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;

namespace EnjoyEat.Controllers
{
    public class ReservationController : Controller
    {
        private readonly db_a989fe_thm101team6Context _context;

        public ReservationController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
              return _context.ReservationViewModel != null ? 
                          View(await _context.ReservationViewModel.ToListAsync()) :
                          Problem("Entity set 'db_a989fe_thm101team6Context.ReservationViewModel'  is null.");
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ReservationViewModel == null)
            {
                return NotFound();
            }

            var reservationViewModel = await _context.ReservationViewModel
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (reservationViewModel == null)
            {
                return NotFound();
            }

            return View(reservationViewModel);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,ReservationDate,ConfirmationDate,NumberofGuest,ReservationTime")] ReservationViewModel reservationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationViewModel);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ReservationViewModel == null)
            {
                return NotFound();
            }

            var reservationViewModel = await _context.ReservationViewModel.FindAsync(id);
            if (reservationViewModel == null)
            {
                return NotFound();
            }
            return View(reservationViewModel);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,ReservationDate,ConfirmationDate,NumberofGuest,ReservationTime")] ReservationViewModel reservationViewModel)
        {
            if (id != reservationViewModel.PhoneNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationViewModelExists(reservationViewModel.PhoneNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationViewModel);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ReservationViewModel == null)
            {
                return NotFound();
            }

            var reservationViewModel = await _context.ReservationViewModel
                .FirstOrDefaultAsync(m => m.PhoneNumber == id);
            if (reservationViewModel == null)
            {
                return NotFound();
            }

            return View(reservationViewModel);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ReservationViewModel == null)
            {
                return Problem("Entity set 'db_a989fe_thm101team6Context.ReservationViewModel'  is null.");
            }
            var reservationViewModel = await _context.ReservationViewModel.FindAsync(id);
            if (reservationViewModel != null)
            {
                _context.ReservationViewModel.Remove(reservationViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationViewModelExists(string id)
        {
          return (_context.ReservationViewModel?.Any(e => e.PhoneNumber == id)).GetValueOrDefault();
        }
    }
}
