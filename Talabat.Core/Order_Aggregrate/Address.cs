using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregrate
{ //Address_order
    public class Address
    {
        public Address(string firstName, string lastName, string city, string street, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            Country = country;
        }
        public Address()
        {
            
        }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string City { set; get; }
        public string Street { set; get; }
        public string Country { set; get; }


    }
}
