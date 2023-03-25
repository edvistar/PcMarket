using Microsoft.AspNetCore.Mvc.Rendering;

namespace PcMarket.Models.ViewModels
{
    public class ProductoVM
    {
        public Producto Producto { get; set; }
        public IEnumerable<SelectListItem>? ListaCategorias { get; set; }
    }
}
