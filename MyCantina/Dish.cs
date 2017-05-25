namespace MyCantina
{
    public class Dish
    {
        public Dish(string name, double cost, int cantinaID)
        {
            Name = name;
            Cost = cost;
            CantinaID = cantinaID;
        }

        public Dish()
        {
            
        }

        public override string ToString()
        {
            return Name;
        }

        public int CantinaID { get; set; }
        public int DishID { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        
        public Cantina Cantina { get; set; }
    }
}
