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
using Рисовалка.Model;
using Рисовалка.Windows;

namespace Рисовалка.Windows
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        private static DRAWellEntities _context = new DRAWellEntities();
        public Auth()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text != "" && Password.Text != "")
                {
                    if (_context.Users.Where(user => user.Login == Login.Text && user.Pass == Password.Text).Count() > 0)
                    {
                        Global.log = Login.Text;
                        Home home = new Home();
                        home.Show();
                        Hide();
                    }
                    else if (_context.Teacher.Where(user => user.Login == Login.Text && user.Pass == Password.Text).Count() > 0)
                    {
                        Global.log = Login.Text;
                        Teacher teacher = new Teacher();
                        teacher.Show();
                        Hide();
                    }
                    else
                    {
                        Login.Text = "";
                        Password.Text = "";
                        MessageBox.Show("Неверный логин или пароль", "Ошибка");
                    }
                }
                else MessageBox.Show(this, "Заполните поля!", "Ошибка");
            }
            catch { }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg(_context);
            reg.Show();
            Close();
        }
    }
}
