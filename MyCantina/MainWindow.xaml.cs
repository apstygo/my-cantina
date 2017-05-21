using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Documents;
using Res = MyCantina.Properties.Resources;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";
        private List<string> logins = new List<string>();
        private List<string> passwords = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            Directory.CreateDirectory(dirTemp + "ProfilePictures");
            Directory.CreateDirectory(dirTemp + "ResourceFiles");
            Res.genericAvatar.Save(dirTemp + @"ResourceFiles\" + "genericAvatar.jpg");
            Res.iconOrder.Save(dirTemp + @"ResourceFiles\" + "iconOrder.jpg");
            Res.iconSettings.Save(dirTemp + @"ResourceFiles\" + "iconSettings.jpg");
            Res.iconView.Save(dirTemp + @"ResourceFiles\" + "iconView.jpg");

            //Res.borsch.Save(dirTemp + @"ResourceFiles\" + "borsch.jpg");
            //Res.bread.Save(dirTemp + @"ResourceFiles\" + "bread.jpg");
            //Res.coffee.Save(dirTemp + @"ResourceFiles\" + "coffee.jpg");
            //Res.croissants.Save(dirTemp + @"ResourceFiles\" + "croissants.jpg");
            //Res.kotlety.Save(dirTemp + @"ResourceFiles\" + "kotlety.jpg");
            //Res.pasta.Save(dirTemp + @"ResourceFiles\" + "pasta.jpg");
            //Res.plov.Save(dirTemp + @"ResourceFiles\" + "plov.jpg");
            //Res.salad.Save(dirTemp + @"ResourceFiles\" + "salad.jpg");
            //Res.scons.Save(dirTemp + @"ResourceFiles\" + "scons.jpg");
            //Res.tea.Save(dirTemp + @"ResourceFiles\" + "tea.jpg");
            
            GetLogins();

            textBoxLogin.Focus();

            using (var ctx = new MyCantinaDbContext())
            {
                //List<Cantina> cantinas = new List<Cantina>();
                //cantinas.Add(new Cantina("На Кирпичной", "ул. Кирпичная, д. 33"));
                //cantinas.Add(new Cantina("На Шаболовке", "ул. Шаболовка, д. 26"));
                //cantinas.Add(new Cantina("На Мясницкой", "ул. Мясницкая, д. 20"));
                //foreach (Cantina cantina in cantinas)
                //{
                //    ctx.Cantinas.Add(cantina);
                //}

                //ctx.SaveChanges();


                //ctx.Dishes.Add(new Dish("Чай", 20, ctx.Cantinas.Find(1)));
                //ctx.Dishes.Add(new Dish("Кофе", 120, ctx.Cantinas.Find(1)));
                //ctx.Dishes.Add(new Dish("Хлеб", 10, ctx.Cantinas.Find(1)));
                //ctx.Dishes.Add(new Dish("Борщ", 70, ctx.Cantinas.Find(2)));
                //ctx.Dishes.Add(new Dish("Плов", 150, ctx.Cantinas.Find(2)));
                //ctx.Dishes.Add(new Dish("Круассан", 40, ctx.Cantinas.Find(2)));
                //ctx.Dishes.Add(new Dish(@"Салат 'Весне я рад'", 50, ctx.Cantinas.Find(2)));
                //ctx.Dishes.Add(new Dish("Пампушки", 30, ctx.Cantinas.Find(3)));
                //ctx.Dishes.Add(new Dish("Котлеты с пюрешкой", 100, ctx.Cantinas.Find(3)));
                //ctx.Dishes.Add(new Dish("Макароны по-флотски", 90, ctx.Cantinas.Find(3)));

                //ctx.SaveChanges();
            }

            //var test = new TestWindow();
            //test.ShowDialog();
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

        //  Получает логины-пароли из файла

        private void GetLogins()
        {
            using (var ctx = new MyCantinaDbContext())
            {
                List<User> users = ctx.Users.ToList();
                foreach (User n in users)
                {
                    logins.Add(n.Login);
                    passwords.Add(n.PasswordHash);
                }
            }
        }

        //  Ищет (по логину) пару логин-пароль из ЛИСТОВ

        private string[] GetLogPas(string loginAttempt)
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
            var window = new SignUpWindow(logins);
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
