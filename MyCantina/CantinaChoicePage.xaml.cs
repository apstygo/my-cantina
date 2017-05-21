using System.Windows.Controls;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для CantinaChoicePage.xaml
    /// </summary>
    public partial class CantinaChoicePage : Page
    {
        
        public CantinaChoicePage()
        {
            InitializeComponent();
        }

        private void buttonKirpich_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var ctx = new MyCantinaDbContext())
            {
                NavigationService.Navigate(new OrderPage(ctx.Cantinas.Find(1)));
            }
        }

        private void buttonShabolovka_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var ctx = new MyCantinaDbContext())
            {
                NavigationService.Navigate(new OrderPage(ctx.Cantinas.Find(2)));
            }
        }

        private void buttonMyaso_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            using (var ctx = new MyCantinaDbContext())
            {
                NavigationService.Navigate(new OrderPage(ctx.Cantinas.Find(3)));
            }
        }

        private void buttonBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
