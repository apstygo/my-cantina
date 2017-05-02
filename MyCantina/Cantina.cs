using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace MyCantina
{
    class Cantina
    {
        public int CantinaID { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
    }
}
