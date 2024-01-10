using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarbuckXamarin.Models
{
    public class Cart
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
    }
}
