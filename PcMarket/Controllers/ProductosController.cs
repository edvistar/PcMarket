using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PcMarket.Data;
using PcMarket.Models;
using PcMarket.Models.ViewModels;

namespace PcMarket.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductosController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
    
        public IActionResult Index()
        {
            IEnumerable<Producto> lista = _db.Producto.Include(c => c.Categoria);
            return View(lista);
            
        }
        public IActionResult Upsert(int? Id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                ListaCategorias = _db.Categoria.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            if (Id == null)
            {
                //Crear nuevo producto
                return View(productoVM);
            }
            else
            {
                productoVM.Producto = _db.Producto.Find(Id);
                if (productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productoVM.Producto.Id == 0)
                {
                    //Crear
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload,fileName+extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.ImageUrl = fileName + extension;
                    _db.Producto.Add(productoVM.Producto);
                    //TempData[WC.Exitosa] = "Producto creado Exitosamente!";
                }
                else
                {
                    //Actualizar
                    var objProducto = _db.Producto.AsNoTracking().FirstOrDefault(p=> p.Id == productoVM.Producto.Id);
                    if (files.Count> 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        //borrar imagen
                        var anteriorFile = Path.Combine(upload, objProducto.ImageUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        //fin borrado imagen anterior

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productoVM.Producto.ImageUrl = fileName + extension;

                    }
                    else
                    {
                        productoVM.Producto.ImageUrl = objProducto.ImageUrl;
                    }
                    _db.Producto.Update(productoVM.Producto);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }//if ModelIsvalid
            productoVM.ListaCategorias = _db.Categoria.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            return View(productoVM);
        }
        //Get
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Producto producto = _db.Producto.Include(c => c.Categoria).FirstOrDefault();
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Producto producto)
        {
            if (producto == null)
            {
                return NotFound();
            }
            //Borrar la imagen 
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;

            //Borrar la imagen anterior
            var anteriorFile = Path.Combine(upload, producto.ImageUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }
            //fin Borrar imagen 
            _db.Remove(producto);
            _db.SaveChanges();
            //TempData[WC.Exitosa] = "Producto Eliminado Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
    }
}
