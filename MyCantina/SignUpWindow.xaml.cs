using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyCantina
{
    /// <summary>
    /// Логика взаимодействия для SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private string _dirTemp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyCantinaTemp\";
        
        private List<string> _loginsList;

        public SignUpWindow(List<string> loginsList)
        {
            this._loginsList = loginsList;

            InitializeComponent();
            imageAvatar.Source = new BitmapImage(new Uri(_dirTemp + @"ResourceFiles\genericAvatar.jpg"));
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (LoginExists(_loginsList, textBoxLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже существует.");
                return;
            }
            if (ContainsDash(textBoxLogin.Text) || ContainsDash(textBoxName.Text) ||
                ContainsDash(passwordBoxOne.Password) || ContainsDash(passwordBoxTwo.Password))
            {
                MessageBox.Show("Введенные данные содержат недопустимый символ '-', повторите ввод.");
                return;
            }
            if (textBoxLogin.Text == "" || textBoxName.Text == "" || passwordBoxOne.Password == "" || passwordBoxTwo.Password == "")
            {
                MessageBox.Show("Введены не все требуемые данные, повторите ввод.");
                return;
            }
            if (passwordBoxOne.Password != passwordBoxTwo.Password)
            {
                MessageBox.Show("Введенные пароли не совпадают, повторите ввод.");
                return;
            }
            
            string savedPasswordHash = Hash.GetHash(passwordBoxTwo.Password);
            
            using (var ctx = new MyCantinaDbContext())
            {
                User usr = new User(textBoxName.Text, textBoxLogin.Text, savedPasswordHash);

                ctx.Users.Add(usr);
                ctx.SaveChanges();
                Logger.Log("Создан пользователь '" + usr.Login + "'");
            }
            
            if (!Directory.Exists(_dirTemp + @"ProfilePictures\"))
                Directory.CreateDirectory(_dirTemp + @"ProfilePictures\");

            try
            {
                File.Copy(textBoxAvatarPath.Text, _dirTemp + @"ProfilePictures\" + textBoxLogin.Text + ".jpg");
                Logger.Log("Добавлен аватар");
            }
            catch
            {
                MessageBox.Show(
                    @"Вы сможете позже вручную поменять аватар, заменив файл C:\Users\User\Documents\MyCantinaTemp\ProfilePictures\(ваш логин).jpg", "Замена аватара", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            MessageBox.Show("Регистрация выполнена успешно.");
            if (MessageBox.Show("Для завершения регистрации требуется перезапуск. Перезапустить сейчас?",
                    "Завершение регистрации.", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            this.Close();
        }

        private void buttonChooseFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "Файлы изображений (*.gif,*.jpg,*.jpeg,*.bmp,*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.png";
            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                string filename = dlg.FileName;
                textBoxAvatarPath.Text = filename;
            }
        }

        private void buttonChangeAvatar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imageAvatar.Source = new BitmapImage(new Uri(textBoxAvatarPath.Text));
            }
            catch
            {
                MessageBox.Show("Указанный путь не содержит файла изображения.");
            }
        }

        private bool ContainsDash(string str)
        {
            foreach (char n in str)
            {
                if (n == '-') return true;
            }
            return false;
        }

        private bool LoginExists(List<string> loginsList, string login)
        {
            foreach (string n in loginsList)
            {
                if (n == login)
                    return true;
            }
            return false;
        }
    }
}
