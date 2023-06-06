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
    /// Логика взаимодействия для ligthDeviceManager.xaml
    /// </summary>
    public partial class ligthDeviceManager : Page
    {
        public ligthDeviceManager()
        {
            InitializeComponent();
            ligthDeviceList.ItemsSource = arendaDipEntities.GetContext().lightDevice.ToList();
            
        }

        private void cheakActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateLigth();
        }

        private void searchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateLigth();
        }

        private void editAudio_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new editLigthDevice((sender as Button).DataContext as lightDevice));
        }

        private void UpdateLigth()
        {
            var _curen = arendaDipEntities.GetContext().lightDevice.ToList();

            _curen = _curen.Where(p => p.name.ToLower().Contains(searchName.Text.ToLower())).ToList();

            if (cheakActual.IsChecked.Value)
                _curen = _curen.Where(p => p.status == 1).ToList();

            ligthDeviceList.ItemsSource = _curen;
        }
    }
}
