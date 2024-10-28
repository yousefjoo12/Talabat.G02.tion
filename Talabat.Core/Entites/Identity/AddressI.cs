using System.Text.Json.Serialization;

namespace Talabat.Core.Entites.Identity
{
    public class AddressI
    {
        public int Id { get; set; } 
        public string FName { set; get; }
        public string LName { set; get; }
        public string Street { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public string AppUserId { set; get; }
        [JsonIgnore]
        public AppUser User { set; get; }
    }
}