namespace PcMarket.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Producto> Producto { get; set; }
        public IEnumerable<Categoria> Categoria { get; set; }
    }
}
