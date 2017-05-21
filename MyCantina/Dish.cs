using System.Collections.Generic;
using System.Windows.Markup;

namespace MyCantina
{
    public class Dish
    {
        public Dish(string name, double cost, Cantina cantina)
        {
            Name = name;
            Cost = cost;
            Cantina = cantina;
        }

        public Dish()
        {
            
        }

        public override string ToString()
        {
            return Name;
        }

        public int DishID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        
        public Cantina Cantina { get; set; }
    }
}
