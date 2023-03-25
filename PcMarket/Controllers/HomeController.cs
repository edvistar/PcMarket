using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcMarket.Data;
using PcMarket.Models;
using PcMarket.Models.ViewModels;
using System.Diagnostics;

namespace PcMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Producto = _db.Producto.Include(c => c.Categoria),
                Categoria = _db.Categoria
            };
            return View(homeVM);
        }
        public IActionResult Detalle(int Id)
        {
            DetalleVM detalleVM = new DetalleVM()
            {
                Producto = _db.Producto.Include(c => c.Categoria).Where(c => c.Id == Id).FirstOrDefault(),
                //Producto = _productoRepo.ObtenerPrimero(p => p.Id == Id, incluirPropiedades: "Categoria,TipoAplicacion"),
                //ExisteEnCarro = false
            };
            //foreach (var item in carroCompras)
            //{
            //    if (item.ProductoId == Id)
            //    {
            //        detalleVM.ExisteEnCarro = true;
            //    }
            //}
            return View(detalleVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}