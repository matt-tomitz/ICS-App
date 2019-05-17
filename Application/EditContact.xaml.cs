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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EditContact.xaml
    /// </summary>
    public partial class EditContact : Window
    {
         private MainWindow mainwindow;
         public EditContact(MainWindow mainwindow)
         {
            InitializeComponent();
            this.mainwindow = mainwindow;

            using (DBEntities context = new DBEntities())
            {


                string[] name = mainwindow.listBox.SelectedItem.ToString().Split(null);
                var first = name[0];
                var last = name[1];

                var query = from c in context.Contacts
                            where c.FirstName == first
                            where c.LastName == last
                            select c;
                var contact = query.SingleOrDefault();

                this.FirstName.Text = contact.FirstName;
                this.LastName.Text = contact.LastName;
                this.Address.Text = contact.Address;
                this.Phone.Text = contact.PhoneNumber;
                this.Id.Text = contact.Id.ToString();


            }
         }


                private void BackButton_Click(object sender, RoutedEventArgs e)
                {
                    this.Hide();
                    mainwindow.Show();
                }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities context = new DBEntities())
            {
                string[] name = mainwindow.listBox.SelectedItem.ToString().Split(null);
                var first = name[0];
                var last = name[1];

                var query = from c in context.Contacts
                            where c.FirstName == first
                            where c.LastName == last
                            select c;
                var contact = query.SingleOrDefault();

                contact.FirstName = this.FirstName.Text;
                contact.LastName = this.LastName.Text;
                contact.Address = this.Address.Text;
                contact.PhoneNumber = this.Phone.Text;

                mainwindow.listBox.Items.RemoveAt(mainwindow.listBox.Items.IndexOf(mainwindow.listBox.SelectedItem));
                mainwindow.listBox.Items.Add(contact.FirstName + " " + contact.LastName);

                context.SaveChanges();
                this.Hide();
                mainwindow.Show();

            }

        }
    }
}
