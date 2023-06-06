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
using System.Windows.Shapes;

namespace ArendaDiplom
{
    /// <summary>
    /// Логика взаимодействия для managerPage.xaml
    /// </summary>
    public partial class managerPage : Window
    {
        public managerPage()
        {
            InitializeComponent();
            manager.MainFrame = MainFrame;
            manager.MainFrame.Navigate(new wellcome());
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void audio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new audioDeviceManager());
        }

        private void video_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new videoDeviceManager());
        }

        private void light_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new ligthDeviceManager());
        }
    }
}
