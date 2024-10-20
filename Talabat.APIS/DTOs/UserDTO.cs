using Talabat.Core.Entites.Identity;

namespace Talabat.APIS.DTOs
{
    public class UserDTO
    {
        public string DisplayName { set; get; }
        public string Email { set; get; }
        public string Token { set; get; }
    }
}
