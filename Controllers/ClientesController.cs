using Microsoft.AspNetCore.Mvc;
using CadastroClientesMvc.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CadastroClientes.Controllers
{
  public class ClienteController : Controller
  {
    private readonly ClientesLista _clientesLista;

    public ClienteController(ClientesLista listaDeClientes)
    {
      _clientesLista = ClientesLista;
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(string nome, string endereco, string telefone, string rg, string cpf, string email)
    {
      _clientesLista.AdicionarCliente(new Cliente { Nome = nome, Endereco = endereco, Telefone = telefone, RG = rg, CPF = cpf, Email = email });
      return RedirectToAction("Index");
    }
  }

}
