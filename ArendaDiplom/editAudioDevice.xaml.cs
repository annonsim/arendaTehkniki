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
    /// Логика взаимодействия для editAudioDevice.xaml
    /// </summary>
    public partial class editAudioDevice : Page
    { 
        private audioDevice _current;
        public editAudioDevice(audioDevice select)
        {
            InitializeComponent();

            statusBox.ItemsSource = arendaDipEntities.GetContext().typeStatus.ToList();

            if (select != null)
                _current = select;

            DataContext = _current;
            
            nameDevice.Text = _current.name + _current.model;

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        { 
            manager.MainFrame.Navigate(new audioDeviceManager());
            ReloadPage();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder err = new StringBuilder();

            if(statusBox.SelectedIndex == 1)
            {
                _current.chetOrder ++;

                if (string.IsNullOrEmpty(_current.fio))
                {
                    err.AppendLine("Вы не ввели инициалы клиента!");
                    fioClean.Text = null;
                    statusBox.SelectedIndex = 1;
                }


                if (string.IsNullOrEmpty(_current.numberPhone))
                {
                    err.AppendLine("Вы не ввели номер телефона клиента!");
                    phoneClean.Text = null;
                    statusBox.SelectedIndex = 1;
                }


                if (string.IsNullOrEmpty(_current.adress))
                {
                    err.AppendLine("Вы не ввели адрес клиента!"); 
                    adresClean.Text = null;
                    statusBox.SelectedIndex = 1;
                }


                if (datestClean.SelectedDate == null)
                {
                    err.AppendLine("Вы не указали дату начала арендного срока!");
                    statusBox.SelectedIndex = 1;
                }
                    

                if (dateendClean.SelectedDate == null)
                {
                    err.AppendLine("Вы не ввели дату окончания арендного срока!");
                    statusBox.SelectedIndex = 1;
                }
                    


                
                if (err.Length > 0)
                {
                    MessageBox.Show(err.ToString());
                    statusBox.SelectedIndex = 1;
                }
                    

                if (err.Length == 0)
                {
                    arendaDipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    statusBox.SelectedIndex = 1;
                    manager.MainFrame.Navigate(new audioDeviceManager());
                    ReloadPage();
                }
            }

            else
            {
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                manager.MainFrame.Navigate(new audioDeviceManager());
                
            }

            ReloadPage();

        }

        private void cleanAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAllText();
        }

        public void ClearAllText()
        {
            statusBox.SelectedIndex = 0;
            adresClean.Text = null;
            phoneClean.Text = null;
            fioClean.Text = null;
            dateendClean.SelectedDate = null;
            datestClean.SelectedDate = null;
            adresClean.Focus();
            phoneClean.Focus();
            fioClean.Focus();
            statusBox.Focus();
        }
        private void ReloadPage()
        {
            arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            arendaDipEntities.GetContext().audioDevice.ToList();
        }
        
    }
}