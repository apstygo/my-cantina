using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для ViewOrderLinesPage.xaml
    /// </summary>
    public partial class ViewOrderLinesPage : Page
    {
        private double _totalCost = 0;
        private int _orderId;
        public ViewOrderLinesPage(Order order)
        {
            InitializeComponent();

            _orderId = order.OrderID;

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
                    _totalCost += n.Cost;
            }
        }

        private void buttonPay_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Стоимость заказа составила " + _totalCost + "₽. Оплатить?", "Оплата заказа",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                using (var ctx = new MyCantinaDbContext())
                {
                    ctx.Orders.Find(_orderId).Paid = true;
                    ctx.SaveChanges();
                }
                MessageBox.Show("Заказ успешно оплачен.");
                Logger.Log("Оплачен заказ номер '" + _orderId + "'");
                buttonPay.IsEnabled = false;
            }
        }

        private void buttonReturn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewOrdersPage());
        }
    }
}
