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

namespace Рисовалка.Windows
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        private DRAWellEntities _context;
        public Reg(DRAWellEntities context)
        {
            InitializeComponent();
            this._context = context;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && Password.Text != "" && Name.Text != "")
            {
                Users users = null;
                using (DRAWellEntities _context = new DRAWellEntities())
                {
                    users = _context.Users.Where(user => user.Login == Login.Text && user.Pass == Password.Text && user.Name == Name.Text).FirstOrDefault();
                }
                if (users != null) MessageBox.Show(this, "Пользователь с таким логином уже существует!", "Ошибка");
                else
                {
                    _context.Users.Add(new Users { Login = Login.Text, Pass = Password.Text, Name = Name.Text });
                    _context.SaveChanges();
                    Global.log = Login.Text;
                    MessageBox.Show("Вы зарегистрированы");
                    Home home = new Home();
                    home.Show();
                    Hide();
                }
            }
            else MessageBox.Show(this, "Заполните поля!", "Ошибка!");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            Close();
        }
    }
}
