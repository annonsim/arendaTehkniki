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
    /// Логика взаимодействия для audioAdmin.xaml
    /// </summary>
    public partial class audioAdmin : Page
    {
        public audioAdmin()
        {
            InitializeComponent();
            audioDeviceList.ItemsSource = arendaDipEntities.GetContext().audioDevice.ToList();
        }

        private void UpdateAudio()
        {
            var _curent = arendaDipEntities.GetContext().audioDevice.ToList();

            _curent = _curent.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();

            if (cheakActual.IsChecked.Value)
                _curent = _curent.Where(p => p.status == 1).ToList();

            audioDeviceList.ItemsSource = _curent;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new addNewAudioDevice());
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var select = audioDeviceList.SelectedItems.Cast<audioDevice>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {select.Count()} элемента?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                arendaDipEntities.GetContext().audioDevice.RemoveRange(select);
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены!");
                arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                audioDeviceList.ItemsSource = arendaDipEntities.GetContext().audioDevice.ToList();
            }
        }

        private void searchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAudio();
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAudio();
        }

        private void cheakActual_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateAudio();
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new adminEditAudio((sender as Button).DataContext as audioDevice));
        }
    }
}