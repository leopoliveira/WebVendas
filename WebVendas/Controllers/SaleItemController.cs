using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVendas.Contexts;
using WebVendas.Models;
using WebVendas.Models.Entities;

namespace WebVendas.Controllers
{
    public class SaleItemController : Controller
    {

        private readonly Context _context;

        public SaleItemController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? saleId)
        {
            if (saleId.HasValue)
            {
                ViewData["Products"] = await _context.Product
                .OrderBy(product => product.Name)
                .AsNoTracking() //Somente leitura, não farei alterações no Contexto Product
                .ToListAsync();

                ViewData["SaleItems"] = await _context.SaleItem
                    .Where(si => si.SaleId == saleId)
                    .OrderBy(si => si.SaleItemId)
                    .Include(si => si.Product)
                    .AsNoTracking()
                    .ToListAsync();

                var actualSale = await _context.Sale
                    .Include(s => s.Client)
                    .FirstOrDefaultAsync(s => s.SaleId == saleId.Value);

                ViewBag.acutalSale = actualSale;

                SaleItem saleItem = new SaleItem { SaleId = saleId.Value };
                return View(saleItem);
            }
            else
            {
                TempData["message"] = Message.Serialize("Erro ao criar itens do pedido! O pedido não foi criado.", Types.Error);
                return RedirectToAction("Client", "Sale");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SaleItem saleItem)
        {
            ModelState.Remove("Sale"); // Remove a propriedade da verificaçã do Model
            ModelState.Remove("Product"); // Remove a propriedade da verificação do Model
            if (ModelState.IsValid)
            {
                if(!(saleItem.ProductId == null) && !(saleItem.ProductId == 0) && saleItem.Quantity > 0)
                {
                    _context.SaleItem.Add(saleItem);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = Message.Serialize("Item salvo com sucesso!", Types.Info);

                        return RedirectToAction("Create", "SaleItem", new { saleId = saleItem.SaleId });
                    }
                }
            }

            TempData["message"] = Message.Serialize("Selecione um produto antes de salvar! Lembrando que sua quantidade tem que ser maior que 0.", Types.Error);

            return RedirectToAction("Create", "SaleItem", new { saleId = saleItem.SaleId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? saleId)
        {
            if (id.HasValue && saleId.HasValue)
            {
                var item = await _context.SaleItem.FindAsync(id.Value);

                if(item == null)
                {
                    TempData["message"] = Message.Serialize("Item de Venda não encontrado para exclusão! Nenhum registro foi apagado", Types.Error);
                    return RedirectToAction("Create", "SaleItem", new { saleId = saleId.Value });
                }
                else
                {
                    _context.SaleItem.Remove(item);

                    if(await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = Message.Serialize("Item de Venda excluído como sucesso!", Types.Info);
                        return RedirectToAction("Create", "SaleItem", new { saleId = saleId.Value });
                    }
                    else
                    {
                        TempData["message"] = Message.Serialize("Item de Venda não encontrado para exclusão! Nenhum registro foi apagado", Types.Error);
                        return RedirectToAction("Create", "SaleItem", new { saleId = saleId.Value });
                    }
                }
            }
            else
            {
                TempData["message"] = Message.Serialize("Não existe Item de Venda para exclusão!", Types.Error);
                return RedirectToAction("Index", "Sale");
            }
        }
    }
}
