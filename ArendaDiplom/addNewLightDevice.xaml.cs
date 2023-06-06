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
    /// Логика взаимодействия для addNewLightDevice.xaml
    /// </summary>
    public partial class addNewLightDevice : Page
    {
        private lightDevice _current = new lightDevice();
        public addNewLightDevice()
        {
            InitializeComponent();

            comboList.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();

            DataContext = _current;

            inputPrice.Text = null;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new lightAdmin());
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            comboList.SelectedIndex = 0;

            StringBuilder err = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_current.name))
            {
                err.AppendLine("Введите название устройства!");
            }

            if (string.IsNullOrWhiteSpace(_current.price.ToString()))
            {
                err.AppendLine("Укажите стоимость!");
            }

            if (string.IsNullOrWhiteSpace(_current.count.ToString()))
            {
                err.AppendLine("Укажите количество экземпляров!");
            }

            _current.chetOrder = 0;

            if (err.Length > 0)
            {
                MessageBox.Show(err.ToString());
            }

            if (err.Length == 0)
            {
                arendaDipEntities.GetContext().lightDevice.Add(_current);
                try
                {
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    manager.MainFrame.Navigate(new lightAdmin());
                    arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    arendaDipEntities.GetContext().lightDevice.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }
    }
}
