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
    /// Interaction logic for CreateContact.xaml
    /// </summary>
    public partial class CreateContact : Window
    {
        private MainWindow mainwindow;
        private object listbox;

        public CreateContact(MainWindow mainwindow, int Id, ListBox listbox)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
            this.IdText.Text = Id.ToString();
            this.listbox = listbox;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities context = new DBEntities())
            {
                var NewContact = new Contact()
                {
                    Id = Int32.Parse(this.IdText.Text),
                    FirstName = this.FirstName.Text,
                    LastName = this.LastName.Text,
                    Address = this.Address.Text,
                    PhoneNumber = this.Phone.Text,
                    DateCreated = DateTime.Now
                };

               
                mainwindow.listBox.Items.Add(NewContact.FirstName + " " + NewContact.LastName);
                context.Contacts.Add(NewContact);
                context.SaveChanges();
                this.Hide();
                mainwindow.Show();

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainwindow.Show();
        }
    }
}
