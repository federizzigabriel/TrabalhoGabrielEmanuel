using ControleDeContas.Data;
using ControleDeContas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContas.Controllers
{
    public class ContaController : Controller
    {
        public readonly ControleDeContasContext _context;

        public ContaController(ControleDeContasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Contas.OrderBy(i => i.Nome).ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome", "Detalhes")] Conta conta)
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
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar a conta.");
            }
            return View(conta);
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conta = await _context.Contas.SingleOrDefaultAsync(i => i.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id,[Bind("Id", "Nome", "Detalhes")] Conta conta)
        {
            if (id != conta.Id)
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
                    if (!ContaExists(conta.Id))
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
            return View(conta);
        }

        public bool ContaExists(long? id)
        {
            return _context.Contas.Any(e => e.Id == id);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conta = await _context.Contas.SingleOrDefaultAsync(i => i.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conta = await _context.Contas.SingleOrDefaultAsync(i => i.Id == id);
            if (conta == null)
            {
                return NotFound();
            }
            return View(conta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var conta = await _context.Contas.SingleOrDefaultAsync(i => i.Id == id);
            _context.Contas.Remove(conta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
