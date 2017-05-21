using System;
using System.Collections.Generic;

namespace MyCantina
{
    public class Order
    {
        public Order(User user, Cantina cantina, double price)
        {
            OrderDateTime = DateTime.Now;
            Paid = false;
            User = user;
            Cantina = cantina;
            Price = price;
        }

        public Order()
        {
            
        }

        public int OrderID { get; set; }

        public double Price { get; set; }
        public DateTime OrderDateTime { get; set; }
        public bool Paid { get; set; }
        public int UserID { get; set; }

        public Cantina Cantina { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public User User { get; set; }
    }
}
