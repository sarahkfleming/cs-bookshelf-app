using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookshelf.Data;
using Bookshelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace Bookshelf.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        private SqlConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public AuthorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        public async Task<IActionResult> UserAuthorReport()
        {
            using var conn = Connection;
            using var cmd = conn.CreateCommand();
            conn.Open();

            cmd.CommandText = @"
                                                SELECT u.FirstName, u.LastName, COUNT(a.Id) AS AuthorCount
                                                 FROM AspNetUsers u
                                                LEFT JOIN Authors a ON u.Id = a.UserCreatingId
                                                GROUP BY u.FirstName, u.LastName";

            var reader = cmd.ExecuteReader();

            List<UserAuthorReportViewModel> report = new List<UserAuthorReportViewModel>();
            while (reader.Read())
            {
                report.Add(new UserAuthorReportViewModel()
                {
                    UserFirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    UserLastName = reader.GetString(reader.GetOrdinal("LastName")),
                    AuthorCount = reader.GetInt32(reader.GetOrdinal("AuthorCount"))
                });

            }
            reader.Close();
            return View(report);
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            // The code below resticts the results to the current user
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicationDbContext = _context.Authors
                                                        //.Include(a => a.UserCreating)
                                                        .Where(a => a.UserCreatingId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                //.Include(a => a.UserCreating)
                .Include(a => a.BooksWritten)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PenName,PreferredGenre,UserCreatingId")] Author author)
        {
            ModelState.Remove("UserCreatingId");
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                author.UserCreatingId = user.Id;
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PenName,PreferredGenre,UserCreatingId")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            ModelState.Remove("UserCreatingId");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    author.UserCreatingId = user.Id;
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                //.Include(a => a.UserCreating)
                .Include(a => a.BooksWritten)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            //TODO: Find out why BooksWritten doesn't show the books
            if (author.BooksWritten.Count == 0)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
