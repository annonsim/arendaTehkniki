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
    /// Логика взаимодействия для usersPage.xaml
    /// </summary>
    public partial class usersPage : Page
    {

        public usersPage()
        {
            InitializeComponent();

            usersList.ItemsSource = arendaDipEntities.GetContext().users.ToList();
        }

        private void editUsers_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new uesrsEdit((sender as Button).DataContext as users));
        }

        private void addNew_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new usersAdd());
        }

        private void dellete_Click(object sender, RoutedEventArgs e)
        {
            var selectUser = usersList.SelectedItems.Cast<users>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить этого пользователя", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                arendaDipEntities.GetContext().users.RemoveRange(selectUser);
                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь удалён!");
                arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                usersList.ItemsSource = arendaDipEntities.GetContext().users.ToList();
            }
        }
    }
}
