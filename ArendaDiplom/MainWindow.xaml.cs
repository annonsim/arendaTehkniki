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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();         
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void inSign_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            using(var db = new arendaDipEntities())
            {
                var pass = db.users.AsNoTracking().FirstOrDefault(u => u.login == inputLogin.Text && u.password == inputPassword.Password);
                var log = db.users.AsNoTracking().FirstOrDefault(u => u.login == inputLogin.Text);

                if (inputLogin.Text.Length == 0)
                    MessageBox.Show("Введите логин!");

                else if(inputPassword.Password.Length == 0)
                    MessageBox.Show("Введите пароль!");

                else
                {
                    if (log == null)
                        MessageBox.Show("Пользователь не найден!");
                    else
                    {
                        if (pass == null)
                            MessageBox.Show("Неверный пароль!");
                        else
                        {
                            if (pass.isAdmin == true)
                            {
                                adminPage admin = new adminPage();
                                admin.Show();
                                Close();
                            }
                            if (pass.isAdmin == false)
                            {
                                managerPage managers = new managerPage();
                                managers.Show();
                                Close();
                            }

                        }
                    }
                }

            }
        }
    }
}
