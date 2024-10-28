using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class AddressIDto
    {
        [Required]
        public string FName { set; get; }
        [Required] 
        public string LName { set; get; }
        [Required] 
        public string Street { set; get; }
        [Required]
        public string City { set; get; }
        [Required]
        public string Country { set; get; }
    }
}
