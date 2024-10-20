﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
            Item = new List<BasketItem>();
        }

        public string Id { get; set; }
        public List<BasketItem> Item { get; set; }
    }
}
