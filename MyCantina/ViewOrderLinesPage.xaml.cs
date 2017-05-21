using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для ViewOrderLinesPage.xaml
    /// </summary>
    public partial class ViewOrderLinesPage : Page
    {
        private double totalCost = 0;
        private int orderID;
        public ViewOrderLinesPage(Order order)
        {
            InitializeComponent();

            orderID = order.OrderID;

            if (order.Paid)
                buttonPay.IsEnabled = false;

            using (var ctx = new MyCantinaDbContext())
            {
                var orderlines = from o in ctx.OrderLines
                    where o.OrderID == order.OrderID
                    select o;

                var dishes = new List<Dish>();
                foreach (var n in orderlines)
                {
                    dishes.Add(ctx.Dishes.Single(d => d.DishID == n.DishID));
                }
                listViewOrderLines.ItemsSource = dishes.ToList();

                foreach (Dish n in dishes)
                    totalCost += n.Cost;
            }
        }

        private void buttonPay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Стоимость заказа составила " + totalCost + "₽. Оплатить?", "Оплата заказа",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var ctx = new MyCantinaDbContext())
                {
                    ctx.Orders.Find(orderID).Paid = true;
                    ctx.SaveChanges();
                }
                MessageBox.Show("Заказ успешно оплачен.");
                buttonPay.IsEnabled = false;
            }
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewOrdersPage());
        }
    }
}
