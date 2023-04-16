using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcMarket.Data;
using PcMarket.Models;
using System.Data;

namespace PcMarket.Controllers
{
    [Authorize(Roles = WC.AdmiRole)]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoriasController(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public IActionResult Index()
        {
            IEnumerable<Categoria> Lista = _db.Categoria;
            return View(Lista);
        }
        //Get
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _db.Categoria.Add(categoria);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public IActionResult Editar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categoria.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _db.Categoria.Update(categoria);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Categoria.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Categoria categoria)
        {
            if (categoria == null)
            {
                return NotFound();
            }
            _db.Categoria.Remove(categoria);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
