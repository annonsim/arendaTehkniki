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
    /// Логика взаимодействия для lightAdmin.xaml
    /// </summary>
    public partial class lightAdmin : Page
    {
        public lightAdmin()
        {
            InitializeComponent();
            lightDeviceList.ItemsSource = arendaDipEntities.GetContext().lightDevice.ToList();
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateVideo();
        }

        private void cheakActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateVideo();
        }

        private void searchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateVideo();
        }

        private void UpdateVideo()
        {
            var _update = arendaDipEntities.GetContext().lightDevice.ToList();
            _update = _update.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();
            if (cheakActual.IsChecked.Value)
                _update = _update.Where(p => p.status == 1).ToList();
            lightDeviceList.ItemsSource = _update;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var selectDevice = lightDeviceList.SelectedItems.Cast<lightDevice>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {selectDevice.Count} элементов?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                arendaDipEntities.GetContext().lightDevice.RemoveRange(selectDevice);
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");
                arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                lightDeviceList.ItemsSource = arendaDipEntities.GetContext().lightDevice.ToList();

            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new addNewLightDevice());
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new adminEditLight((sender as Button).DataContext as lightDevice));
        }
    }
}
