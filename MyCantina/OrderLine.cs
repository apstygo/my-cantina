namespace MyCantina
{
    public class OrderLine
    {
        public OrderLine(int orderID, Dish dish)
        {
            OrderID = orderID;
            Dish = dish;
        }

        public OrderLine()
        {
            
        }
        public int OrderLineID { get; set; }
        public int OrderID { get; set; }
        public int DishID { get; set; }

        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}
