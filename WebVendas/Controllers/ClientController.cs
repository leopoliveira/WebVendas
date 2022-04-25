using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVendas.Contexts;
using WebVendas.Models;
using WebVendas.Models.Entities;

namespace WebVendas.Controllers
{
    public class ClientController : Controller
    {

        private readonly Context _context;

        public ClientController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var client = await _context.Client
                .OrderBy(name => name.Name)
                .ToListAsync();

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if(id.HasValue)
            {
                var client = await _context.Client.FindAsync(id);

                if(client == null)
                {
                    TempData["message"] = Message.Serialize("Não foram encontrados clientes.", Types.Error);
                    return RedirectToAction("Index", "Client");
                }
                else
                {
                    return View(client);
                }
            }
            else
            {
                return View(new Client());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? id, [FromForm] Client client)
        {
            ModelState.Remove("Sales"); // Remove a propriedade Sales da validação
            if (ModelState.IsValid) // Checa as restrições impostas na criação da Entidade
            {
                if (id.HasValue)
                {
                    bool IdExist = _context.Client.Any(clientId => clientId.ClientId == id);

                    if (IdExist)
                    {
                        _context.Client.Update(client);
                        if(await _context.SaveChangesAsync() > 0)
                        {
                            TempData["message"] = Message.Serialize("Cliente atualizado com sucesso!", Types.Info);
                        }
                        else
                        {
                            TempData["message"] = Message.Serialize("Cliente não encontrado.Nenhum registro foi atualizado.", Types.Error);
                        }
                    }
                    else
                    {
                        TempData["message"] = Message.Serialize("Erro ao atualizar os dados do cliente. Não foram encontrados registros.", Types.Error);
                    }
                }
                else
                {
                    _context.Client.Add(client);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        TempData["message"] = Message.Serialize("Cliente criado com sucesso!", Types.Info);
                    }
                    else
                    {
                        TempData["message"] = Message.Serialize("Produto não encontrado. Nenhum registro foi atualizado", Types.Error);
                    }
                }
            }
            else
            {
                return View(client);
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var client = await _context.Client.FindAsync(id);
                
                if(client == null)
                {
                    TempData["message"] = Message.Serialize("Não foram encontrados clientes para exclusão.", Types.Error);
                }
                else
                {
                    return View(client);
                }
            }
            else
            {
                TempData["message"] = Message.Serialize("Não foram encontrados clientes para exclusão.", Types.Error);
            }

            return RedirectToAction("Index", "Client");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Client.FindAsync(id);

            if (client == null)
            {
                TempData["message"] = Message.Serialize("Não foram encontrados clientes para excluir.", Types.Error);
            }
            else
            {
                _context.Client.Remove(client);

                if (await _context.SaveChangesAsync() > 0)
                {
                    TempData["message"] = Message.Serialize("Cliente excluído com sucesso!", Types.Info);
                }
                else
                {
                    TempData["message"] = Message.Serialize("Cliente não encontrado. Nenhum registro foi excluído.", Types.Error);
                }
            }

            return RedirectToAction("Index", "Client");
        }
    }
}
