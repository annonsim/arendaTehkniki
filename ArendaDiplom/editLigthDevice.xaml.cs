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
    /// Логика взаимодействия для editLigthDevice.xaml
    /// </summary>
    public partial class editLigthDevice : Page
    {
        private lightDevice _curent;
        public editLigthDevice(lightDevice selectDevice)
        {
            InitializeComponent();

            if (selectDevice != null)
                _curent = selectDevice;
            DataContext = _curent;

            nameDevice.Text = _curent.name + _curent.model;

            statusBox.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();
        }

        private void cleanAll_Click(object sender, RoutedEventArgs e)
        {
            statusBox.SelectedIndex = 0;
            fioClean.Text = null;
            adresClean.Text = null;
            phoneClean.Text = null;
            datestClean.SelectedDate = null;
            dateendClean.SelectedDate = null;
            fioClean.Focus();
            adresClean.Focus();
            phoneClean.Focus();
            statusBox.Focus();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new ligthDeviceManager());
            ReloadPage();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();

            if (statusBox.SelectedIndex == 1)
            {
                _curent.chetOrder ++;

                if (string.IsNullOrWhiteSpace(_curent.fio))
                {
                    err.AppendLine("Вы не ввели инициалы клиента!");
                    fioClean.Text = null;
                }


                if (string.IsNullOrWhiteSpace(_curent.numberPhone))
                {
                    err.AppendLine("Вы не ввели номер телефона клиента!");
                    phoneClean.Text = null;
                }


                if (string.IsNullOrWhiteSpace(_curent.adress))
                {
                    err.AppendLine("Вы не ввели адрес клиента!");
                    adresClean.Text = null;
                }

                if (datestClean.SelectedDate == null)
                    err.AppendLine("Вы не указали дату начала арендного срока!");

                if (dateendClean.SelectedDate == null)
                    err.AppendLine("Вы не ввели дату окончания арендного срока!");



                if (err.Length > 0)
                    MessageBox.Show(err.ToString());

                if (err.Length == 0)
                {
                    statusBox.SelectedIndex = 1;
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    manager.MainFrame.Navigate(new ligthDeviceManager());
                    ReloadPage();
                }
            }

            else
            {
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                manager.MainFrame.Navigate(new ligthDeviceManager());

            }

            ReloadPage();

        }

        private void ReloadPage()
        {
            arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            arendaDipEntities.GetContext().lightDevice.ToList();
        }
    }
}
