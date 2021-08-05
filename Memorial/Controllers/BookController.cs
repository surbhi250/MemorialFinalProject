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
    public class BookController : Controller
    {
        private readonly MemorialContext _context;

        public BookController(MemorialContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
          return View(await _context.Books.ToListAsync());
        }
        public async Task<IActionResult> AddBook(int? id)
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                BuyBooks buyBook = new BuyBooks();
                var book = _context.Books.Where(x => x.Id == id).FirstOrDefault();
                var author = _context.Author.Where(x => x.Id == book.AuthorId).FirstOrDefault();
                buyBook.BookName = book.BookName;
                buyBook.Title = book.Title;
                buyBook.NumberOfChapters = book.NumberOfChapters;
                buyBook.Author = author.AuthorName;
                buyBook.UserId = Convert.ToInt32(value);
                _context.Add(buyBook);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "UserBooks");
            }
            else
                return RedirectToAction("UserLogin", "Logins");
        }

    }
}
