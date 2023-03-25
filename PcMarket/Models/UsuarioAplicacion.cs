using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcMarket.Models
{
    public class UsuarioAplicacion : IdentityUser
    {
        public string NombreCompleto { get; set; }
        [NotMapped]
        public string Role { get; set; }
    }
}
