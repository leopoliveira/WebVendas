using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVendas.Contexts;
using WebVendas.Models;
using WebVendas.Models.Entities;

namespace WebVendas.Controllers
{
    public class ProductController : Controller
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product
                .OrderBy(name => name.Name)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if(id.HasValue)
            {
                var product = await _context.Product.FindAsync(id);
                if(product == null)
                {
                    TempData["message"] = Message.Serialize("Não foram encontrados registros para o produto selecionado.", Types.Error);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    return View(product);
                }
            }
            else
            {
                return View(new Product());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? id, [FromForm] Product product)
        {
            if (ModelState.IsValid) // Checa as restrições impostas na criação da Entidade
            {
                if (id.HasValue)
                {
                    bool IdExists = _context.Product.Any(productId => productId.ProductId == id);

                    if (IdExists)
                    {
                        product.Value = Convert.ToDouble(product.Value.ToString().Replace(',', '.'));
                        _context.Product.Update(product);
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            TempData["message"] = Message.Serialize("Produto atualizado com sucesso!", Types.Info);
                        }
                        else
                        {
                            TempData["message"] = Message.Serialize("Erro ao atualizar os dados do produto.", Types.Error);
                        }
                    }
                    else
                    {
                        TempData["message"] = Message.Serialize("Erro ao atualizar os dados do produto. Não foram encontrados registros.", Types.Error);
                    }
                }
                else
                {
                    product.Value = Convert.ToDouble(product.Value.ToString().Replace(',', '.'));
                    _context.Product.Add(product);
                    if(await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = Message.Serialize("Produto criado com sucesso!", Types.Info);
                    }
                    else
                    {
                        TempData["message"] = Message.Serialize("Produto não encontrado. Nenhum registro foi atualizado", Types.Error);
                    }
                }

                return RedirectToAction("Index", "Product"); //Ou usar nameof(Index) -> Método Index
            }
            else
            {
                TempData["message"] = Message.Serialize("Por favor, corriga os campos indicados.", Types.Error);
                return View("Create", product);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id.HasValue)
            {
                var product = await _context.Product.FindAsync(id.Value);

                if(product == null)
                {
                    TempData["message"] = Message.Serialize("Não foram encontrados produtos para exclusão.", Types.Error);
                }
                else
                {
                    return View(product);
                }
            }
            else
            {
                TempData["message"] = Message.Serialize("Não foram encontrados produtos para exclusão.", Types.Error); 
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if(product != null)
            {
                _context.Product.Remove(product);
                if(await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = Message.Serialize("Produto excluído com sucesso!", Types.Info);
                }
                else
                {
                    TempData["message"] = Message.Serialize("Erro ao excluir produto!", Types.Error);
                }
            }
            else
            {
                TempData["message"] = Message.Serialize("Produto não encontrado. Nenhum registro foi excluído.", Types.Error);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetProductValue(int id)
        {
            var productValue = _context.Product
                .Where(productValue => productValue.ProductId == id)
                .AsNoTracking() // Apenas Leitura do Valor
                .Select(column => column.Value)
                .FirstOrDefault();

            if (productValue == null)
            {
                return Content("0.00");
            }

            return Content(productValue.ToString("F2"));
        }
    }
}
