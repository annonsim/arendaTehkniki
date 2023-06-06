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
    /// Логика взаимодействия для videoDeviceManager.xaml
    /// </summary>
    public partial class videoDeviceManager : Page
    {
        
        public videoDeviceManager()
        {
            InitializeComponent();
            videoList.ItemsSource = arendaDipEntities.GetContext().videosDevice.ToList();
        }

        public void UpdateVideo()
        {
            var _curen = arendaDipEntities.GetContext().videosDevice.ToList();
            _curen = _curen.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();

            if (cheakActual.IsChecked.Value)
                _curen = _curen.Where(p => p.status == 1).ToList();

            videoList.ItemsSource = _curen;
        }

        private void searchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVideo();
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateVideo();
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new editVideoDevice((sender as Button).DataContext as videosDevice));
        }
    }
}
