using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public static User User;
        private string _dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";

        public MainMenuWindow(User user)
        {
            InitializeComponent();

            if (!File.Exists(_dirTemp + "log.ini"))
                buttonOpenLog.IsEnabled = false;

            Logger.Log("Пользователь '" + user.Login + "' совершил вход");
            User = user;

            try
            {
                rectangleAvatar.Fill = new ImageBrush(
                    new BitmapImage(new Uri(_dirTemp + @"ProfilePictures\" + user.Login + ".jpg")));
            }
            catch
            {
                rectangleAvatar.Fill = new ImageBrush(
                    new BitmapImage(new Uri(_dirTemp + @"ResourceFiles\genericAvatar.jpg")));
            }

            frameMain.Content = new MainPage();
        }

        private void buttonOpenLog_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_dirTemp + "log.ini");
        }
    }
}
