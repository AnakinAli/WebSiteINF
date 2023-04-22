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
    public class SubjectAreasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectAreasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubjectAreas
        public async Task<IActionResult> Index()
        {
              return _context.SubjectArea != null ? 
                          View(await _context.SubjectArea.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SubjectArea'  is null.");
        }

        // GET: SubjectAreas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubjectArea == null)
            {
                return NotFound();
            }

            var subjectArea = await _context.SubjectArea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectArea == null)
            {
                return NotFound();
            }

            return View(subjectArea);
        }

        // GET: SubjectAreas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubjectAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectAreaDisctiption")] SubjectArea subjectArea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectArea);
        }

        // GET: SubjectAreas/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubjectArea == null)
            {
                return NotFound();
            }

            var subjectArea = await _context.SubjectArea.FindAsync(id);
            if (subjectArea == null)
            {
                return NotFound();
            }
            return View(subjectArea);
        }

        // POST: SubjectAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectAreaDisctiption")] SubjectArea subjectArea)
        {
            if (id != subjectArea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectArea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectAreaExists(subjectArea.Id))
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
            return View(subjectArea);
        }

        // GET: SubjectAreas/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubjectArea == null)
            {
                return NotFound();
            }

            var subjectArea = await _context.SubjectArea
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subjectArea == null)
            {
                return NotFound();
            }

            return View(subjectArea);
        }

        // POST: SubjectAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubjectArea == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubjectArea'  is null.");
            }
            var subjectArea = await _context.SubjectArea.FindAsync(id);
            if (subjectArea != null)
            {
                _context.SubjectArea.Remove(subjectArea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectAreaExists(int id)
        {
          return (_context.SubjectArea?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
