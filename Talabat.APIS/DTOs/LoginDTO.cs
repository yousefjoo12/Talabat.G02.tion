using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }

        [Required]
        public string Passwoed { set; get; }

    }
}
