using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { set; get; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string PhoneNumber { set; get; }
        [Required]
       // [RegularExpression(@"^(?=.[A-Za-z])(?=.\d)(?=.[@$!%#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage ="Invalid Password")]
        public string Password { set; get; }
    }
}
