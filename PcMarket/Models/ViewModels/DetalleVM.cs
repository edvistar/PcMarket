namespace PcMarket.Models.ViewModels
{
    public class DetalleVM
    {
        public DetalleVM()
        {
            Producto= new Producto();
        }
        public Producto Producto { get; set; }
        public int MyProperty { get; set; }
    }
}
