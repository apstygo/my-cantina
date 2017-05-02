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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string filename = "LoginsTemp.txt";
        private List<string> logins = new List<string>();
        private List<string> passwords = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            getLogins();
        }

        private void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            string login, password;
            try
            {
                login = getLogPas(textBoxLogin.Text)[0];
            }
            catch
            {
                login = null;
            }
            if (login != null)
                password = getLogPas(textBoxLogin.Text)[1];
            else
                password = null;
            var window = new MainMenuWindow();


            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                MessageBox.Show("Для входа требуется логин.");
                textBoxLogin.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                MessageBox.Show("Для входа требуется пароль.");
                passwordBox.Focus();
                return;
            }
            if (login == null)
            {
                MessageBox.Show("Пользователя с таким именем не существует.");
                textBoxLogin.Focus();
                return;
            }
            if (passwordBox.Password != password)
            {
                MessageBox.Show("Неверный пароль, попробуйте снова.");
                passwordBox.Focus();
                return;
            }
            this.Close();
            window.Show();

            
        }

        private void getLogins()
        {
            using (var sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var split = line.Split('-');
                    logins.Add(split[0]);
                    passwords.Add(split[1]);
                }
            }
        }

        private string[] getLogPas(string loginAttempt)
        {
            try
            {
                string[] logPas = new string[2];
                int n = this.logins.IndexOf(loginAttempt);
                string pas = this.passwords[n];
                logPas[0] = loginAttempt;
                logPas[1] = pas;
                return logPas;
            }
            catch
            {
                return null;
            }
        }

        private void buttonSignUp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
