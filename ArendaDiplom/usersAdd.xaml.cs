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
    /// Логика взаимодействия для usersAdd.xaml
    /// </summary>
    public partial class usersAdd : Page
    {
        private users _current = new users();
        public usersAdd()
        {
            InitializeComponent();

            DataContext = _current;    
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new usersPage());
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_current.login))
                error.AppendLine("Введите логин!");

            if (string.IsNullOrWhiteSpace(_current.password))
                error.AppendLine("Введите пароль!");

            if (string.IsNullOrWhiteSpace(_current.isAdmin.ToString()))
                error.AppendLine("Укажите роль пользователя!");

            if (error.Length > 0)
                MessageBox.Show(error.ToString());

            if (error.Length == 0)
            {
                arendaDipEntities.GetContext().users.Add(_current);

                arendaDipEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь добавлен!");
                manager.MainFrame.Navigate(new usersPage());
                //arendaDipEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                arendaDipEntities.GetContext().users.ToList();
            }

        }
    }
}
