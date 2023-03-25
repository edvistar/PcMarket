using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcMarket.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre del producto es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ingrese la cantidad para el stock")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "La descripcion Corta es requerida")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "La descripcion Completa es requerida")]
        public string LongDescription { get; set; }
        [Required(ErrorMessage = "El Nombre del producto es requerido")]
        [Range(1, double.MaxValue, ErrorMessage="El precio debe ser mayor a cero")]
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }

    }
}
