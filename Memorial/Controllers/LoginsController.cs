using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Memorial.Data;
using Memorial.Models;

namespace Memorial.Controllers
{
    public class LoginsController : Controller
    {
        private readonly MemorialContext _context;

        public LoginsController(MemorialContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Login.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create

        public IActionResult LoginArea()
        {
            return View();
        }

        public IActionResult Erorr()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin([Bind("Id,Name,Password")] Login login)
        {
            var loginData = _context.Login.Where(m => m.Name == login.Name && m.Password == login.Password).FirstOrDefault();
            
            if (loginData != null)
            {
                HttpContext.Session.SetString("Id", loginData.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return RedirectToAction(nameof(UserLogin));
            }            
        }
        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser([Bind("Id,Name,Password")] Login login)
        {
            var loginData = _context.Login.Where(m => m.Name == login.Name && m.Password == login.Password).FirstOrDefault();
            
            if (loginData != null)
            {
                HttpContext.Session.SetString("Id", loginData.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return RedirectToAction(nameof(UserLogin));
            }            
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin([Bind("Id,Name,Password")] Login login)
        {
            // verification 
            if (login.Name.Equals("Admin@admin.com") && login.Password.Equals("admin"))
            {
                HttpContext.Session.SetString("Name", login.Name.ToString());
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return RedirectToAction(nameof(AdminLogin));
            }            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", login.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
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
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.FindAsync(id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
