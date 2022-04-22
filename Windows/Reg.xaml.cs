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
            if (Login.Text != "" || Password.Text != "" || Name.Text != "")
            {
                Users users = null;
                using (DRAWellEntities _context = new DRAWellEntities())
                {
                    users = _context.Users.Where(user => user.Login == Login.Text).FirstOrDefault();
                }
                if (users != null) MessageBox.Show(this, "Пользователь с таким логином уже существует!", "Ошибка");
                else
                {
                    _context.Users.Add(new Users { Login = Login.Text, Pass = Password.Text, Name = Name.Text });
                    _context.SaveChanges();
                    //Global.log = Login.Text;
                    MessageBox.Show("Вы зарегистрированы");
                    Home home = new Home();
                    home.Show();
                    Hide();
                }
                
                //if (Password.Text.Length >= 6)
                //{
                //    bool en = true; // английская раскладка
                //    bool symbol = false; // символ
                //    bool number = false; // цифра

                //    for (int i = 0; i < Password.Text.Length; i++) // перебираем символы
                //    {
                //        if (Password.Text[i] >= 'А' && Password.Text[i] <= 'Я') en = false; // если русская раскладка
                //        if (Password.Text[i] >= '0' && Password.Text[i] <= '9') number = true; // если цифры
                //        if (Password.Text[i] == '_' || Password.Text[i] == '-' || Password.Text[i] == '!') symbol = true; // если символ
                //    }

                //    if (!en) MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
                //    else if (!symbol) MessageBox.Show("Добавьте один из следующих символов: _ - !"); // выводим сообщение
                //    else if (!number) MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                //    if (en && symbol && number) // проверяем соответствие
                //    {
                //        _context.Users.Add(new Users { Login = Login.Text, Pass = Password.Text, Name = Name.Text });
                //        _context.SaveChanges();

                //        MessageBox.Show("Вы зарегистрированы");
                //        Home home = new Home();
                //        home.Show();
                //        Hide();
                //    }
                //}
                //else MessageBox.Show("пароль слишком короткий, минимум 6 символов");
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
