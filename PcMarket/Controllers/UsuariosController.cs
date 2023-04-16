using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcMarket.Data;
using PcMarket.Models;
using System.Data;

namespace PcMarket.Controllers
{
    [Authorize(Roles = WC.AdmiRole)]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UsuariosController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           IEnumerable<UsuarioAplicacion> lista = _db.UsuarioAplicacion;
            var usuarioLista = _db.UsuarioAplicacion.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var usuario in usuarioLista)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name; 
            }
            return View(lista);
            //return Json(new {data = usuarioLista});
        }
        public IActionResult Upsert() 
        {
            return View();
        }
    }
}
