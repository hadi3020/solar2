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
    public class OrderItemsController : Controller
    {
        private readonly SolarPanelContext _context;

        public OrderItemsController(SolarPanelContext context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {

            var solarPanelContext = _context.OrderItems.Include(o => o.OIdNavigation).Include(o => o.PIdNavigation)
                .Include(o=>o.OIdNavigation.UIdNavigation);

            var order = from data in _context.OrderItems
                        join data2 in _context.Orders
                        on data.OId equals data2.OId
                        join data3 in _context.Products
                        on data.PId equals data3.PId
                        select data;


            return View(await solarPanelContext.ToListAsync());
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.OIdNavigation)
                .Include(o => o.PIdNavigation)
                .FirstOrDefaultAsync(m => m.OIId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OId"] = new SelectList(_context.Orders, "OId", "OId");
            ViewData["PId"] = new SelectList(_context.Products, "PId", "Pdetails");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OIId,Qty,OId,PId")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OId"] = new SelectList(_context.Orders, "OId", "OId", orderItem.OId);
            ViewData["PId"] = new SelectList(_context.Products, "PId", "Pdetails", orderItem.PId);
            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OId"] = new SelectList(_context.Orders, "OId", "OId", orderItem.OId);
            ViewData["PId"] = new SelectList(_context.Products, "PId", "Pdetails", orderItem.PId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OIId,Qty,OId,PId")] OrderItem orderItem)
        {
            if (id != orderItem.OIId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OIId))
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
            ViewData["OId"] = new SelectList(_context.Orders, "OId", "OId", orderItem.OId);
            ViewData["PId"] = new SelectList(_context.Products, "PId", "Pdetails", orderItem.PId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.OIdNavigation)
                .Include(o => o.PIdNavigation)
                .FirstOrDefaultAsync(m => m.OIId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderItems == null)
            {
                return Problem("Entity set 'SolarPanelContext.OrderItems'  is null.");
            }
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
          return (_context.OrderItems?.Any(e => e.OIId == id)).GetValueOrDefault();
        }
    }
}
