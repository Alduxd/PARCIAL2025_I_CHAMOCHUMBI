using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FutbolPeruano.Data;
using FutbolPeruano.Models;
using FutbolPeruano.ViewModels;

namespace FutbolPeruano.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly FutbolPeruanoContext _context;

        public AssignmentsController(FutbolPeruanoContext context)
        {
            _context = context;
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,TeamId,Notes")] Assignment assignment)
        {
            // Verificar si ya existe la asignación
            bool exists = await _context.Assignments
                .AnyAsync(a => a.PlayerId == assignment.PlayerId && a.TeamId == assignment.TeamId);
                
            if (exists)
            {
                ModelState.AddModelError("", "El jugador ya pertenece a este equipo");
            }

            if (ModelState.IsValid)
            {
                assignment.JoinDate = DateTime.Now;
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", assignment.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", assignment.TeamId);
            return View(assignment);
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.Assignments
                .Include(a => a.Player)
                .Include(a => a.Team)
                .ToListAsync();
                
            return View(assignments);
        }
    }
}
