﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyCantina
{
    class Dish
    {
        public int DishID { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }

        public List<Order> Orders { get; set; }
        public List<Cantina> Cantinas { get; set; }
    }
}
