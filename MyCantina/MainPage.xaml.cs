using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private string _dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";
        public MainPage()
        {
            InitializeComponent();
            
            imageOrder.Source = new BitmapImage(new Uri(_dirTemp + @"ResourceFiles\iconOrder.jpg"));
            imageView.Source = new BitmapImage(new Uri(_dirTemp + @"ResourceFiles\iconView.jpg"));
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CantinaChoicePage());
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewOrdersPage());
        }
    }
}
