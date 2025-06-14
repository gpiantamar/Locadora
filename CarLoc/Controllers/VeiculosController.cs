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
    public class VeiculosController : Controller
    {
        private readonly CarLocContext _context;

        public VeiculosController(CarLocContext context)
        {
            _context = context;
        }

        // GET: Veiculos
        public async Task<IActionResult> Index(string marca)
        {
            if (_context.Veiculo == null)
            {
                return Problem("Tabela Veiculos está vazia");
            }

            var veiculos = _context.Veiculo
                            .Include(v => v.Categoria)
                            .Include(v => v.Status)
                            .AsQueryable();

            if (!string.IsNullOrEmpty(marca))
            {
                veiculos = veiculos.Where(v => v.Marca!.ToUpper().Contains(marca.ToUpper()));
            }

            return View(await veiculos.ToListAsync());
        }


        // GET: Veiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var veiculo = await _context.Veiculo
                .Include(v => v.Categoria)
                .Include(v => v.Status)
                .Include(v => v.Contratos)
                    .ThenInclude(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veiculo == null) return NotFound();

            return View(veiculo);
        }

        // GET: Veiculos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,Ano,Placa,Cor,StatusId,CategoriaId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,Ano,Placa,Cor,StatusId,CategoriaId")] Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Categoria, "Id", "Nome");
            ViewData["VeiculoId"] = new SelectList(_context.Veiculo, "Id", "Modelo");
            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculo = await _context.Veiculo.FindAsync(id);
            if (veiculo != null)
            {
                _context.Veiculo.Remove(veiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculo.Any(e => e.Id == id);
        }
    }
}
