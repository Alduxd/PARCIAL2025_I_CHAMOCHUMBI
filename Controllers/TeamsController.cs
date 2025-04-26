using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FutbolPeruano.Data;
using FutbolPeruano.Models;

namespace FutbolPeruano.Controllers
{
    public class TeamsController : Controller
    {
        private readonly FutbolPeruanoContext _context;

        public TeamsController(FutbolPeruanoContext context)
        {
            _context = context;
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,City,FoundedDate")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.ToListAsync();
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Assignments)
                .ThenInclude(a => a.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }
    }
}
