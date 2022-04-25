using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebVendas.Contexts;
using WebVendas.Models;
using WebVendas.Models.Entities;

namespace WebVendas.Controllers
{
    public class SaleController : Controller
    {

        private readonly Context _context;

        public SaleController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sale
                .OrderByDescending(sale => sale.SaleNumber)
                .Take(20)
                .Include("Client")
                .ToListAsync();

            return View(sales);
        }

        [HttpGet]
        public async Task<IActionResult> Client()
        {
            Sale sale = new Sale();

            ViewData["Clients"] = await _context.Client
                .OrderBy(client => client.Name)
                .AsNoTracking() // Apenas leitura nesse contexto
                .ToListAsync();

            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Sale sale)
        {
            if(sale == null)
            {
                TempData["message"] = Message.Serialize("Por favor, selecione um Cliente para iniciar a venda.", Types.Error);
                return RedirectToAction("Client", "Sale");
            }
            else
            {
                sale.Date = DateTime.Now.ToLocalTime();
                sale.SaleNumber = _context.Sale.Max(s => s.SaleNumber) + 1;

                _context.Sale.Add(sale);


                if(await _context.SaveChangesAsync() > 0)
                {
                    return RedirectToAction("Create", "SaleItem", new { saleId = sale.SaleId });
                }
                else
                {
                    TempData["message"] = Message.Serialize("Erro ao criar uma nova venda, por favor, tente novamente!", Types.Error);
                    return RedirectToAction("Index", "Sale");
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Close(int id, double totalPrice)
        {
            var sale = await _context.Sale.FindAsync(id);

            if(sale == null)
            {
                TempData["message"] = Message.Serialize("Erro ao fechar pedido, por favor, tente novamente!", Types.Error);

                return RedirectToAction("Create", "SaleItem", new { saleId = id });
            }
            else
            {

                ViewBag.Sale = await _context.Sale
                    .Include(s => s.Client)
                    .FirstOrDefaultAsync(s => s.SaleId == id);

                ViewBag.SaleItems = await _context.SaleItem
                    .Where(si => si.SaleId == id)
                    .Include(si => si.Product)
                    .AsNoTracking()
                    .ToListAsync();

                sale.TotalValue = totalPrice;
                sale.Closed = true;
                _context.Sale.Update(sale);

                if(await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = Message.Serialize("Venda fechada com sucesso!", Types.Info);
                    return View(sale);
                }
                else
                {
                    TempData["message"] = Message.Serialize("Erro ao fechar pedido, por favor, tente novamente!", Types.Error);

                    return RedirectToAction("Create", "SaleItem", new { saleId = id });
                }
            }
        }

        [HttpPost]
        public IActionResult GetLastSaleId()
        {
            var lastId = _context.Sale
                .OrderByDescending(column => column.SaleId)
                .Select(column => column.SaleId).FirstOrDefault();

            if(lastId == null)
            {
                return NotFound();
            }

            return Content(lastId.ToString());
        }

    }
}