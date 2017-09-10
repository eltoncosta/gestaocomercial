using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoComercial.Web.Data;
using GestaoComercial.Web.Models;

namespace GestaoComercial.Web.Controllers
{
    public class CompraController : Controller
    {
        private readonly GestaoComercialContext _context;

        public CompraController(GestaoComercialContext context)
        {
            _context = context;
        }

        // GET: Compra
        public async Task<IActionResult> Index(string searchCodConta, string searchNomeConta, bool searchStatusCompra)
        {
            ViewData["searchCodConta"] = searchCodConta;
            ViewData["searchNomeConta"] = searchNomeConta;
            ViewData["searchStatusCompra"] = searchStatusCompra;

            var gestaoComercialContext = from c in _context.Compra.Include(c => c.Conta).ThenInclude(d => d.Cliente) select c;

            if (!String.IsNullOrEmpty(searchCodConta))
            {
                gestaoComercialContext = gestaoComercialContext.Where(x => x.Conta.ContaID.Equals(Convert.ToInt32(searchCodConta)));
            }
            if (!String.IsNullOrEmpty(searchNomeConta))
            {
                gestaoComercialContext = gestaoComercialContext.Where(x => x.Conta.Cliente.Nome.Contains(searchNomeConta.ToUpper()));
            }
            if (searchStatusCompra)
            {
                gestaoComercialContext = gestaoComercialContext.Where(x => x.Conta.Status.Equals(searchStatusCompra));
            }

            return View(await gestaoComercialContext.ToListAsync());
        }

        // GET: Compra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra.SingleOrDefaultAsync(m => m.CompraID == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compra/Create
        public IActionResult Create()
        {
            var compra = from s in _context.Conta.Include(c => c.Cliente) select new { ContaID = s.ContaID, CONTA = s.ContaID + " - " + s.Cliente.Nome };
            ViewData["ContaID"] = new SelectList(compra, "ContaID", "CONTA");
            return View();
        }

        // POST: Compra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContaID,Valor")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var compraList = from s in _context.Conta.Include(c => c.Cliente) select new { ContaID = s.ContaID, CONTA = s.ContaID + " - " + s.Cliente.Nome };
            ViewData["ContaID"] = new SelectList(compraList, "ContaID", "ContaID", compra.ContaID);
            return View(compra);
        }

        // GET: Compra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra.SingleOrDefaultAsync(m => m.CompraID == id);
            if (compra == null)
            {
                return NotFound();
            }
            var compraList = from s in _context.Conta.Include(c => c.Cliente) select new { ContaID = s.ContaID, CONTA = s.ContaID + " - " + s.Cliente.Nome };
            ViewData["ContaID"] = new SelectList(compraList, "ContaID", "CONTA", compra.ContaID);
            return View(compra);
        }

        // POST: Compra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompraID,ContaID,Valor,Status")] Compra compra)
        {
            if (id != compra.CompraID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.CompraID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            var compraList = from s in _context.Conta.Include(c => c.Cliente) select new { ContaID = s.ContaID, CONTA = s.ContaID + " - " + s.Cliente.Nome };
            ViewData["ContaID"] = new SelectList(compraList, "ContaID", "ContaID", compra.ContaID);
            return View(compra);
        }

        // GET: Compra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compra.SingleOrDefaultAsync(m => m.CompraID == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compra.SingleOrDefaultAsync(m => m.CompraID == id);
            _context.Compra.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CompraExists(int id)
        {
            return _context.Compra.Any(e => e.CompraID == id);
        }
    }
}
