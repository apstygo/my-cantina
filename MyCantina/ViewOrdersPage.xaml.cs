using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для ViewOrdersPage.xaml
    /// </summary>
    public partial class ViewOrdersPage : Page
    {
        private List<Order> _orderList;
        public ViewOrdersPage()
        {
            InitializeComponent();
            using (var ctx = new MyCantinaDbContext())
            {
                var orders = from o in ctx.Orders
                    where o.UserID == MainMenuWindow.User.UserID
                    select o;
                listViewOrders.ItemsSource = orders.ToList();
                _orderList = orders.ToList();
            }
        }

        private void buttonReturn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void buttonView_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            View();
        }

        private void buttonClearHistory_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool allPaid = true;
            foreach (Order o in _orderList)
                if (o.Paid == false)
                    allPaid = false;

            if (allPaid)
            {
                if (MessageBox.Show(
                        "Вы действительно хотите очистить историю заказов? Это действие необратимо.",
                        "Очистить историю", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    using (var ctx = new MyCantinaDbContext())
                    {
                        var orders = from o in ctx.Orders
                            where o.UserID == MainMenuWindow.User.UserID
                            select o;
                        foreach (Order o in orders)
                        {
                            var orderLines = from l in ctx.OrderLines
                                where l.OrderID == o.OrderID
                                select l;
                            foreach (OrderLine l in orderLines)
                            {
                                ctx.OrderLines.Remove(l);
                            }

                            ctx.Orders.Remove(o);
                        }
                        ctx.SaveChanges();


                        listViewOrders.ItemsSource = orders.ToList();
                        _orderList = orders.ToList();
                    }
                    Logger.Log("Очищена история заказов");
                }
            }
            else
                MessageBox.Show(
                    "Очистка истории невозможна, пока есть неоплаченные заказы. Оплатите заказы и попробуйте снова.",
                    "Очистка невозможна", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void listViewOrders_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            View();
        }

        private void View()
        {
            if (listViewOrders.SelectedIndex >= 0)
            {
                var order = _orderList[listViewOrders.SelectedIndex];
                NavigationService.Navigate(new ViewOrderLinesPage(order));
            }
        }
    }
}
