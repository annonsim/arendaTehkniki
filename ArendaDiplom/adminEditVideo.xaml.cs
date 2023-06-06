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
    /// Логика взаимодействия для adminEditVideo.xaml
    /// </summary>
    public partial class adminEditVideo : Page
    {
        private videosDevice _curent;
        public adminEditVideo(videosDevice _selectVideo)
        {
            InitializeComponent();

            if (_selectVideo != null)
                _curent = _selectVideo;
            DataContext = _curent;

            nameDevice.Text = _curent.name + _curent.model;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new videoAdmin());
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
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
