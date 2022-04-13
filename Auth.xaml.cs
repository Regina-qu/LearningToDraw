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

namespace Рисовалка
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
            //var user = db.Users.Where(u => u.Login.Equals(login) && u.Pass.Equals(pass));
            //if (user.Count() == 0) return false;

            //userId = user.First().ID;
            //return true;
            if (Login.Text != "" && Password.Text != "")
            {
                Users users = null;
                using (DRAWellEntities _context = new DRAWellEntities())
                {
                    users = _context.Users.Where(user => user.Login == Login.Text && user.Pass == Password.Text).FirstOrDefault();
                }
                if (users != null)
                {
                    //Global.log = Login.Text;
                    Home home = new Home();
                    home.Show();
                    Hide();
                }
                else MessageBox.Show(this, "Вы еще не зарегистрированы!", "Ошибка");
            }
            else MessageBox.Show(this, "Заполните поля!", "Ошибка");
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg(_context);
            reg.Show();
            Close();
        }
    }
}
