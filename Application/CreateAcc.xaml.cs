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
    /// Interaction logic for CreateAcc.xaml
    /// </summary>
    public partial class CreateAcc : Window
    {
        private LoginWindow loginWindow;

     

        public CreateAcc(LoginWindow loginWindow)
        {
            InitializeComponent();
            this.loginWindow = loginWindow;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.loginWindow.Show();
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

        private void SubButton_Click(object sender, RoutedEventArgs e)
        {

            using (DBEntities context = new DBEntities())
            {
                var user = this.usernamebox.Text;
                if (!(String.IsNullOrEmpty(user) || String.IsNullOrEmpty(this.pass1.Password) || String.IsNullOrEmpty(this.pass2.Password)))

                {
                    Console.WriteLine(user + this.pass1.Password);
                    var founduser = (context.UserTables.SingleOrDefault(u => u.UserName == user));
                    if (founduser != null)
                    {
                        this.TextBox.Text = "User already exists.";
                    }



                    else if (this.pass1.Password != this.pass2.Password)
                    {
                        this.TextBox.Text = "Password do not match. Try again.";
                    }
                   
                    else
                    {
                        this.TextBox.Text = "Complete! Returning to Login Screen...";
                        var NewUser = new UserTable()
                        {
                            UserName = user,
                            PasswordHash = Encrypt(this.pass1.Password),
                            DateCreated = DateTime.Now
                        };
                        context.UserTables.Add(NewUser);
                        context.SaveChanges();
                        this.Hide();
                        loginWindow.Show();
                    }

                }
                else
                {
                    this.TextBox.Text = "Fill in all blanks.";
                }
            }
        }
    }
}
