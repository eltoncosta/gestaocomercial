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
    public class ContaController : Controller
    {
        private readonly GestaoComercialContext _context;

        public ContaController(GestaoComercialContext context)
        {
            _context = context;
        }

        // GET: Conta
        public async Task<IActionResult> Index(string searchCodConta, string searchNomeConta)
        {
            ViewData["searchCodConta"] = searchCodConta;
            ViewData["searchNomeConta"] = searchNomeConta;

            var contas = from s in _context.Conta.Include(c => c.Cliente)
                         select s;
            if (!String.IsNullOrEmpty(searchCodConta))
            {
                contas = contas.Where(s => s.ContaID.Equals(Convert.ToInt32(searchCodConta)));

            }
            if (!String.IsNullOrEmpty(searchNomeConta))
            {
                contas = contas.Where(s => s.Cliente.Nome.Contains(searchNomeConta.ToUpper()));

            }
            return View(await contas.AsNoTracking().ToListAsync());


        }

        public ActionResult ClientesAtivos()
        {
            var contador = _context.Conta.Where(X => X.Status.Equals(true)).Count();
            return Json(new { quantidade = contador});
        }

        // GET: Conta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta.SingleOrDefaultAsync(m => m.ContaID == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // GET: Conta/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "Nome");
            return View();
        }

        // POST: Conta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteID,Status")] Conta conta)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(conta);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "Nome", conta.ClienteID);
            return View(conta);

        }

        // GET: Conta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta.SingleOrDefaultAsync(m => m.ContaID == id);
            if (conta == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "Nome", conta.ClienteID);
            return View(conta);
        }

        // POST: Conta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContaID,ClienteID,Status")] Conta conta)
        {
            if (id != conta.ContaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContaExists(conta.ContaID))
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
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "Nome", conta.ClienteID);
            return View(conta);
        }

        // GET: Conta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conta = await _context.Conta.SingleOrDefaultAsync(m => m.ContaID == id);
            if (conta == null)
            {
                return NotFound();
            }

            return View(conta);
        }

        // POST: Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conta = await _context.Conta.SingleOrDefaultAsync(m => m.ContaID == id);
            _context.Conta.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContaExists(int id)
        {
            return _context.Conta.Any(e => e.ContaID == id);
        }
    }
}
