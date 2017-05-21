using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для ViewOrdersPage.xaml
    /// </summary>
    public partial class ViewOrdersPage : Page
    {
        private List<Order> orderList;
        public ViewOrdersPage()
        {
            InitializeComponent();
            using (var ctx = new MyCantinaDbContext())
            {
                var orders = from o in ctx.Orders
                    where o.UserID == MainMenuWindow.User.UserID
                    select o;
                listViewOrders.ItemsSource = orders.ToList();
                orderList = orders.ToList();
            }
        }

        private void buttonReturn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void buttonView_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var order = orderList[listViewOrders.SelectedIndex];
            NavigationService.Navigate(new ViewOrderLinesPage(order));
        }
    }
}
