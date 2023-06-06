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
    /// Логика взаимодействия для adminEditAudio.xaml
    /// </summary>
    public partial class adminEditAudio : Page
    {
        private audioDevice _curent;
        public adminEditAudio(audioDevice selectDevice)
        {
            InitializeComponent();

            if (selectDevice != null)
                _curent = selectDevice;

            DataContext = _curent;

            nameDevice.Text = _curent.name + _curent.model;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new audioAdmin());
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
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
