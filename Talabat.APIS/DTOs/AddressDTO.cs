﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs
{
    public class AddressDTO
    {
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
        [Required]
        public string City { set; get; }
        [Required]
        public string Street { set; get; }
        [Required]
        public string Country { set; get; }
    }
}