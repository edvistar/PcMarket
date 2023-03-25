using System.ComponentModel.DataAnnotations;

namespace PcMarket.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La categoria es obligatoria")]
        public string Name { get; set; }
    }
}
