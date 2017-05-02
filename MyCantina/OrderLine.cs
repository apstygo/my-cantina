using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCantina
{
    class OrderLine
    {
        [Key]
        public int OrderLineID { get; set; }

        public int OrderID { get; set; }
        public int Quantity { get; set; }

        public Dish Dish { get; set; }
    }
}
