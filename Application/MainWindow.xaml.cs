using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginWindow loginWindow;

        public MainWindow(LoginWindow loginWindow, int Id)
        {
            InitializeComponent();
            this.loginWindow = loginWindow;
            this.IdText.Text = Id.ToString();

           
        

            using (DBEntities context = new DBEntities())
            {

                foreach (Contact c in context.Contacts)
                {

                    if (c.Id == Id)
                    {
                        listBox.Items.Add(c.FirstName + " " + c.LastName);
                        context.SaveChanges();
                    }
                }
            }
        }

       

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities context = new DBEntities())
            {
                string[] name = listBox.SelectedItem.ToString().Split(null);
                var first = name[0];
                var last = name[1];

                var query = from c in context.Contacts
                            where c.FirstName == first
                            where c.LastName == last
                            select c;
                var contact = query.SingleOrDefault();
                MessageBox.Show(contact.FirstName + " " + contact.LastName + "\n Phone: " 
                    + contact.PhoneNumber + "\n Address " + contact.Address + "\n Date Created: " + contact.DateCreated);
            }
                
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = Int32.Parse(this.IdText.Text);
            CreateContact create = new CreateContact(this, Id, listBox);
            this.Hide();
            create.Show();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities context = new DBEntities())
            {
                string[] name = listBox.SelectedItem.ToString().Split(null);
                var first = name[0];
                var last = name[1];

                var del = from c in context.Contacts
                          where c.FirstName == first
                          where c.LastName == last
                          select c;
                var con = del.Single();

                context.Contacts.Remove(con);
                listBox.Items.RemoveAt(listBox.Items.IndexOf(listBox.SelectedItem));
                context.SaveChanges();

            }

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            loginWindow.Show();

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditContact edit = new EditContact(this);
            this.Hide();
            edit.Show();



    }
    }
}
