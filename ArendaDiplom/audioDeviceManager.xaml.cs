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
    /// Логика взаимодействия для audioDeviceManager.xaml
    /// </summary>
    public partial class audioDeviceManager : Page
    {

        public audioDeviceManager()
        {
            InitializeComponent();
            audioDeviceList.ItemsSource = arendaDipEntities.GetContext().audioDevice.ToList();

        }

        private void UpdateDevice()
        {
            var currentDevice = arendaDipEntities.GetContext().audioDevice.ToList();

            currentDevice = currentDevice.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();

            if (cheakActual.IsChecked.Value)
                currentDevice = currentDevice.Where(p => p.status == 1).ToList();

            audioDeviceList.ItemsSource = currentDevice;



        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDevice();
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateDevice();
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new editAudioDevice((sender as Button).DataContext as audioDevice));
        }
    }
}
