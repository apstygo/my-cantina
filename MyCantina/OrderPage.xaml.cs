﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public List<Dish> ChosenDishes = new List<Dish>();
        public List<Dish> AvailableDishes;
        private Cantina _cantina;

        public OrderPage(Cantina cantina)
        {
            InitializeComponent();
            _cantina = cantina;
            using (var ctx = new MyCantinaDbContext())
            {
                var dishes = from b in ctx.Dishes
                    where b.Cantina.Name == cantina.Name
                    select b;
                AvailableDishes = dishes.ToList();
            }
        }

        private void buttonCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.GoBack();
        }

        private void buttonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var addWindow = new AddDishWindow(this);
            addWindow.ShowDialog();
        }

        private void buttonAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ChosenDishes.Count > 0)
            {
                double price = 0;
                foreach (Dish dsh in ChosenDishes)
                    price += dsh.Cost;

                if (MessageBox.Show("Оформить заказ?", "Оформить заказ", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) ==
                    MessageBoxResult.Yes)
                {
                    using (var ctx = new MyCantinaDbContext())
                    {
                        var cantina = ctx.Cantinas.First(c => c.Name == _cantina.Name);
                        var user = ctx.Users.First(u => u.Login == MainMenuWindow.User.Login);

                        var order = new Order(user, cantina, price);
                        ctx.Orders.Add(order);
                        int id = order.OrderID;
                        foreach (Dish dish in ChosenDishes)
                        {
                            var dBDish = ctx.Dishes.Single(d => d.DishID == dish.DishID);
                            ctx.OrderLines.Add(new OrderLine(id, dBDish));
                        }
                        ctx.SaveChanges();
                        MessageBox.Show("Заказ номер '" + order.OrderID + "' успешно создан.");
                        Logger.Log("Создан заказ номер '"+ order.OrderID + "'");
                    }

                    NavigationService.GoBack();
                    NavigationService.GoBack();
                }
            }
        }

        public void Refresh()
        {
            listViewOrder.ItemsSource = null;
            listViewOrder.ItemsSource = ChosenDishes;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ChosenDishes.RemoveAt(listViewOrder.SelectedIndex);
                Refresh();
            }
            catch { }
        }
    }
}
