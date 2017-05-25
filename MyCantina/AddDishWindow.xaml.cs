using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        private OrderPage _orderPage;
        
        public AddDishWindow(OrderPage orderPage)
        {
            InitializeComponent();
            _orderPage = orderPage;
            comboBoxDishes.ItemsSource = _orderPage.AvailableDishes;
        }
        
        private void comboBoxDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDishes.Text != "")
            {
                string selectedItem = comboBoxDishes.Text;
                using (var ctx = new MyCantinaDbContext())
                {
                    var dish = ctx.Dishes
                        .FirstOrDefault(b => b.Name == selectedItem);

                    _orderPage.ChosenDishes.Add(dish);
                }

                _orderPage.Refresh();

                this.Close();
            }
        }
    }
}
