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
    /// Логика взаимодействия для editVideoDevice.xaml
    /// </summary>
    public partial class editVideoDevice : Page
    {
        private videosDevice _current;
        public editVideoDevice(videosDevice selectDevice)
        {
            InitializeComponent();
            if (selectDevice != null)
                _current = selectDevice;

            DataContext = _current;

            nameDevice.Text = _current.name + _current.model;

            statusBox.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new videoDeviceManager());
            ReloadPage();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if(statusBox.SelectedIndex == 1)
            {
                _current.chetOrder ++;
                if (string.IsNullOrWhiteSpace(_current.fio))
                {
                    errors.AppendLine("Вы не ввели инициалы клиента!");
                    fioClean.Text = null;
                }


                if (string.IsNullOrWhiteSpace(_current.numberPhone))
                {
                    errors.AppendLine("Вы не ввели номер телефона клиента!");
                    phoneClean.Text = null;
                }


                if (string.IsNullOrWhiteSpace(_current.adress))
                {
                    errors.AppendLine("Вы не ввели адрес клиента!");
                    adresClean.Text = null;
                }

                if (datestClean.SelectedDate == null)
                    errors.AppendLine("Вы не указали дату начала арендного срока!");

                if (dateendClean.SelectedDate == null)
                    errors.AppendLine("Вы не ввели дату окончания арендного срока!");

                if (errors.Length > 0)
                    MessageBox.Show(errors.ToString());

                if (errors.Length == 0)
                {
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    manager.MainFrame.Navigate(new videoDeviceManager());
                    ReloadPage();
                }
            }
            else
            {
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                manager.MainFrame.Navigate(new videoDeviceManager());
            }

            ReloadPage();
        }

        private void cleanAll_Click(object sender, RoutedEventArgs e)
        {
            statusBox.SelectedIndex = 0;
            fioClean.Text = null;
            phoneClean.Text = null;
            adresClean.Text = null;
            dateendClean.SelectedDate = null;
            datestClean.SelectedDate = null;
            fioClean.Focus();
            phoneClean.Focus();
            adresClean.Focus();
            statusBox.Focus();
            
        }

        private void ReloadPage()
        {
            arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            arendaDipEntities.GetContext().videosDevice.ToList();
        }
    }
}
