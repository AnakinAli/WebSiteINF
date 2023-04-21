using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteINF.Data;
using WebSiteINF.Models;

namespace WebSiteINF.Controllers
{
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
              return _context.Ad != null ? 
                          View(await _context.Ad.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Ad'  is null.");
        }

        // GET: Ads
        public async Task<IActionResult> GetAds()
        {
            return _context.Ad != null ?
                        View(await _context.Ad.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Ad'  is null.");
        }

        

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ad == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        [Authorize]
        // GET: Ads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AdminID,ImageLink,Text,Title")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ad == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AdminID,ImageLink,Text,Title")] Ad ad)
        {
            if (id != ad.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.ID))
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
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ad == null)
            {
                return NotFound();
            }

            var ad = await _context.Ad
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ad == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Ad'  is null.");
            }
            var ad = await _context.Ad.FindAsync(id);
            if (ad != null)
            {
                _context.Ad.Remove(ad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
          return (_context.Ad?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
