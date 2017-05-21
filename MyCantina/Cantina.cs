using System.Collections.Generic;

namespace MyCantina
{
    public class Cantina
    {
        public Cantina(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public Cantina()
        {
            
        }

        public int CantinaID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        public List<Dish> Dishes { get; set; }
        public List<Order> Orders { get; set; }
    }
}
