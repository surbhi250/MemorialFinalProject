using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Memorial.Data;
using Memorial.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Memorial.Controllers
{
        public class BooksController : Controller
        {
            private readonly MemorialContext _context;

            public BooksController(MemorialContext context)
            {
                _context = context;
            }

            // GET: Books
            public async Task<IActionResult> Index()
            {
                var value = HttpContext.Session.GetString("Id");
                var name = HttpContext.Session.GetString("Name");

            if (HttpContext.Session.GetString("Id") != null || HttpContext.Session.GetString("Name") != null)
            {
                var login = _context.Login.Where(x => x.Id == Convert.ToInt32(value)).FirstOrDefault();
                if(_context.Author.ToList().Count == 0)
                {
                    return RedirectToAction(nameof(AuthorNotFound));
                }
                else
                {
                    if (name == "Admin@admin.com")
                    {
                        return View(await _context.Books.ToListAsync());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Book");
                    }
                }
            }
            else
                return RedirectToAction("UserLogin", "Logins");
            }

            // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Books = await _context.Books
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Books == null)
                {
                    return NotFound();
                }

                return View(Books);
            }
        public IActionResult AuthorNotFound()
        {
            return View();
        }
        
        // GET: Books/Create
        public IActionResult Create()
            {
            var Authors = _context.Author.ToList();
            Authors.Insert(0, new Author { Id = 0, AuthorName = "Select Author" });
            ViewBag.ListAuthors = Authors;
            return View();
            }

            // POST: Books/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,BookName,Title,NumberOfChapters,AuthorId")] Books Books)
            {
                if (ModelState.IsValid)
                {
                    //Books.UserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                    _context.Add(Books);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(Books);
            }

            // GET: Books/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Books = await _context.Books.FindAsync(id);
                if (Books == null)
                {
                    return NotFound();
                }
                return View(Books);
            }

            // POST: Books/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BookName,Title,NumberOfChapters,AuthorId,Contact,Email,Address")] Books Books)
            {
                if (id != Books.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(Books);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BooksExists(Books.Id))
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
                return View(Books);
            }

            // GET: Books/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var Books = await _context.Books
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (Books == null)
                {
                    return NotFound();
                }

                return View(Books);
            }

            // POST: Books/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var Books = await _context.Books.FindAsync(id);
                _context.Books.Remove(Books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool BooksExists(int id)
            {
                return _context.Books.Any(e => e.Id == id);
            }
        }
    }
