using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace MyCantina
{
    class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double OrderPrice { get; set; }

        public List<OrderLine> OrderLines { get; set; }
        public User User { get; set; }
    }
}
