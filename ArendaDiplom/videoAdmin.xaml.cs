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
    /// Логика взаимодействия для videoAdmin.xaml
    /// </summary>
    public partial class videoAdmin : Page
    {
        
        public videoAdmin()
        {
            InitializeComponent();
            videoDeviceList.ItemsSource = arendaDipEntities.GetContext().videosDevice.ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new addNewVideoDevice());
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var selectVideo = videoDeviceList.SelectedItems.Cast<videosDevice>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {selectVideo.Count()} элементов?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                arendaDipEntities.GetContext().videosDevice.RemoveRange(selectVideo);
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");
                arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                videoDeviceList.ItemsSource = arendaDipEntities.GetContext().videosDevice.ToList();
            }
        }

        private void searchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVideo();
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateVideo();
        }

        private void cheakActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateVideo();
        }

        private void UpdateVideo()
        {
            var _currentVideo = arendaDipEntities.GetContext().videosDevice.ToList();

            _currentVideo = _currentVideo.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();

            if (cheakActual.IsChecked.Value)
                _currentVideo = _currentVideo.Where(p => p.status == 1).ToList();

            videoDeviceList.ItemsSource = _currentVideo;
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new adminEditVideo((sender as Button).DataContext as videosDevice));
        }
    }
}
