using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarLoc.Data;
using CarLoc.Models;

namespace CarLoc.Controllers
{
    public class ContratosController : Controller
    {
        private readonly CarLocContext _context;

        public ContratosController(CarLocContext context)
        {
            _context = context;
        }

        // GET: Contratos
        public async Task<IActionResult> Index()
        {
            var CarLocContext = _context.Contrato
                            .Include(c => c.Cliente)
                            .Include(c => c.Veiculo);

            var contratos = from c in CarLocContext
                            select c;

            return View(await contratos.ToListAsync());
        }

        // GET: Contratos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var contrato = await _context.Contrato
                .Include(c => c.Cliente)
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contrato == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");

            return View(contrato);
        }

        // GET: Contratos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");

            return View();
        }

        // POST: Contratos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,VeiculoId,DataInicio,DataTermino")] Contrato contrato)
        {
            contrato.Veiculo = await _context.Veiculo
                                        .Include(v => v.Categoria)
                                        .FirstOrDefaultAsync(v => v.Id == contrato.VeiculoId);

            contrato.ValorTotal = contrato.CalcularValorTotal(); // <-- já calcula antes!

            if (ModelState.IsValid)
            {
                _context.Add(contrato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Se deu erro de validação, volta para a view com o ValorTotal já calculado
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", contrato.ClienteId);
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo", contrato.VeiculoId);

            return View(contrato);
        }


        // GET: Contratos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");

            return View(contrato);
        }

        // POST: Contratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,VeiculoId,DataInicio,DataTermino,ValorTotal")] Contrato contrato)
        {
            if (id != contrato.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contrato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratoExists(contrato.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");

            return View(contrato);
        }

        // GET: Contratos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var contrato = await _context.Contrato
                .Include(c => c.Cliente)
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contrato == null) return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");

            return View(contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contrato = await _context.Contrato.FindAsync(id);
            if (contrato != null)
            {
                _context.Contrato.Remove(contrato);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContratoExists(int id)
        {
            return _context.Contrato.Any(e => e.Id == id);
        }
    }
}
