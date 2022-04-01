using Example.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleMVC.Models
{
    public class CartViewModel
    {
        public CartViewModel(Product prod, int q) { product = prod; quantity = q; }
        public Product product{ get; set; }
        public int quantity { get; set; }
    }
}