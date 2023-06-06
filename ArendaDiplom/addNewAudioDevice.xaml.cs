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
    /// Логика взаимодействия для addNewAudioDevice.xaml
    /// </summary>
    public partial class addNewAudioDevice : Page
    {
        private audioDevice _curent = new audioDevice();
        public addNewAudioDevice()
        {
            InitializeComponent();
             
            comboList.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();
            DataContext = _curent;

            inputPrice.Text = null;

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new audioAdmin());
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            comboList.SelectedIndex = 0;

            StringBuilder err = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_curent.name))
            {
                err.AppendLine("Введите название устройства!");
            }

            if (inputPrice.Text == "0")
            {
                err.AppendLine("Укажите стоимость!");
            }
            
            if (string.IsNullOrWhiteSpace(_curent.count.ToString()))
            {
                err.AppendLine("Укажите количество экземпляров!");
            }
            _curent.chetOrder = 0;

            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
            }

            if (err.Length == 0)
            {
                arendaDipEntities.GetContext().audioDevice.Add(_curent);
                try
                {
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    manager.MainFrame.Navigate(new audioAdmin());
                    arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    arendaDipEntities.GetContext().audioDevice.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }

        }

        
    }
}
