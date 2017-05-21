using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public static User User;
        private string dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";

        public MainMenuWindow(User user)
        {
            InitializeComponent();
            User = user;
            try
            {
                rectangleAvatar.Fill = new ImageBrush(
                    new BitmapImage(new Uri(dirTemp + @"ProfilePictures\" + user.Login + ".jpg")));
            }
            catch
            {
                rectangleAvatar.Fill = new ImageBrush(
                    new BitmapImage(new Uri(dirTemp + @"ResourceFiles\genericAvatar.jpg")));
            }

            frameMain.Content = new MainPage();
        }
    }
}
