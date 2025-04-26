using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutbolPeruano.Data;
using FutbolPeruano.Models;

namespace FutbolPeruano.Controllers
{
    public class PlayersController : Controller
    {
        private readonly FutbolPeruanoContext _context;

        public PlayersController(FutbolPeruanoContext context)
        {
            _context = context;
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["Teams"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Position")] Player player, int? teamId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();

                // Si se seleccionó un equipo, crear la asignación
                if (teamId.HasValue)
                {
                    var assignment = new Assignment
                    {
                        PlayerId = player.Id,
                        TeamId = teamId.Value,
                        JoinDate = DateTime.Now
                    };
                    _context.Assignments.Add(assignment);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Teams"] = new SelectList(_context.Teams, "Id", "Name");
            return View(player);
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.ToListAsync());
        }
    }
}
