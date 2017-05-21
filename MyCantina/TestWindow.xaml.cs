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
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            using (var ctx = new MyCantinaDbContext())
            {
                //var cantina = ctx.Cantinas.Find(1);
                //var dishes = from b in ctx.Dishes
                //             where b.Cantina.Name == "На Мясницкой"
                //             select b;
                //var dish = dishes.ToList()[0];
                //listView.ItemsSource = dishes.ToList();
                //MessageBox.Show(dishes.ToList()[0].Name);

                //var order = ctx.Orders.Find(16);
                //var orderlines = from o in ctx.OrderLines
                //                 where o.OrderID == order.OrderID
                //                 select o;

                //var dishes = new List<Dish>();
                //foreach (var n in orderlines)
                //{
                //    dishes.Add(ctx.Dishes.Single(d => d.DishID == n.DishID));
                //}
                //listView.ItemsSource = dishes.ToList();

                var users = ctx.Users.ToList();
                listView.ItemsSource = users;
            }
        }
    }
}
