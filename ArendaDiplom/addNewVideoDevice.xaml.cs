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

namespace ArendaDiplom
{
    /// <summary>
    /// Логика взаимодействия для addNewVideoDevice.xaml
    /// </summary>
    public partial class addNewVideoDevice : Page
    {
        private videosDevice _current = new videosDevice();
        public addNewVideoDevice()
        {
            InitializeComponent();
            comboList.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();
            DataContext = _current;
            inputPrice.Text = null;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new videoAdmin());
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            comboList.SelectedIndex = 0;

            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_current.name))
                error.AppendLine("Введите название устройства!");

            if (inputPrice.Text == "0")
                error.AppendLine("Укажите стоимость!");

            if (string.IsNullOrWhiteSpace(_current.count.ToString()))
                error.AppendLine("Укажите количество экземпляров!");

            _current.chetOrder = 0;

            if (error.Length > 0)
                MessageBox.Show(error.ToString());

            if (error.Length == 0)
            {
                arendaDipEntities.GetContext().videosDevice.Add(_current);
                try
                {
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    manager.MainFrame.Navigate(new videoAdmin());
                    arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    arendaDipEntities.GetContext().videosDevice.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
                
        }
    }
}
