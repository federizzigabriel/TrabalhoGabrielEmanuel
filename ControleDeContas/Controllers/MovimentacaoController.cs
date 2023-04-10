using ControleDeContas.Data;
using ControleDeContas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeMovimentacoes.Controllers
{
    public class MovimentacaoController : Controller
    {
        public readonly ControleDeContasContext _context;

        public MovimentacaoController(ControleDeContasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movimentacoes.OrderBy(i => i.DataPagamento).ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("DataVencimento", "DataPagamento", "ValorDevido", "ValorPago", "TotalParcelas", "NumeroParcela")] Movimentacao movimentacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(movimentacao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar a movimentacao.");
            }
            return View(movimentacao);
        }

        public async Task<ActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movimentacao = await _context.Movimentacoes.SingleOrDefaultAsync(i => i.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            return View(movimentacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id", "DataVencimento", "DataPagamento", "ValorDevido", "ValorPago", "TotalParcelas", "NumeroParcela")] Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movimentacaoExists(movimentacao.Id))
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
            return View(movimentacao);
        }

        public bool movimentacaoExists(long? id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }

        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movimentacao = await _context.Movimentacoes.SingleOrDefaultAsync(i => i.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            return View(movimentacao);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movimentacao = await _context.Movimentacoes.SingleOrDefaultAsync(i => i.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            return View(movimentacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var movimentacao = await _context.Movimentacoes.SingleOrDefaultAsync(i => i.Id == id);
            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
