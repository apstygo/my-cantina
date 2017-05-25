using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using Res = MyCantina.Properties.Resources;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";
        private List<string> _logins = new List<string>();
        private List<string> _passwords = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            Directory.CreateDirectory(_dirTemp + "ProfilePictures");
            Directory.CreateDirectory(_dirTemp + "ResourceFiles");
            Res.genericAvatar.Save(_dirTemp + @"ResourceFiles\" + "genericAvatar.jpg");
            Res.iconOrder.Save(_dirTemp + @"ResourceFiles\" + "iconOrder.jpg");
            Res.iconSettings.Save(_dirTemp + @"ResourceFiles\" + "iconSettings.jpg");
            Res.iconView.Save(_dirTemp + @"ResourceFiles\" + "iconView.jpg");
            
            GetLogins();

            textBoxLogin.Focus();
        }

        private void SignIn()
        {
            string login, passwordHash;
            try
            {
                login = GetLogPas(textBoxLogin.Text)[0];
            }
            catch
            {
                login = null;
            }
            if (login != null)
                passwordHash = GetLogPas(textBoxLogin.Text)[1];
            else
                passwordHash = null;


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
            if (!Hash.Same(passwordBox.Password, passwordHash))
            {
                MessageBox.Show("Неверный пароль, попробуйте снова.");
                passwordBox.Focus();
                return;
            }


            using (var ctx = new MyCantinaDbContext())
            {
                User user = ctx.Users
                    .FirstOrDefault(u => u.Login == login);

                var window = new MainMenuWindow(user);
                this.Close();
                window.Show();
            }
        }

        private void buttonSignIn_Click(object sender, RoutedEventArgs e)
        {
            SignIn();
        }
        
        private void GetLogins()
        {
            using (var ctx = new MyCantinaDbContext())
            {
                if (ctx.Users.ToList().Count > 0)
                {
                    List<User> users = ctx.Users.ToList();
                    foreach (User n in users)
                    {
                        _logins.Add(n.Login);
                        _passwords.Add(n.PasswordHash);
                    }
                }
            }
        }
        
        private string[] GetLogPas(string loginAttempt)
        {
            try
            {
                string[] logPas = new string[2];
                int n = this._logins.IndexOf(loginAttempt);
                string pas = this._passwords[n];
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
            var window = new SignUpWindow(_logins);
            window.ShowDialog();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignIn();
            }
        }
    }
}
