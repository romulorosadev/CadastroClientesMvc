using Microsoft.AspNetCore.Mvc;
using CadastroClientesMvc.Data;
using CadastroClientesMvc.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Controllers
{
  public class ClientesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ClientesController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      return View(await _context.Clientes.ToListAsync());
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID_Cliente,Nome,Endereco,Telefone,RG,CPF,Email")] Cliente cliente)
    {
      if (ModelState.IsValid)
      {
        _context.Add(cliente);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(cliente);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var cliente = await _context.Clientes.FindAsync(id);
      if (cliente == null)
      {
        return NotFound();
      }
      return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID_Cliente,Nome,Endereco,Telefone,RG,CPF,Email")] Cliente cliente)
    {
      if (id != cliente.ID_Cliente)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(cliente);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ClienteExists(cliente.ID_Cliente))
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
      return View(cliente);
    }

    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var cliente = await _context.Clientes
          .FirstOrDefaultAsync(m => m.ID_Cliente == id);
      if (cliente == null)
      {
        return NotFound();
      }

      return View(cliente);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var cliente = await _context.Clientes.FindAsync(id);
      _context.Clientes.Remove(cliente);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ClienteExists(int id)
    {
      return _context.Clientes.Any(e => e.ID_Cliente == id);
    }
  }
}
