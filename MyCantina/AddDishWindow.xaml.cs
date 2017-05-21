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
using System.Windows.Shapes;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для AddDishWindow.xaml
    /// </summary>
    public partial class AddDishWindow : Window
    {
        private OrderPage OrderPage;
        
        public AddDishWindow(OrderPage orderPage)
        {
            InitializeComponent();
            OrderPage = orderPage;
            comboBoxDishes.ItemsSource = OrderPage.AvailableDishes;
            //comboBoxDishes.SelectedIndex = 0;
            //comboBoxDishes.Text = "Выберите блюдо";
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

                    OrderPage.ChosenDishes.Add(dish);
                }

                OrderPage.Refresh();

                this.Close();
            }
        }
    }
}
