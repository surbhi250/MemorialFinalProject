using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Memorial.Data;
using Memorial.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Memorial.Controllers
{
        public class AuthorController : Controller
        {
            private readonly MemorialContext _context;

            public AuthorController(MemorialContext context)
            {
                _context = context;
            }

            // GET: Author
            public async Task<IActionResult> Index()
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("Name");

                if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("Name") != null)
            {
                var login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();

                if (name == "Admin@admin.com")
                {
                    return View(await _context.Author.ToListAsync());
                }
                else
                    return RedirectToAction(nameof(UserAuthor));
            }
                else
                    return RedirectToAction(nameof(UserAuthor));
            }
        
                public async Task<IActionResult> UserAuthor()
                {
                
                    return View(await _context.Author.ToListAsync());
            }

            // GET: Author/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Author = await _context.Author
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Author == null)
                {
                    return NotFound();
                }

                return View(Author);
            }

            // GET: Author/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Author/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,AuthorName,Email,Contact")] Author Author)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(Author);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Author);
            }

            // GET: Author/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Author = await _context.Author.FindAsync(id);
                if (Author == null)
                {
                    return NotFound();
                }
                return View(Author);
            }

            // POST: Author/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorName,Email,Contact")] Author Author)
            {
                if (id != Author.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Author);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AuthorExists(Author.Id))
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
                return View(Author);
            }

            // GET: Author/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Author = await _context.Author
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Author == null)
                {
                    return NotFound();
                }

                return View(Author);
            }

            // POST: Author/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Author = await _context.Author.FindAsync(id);
                _context.Author.Remove(Author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool AuthorExists(int id)
            {
                return _context.Author.Any(e => e.Id == id);
            }
        }
    }