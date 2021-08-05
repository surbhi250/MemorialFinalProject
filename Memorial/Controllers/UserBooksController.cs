using Memorial.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memorial.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly MemorialContext _context;

        public UserBooksController(MemorialContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Id") != null)
            {
                var value = HttpContext.Session.GetString("Id");

                return View(await _context.BuyBooks.Where(x => x.UserId == Convert.ToInt32(value)).ToListAsync());
            }
            else
                return RedirectToAction("UserLogin", "Logins");
        }
    }
}
