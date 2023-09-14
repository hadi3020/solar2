using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Solar_Panel.Data;
using Solar_Panel.Models;

namespace Solar_Panel.Controllers
{
    public class ServiceesController : Controller
    {
        private readonly SolarPanelContext _context;

        public ServiceesController(SolarPanelContext context)
        {
            _context = context;
        }

        // GET: Servicees
        public async Task<IActionResult> Index()
        {
              return _context.Servicees != null ? 
                          View(await _context.Servicees.ToListAsync()) :
                          Problem("Entity set 'SolarPanelContext.Servicees'  is null.");
        }

        // GET: Servicees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicees == null)
            {
                return NotFound();
            }

            var servicee = await _context.Servicees
                .FirstOrDefaultAsync(m => m.SId == id);
            if (servicee == null)
            {
                return NotFound();
            }

            return View(servicee);
        }

        // GET: Servicees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SId,SName,SImage,SDetails,SPrice")] Servicee servicee, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                string imageFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/myfiles");

                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }

                string filePath = Path.Combine(imageFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                var dbAddress = Path.Combine("myfiles", fileName);
                servicee.SImage = dbAddress;

                _context.Add(servicee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicee);
        }

        // GET: Servicees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servicees == null)
            {
                return NotFound();
            }

            var servicee = await _context.Servicees.FindAsync(id);
            if (servicee == null)
            {
                return NotFound();
            }
            return View(servicee);
        }

        // POST: Servicees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SId,SName,SImage,SDetails,SPrice")] Servicee servicee)
        {
            if (id != servicee.SId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceeExists(servicee.SId))
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
            return View(servicee);
        }

        // GET: Servicees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicees == null)
            {
                return NotFound();
            }

            var servicee = await _context.Servicees
                .FirstOrDefaultAsync(m => m.SId == id);
            if (servicee == null)
            {
                return NotFound();
            }

            return View(servicee);
        }

        // POST: Servicees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicees == null)
            {
                return Problem("Entity set 'SolarPanelContext.Servicees'  is null.");
            }
            var servicee = await _context.Servicees.FindAsync(id);
            if (servicee != null)
            {
                _context.Servicees.Remove(servicee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceeExists(int id)
        {
          return (_context.Servicees?.Any(e => e.SId == id)).GetValueOrDefault();
        }
    }
}
