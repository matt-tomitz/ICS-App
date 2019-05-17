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
using System.Security.Cryptography;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities context = new DBEntities()) {
                var user = context.UserTables.FirstOrDefault(u => u.UserName == this.usernameBox.Text);

                if (user != null)
                {
                    if (user.PasswordHash == Encrypt(this.passwordBox.Password))
                    {

                        this.Hide();
                        MainWindow app = new MainWindow(this, user.Id);
                        app.Show();
                    }
                    else
                    {
                        this.Textbox.Text= "Invalid password.";
                    }
                }
                else
                {
                    this.Textbox.Text= "User not found.";
                }
            }


        }
        static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CreateAcc createWin = new CreateAcc(this);
            createWin.Show();
        }
    }
}
