﻿using AutoMapper;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;

namespace Talabat.APIS.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductsDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductsDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)) 
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
