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
    /// Логика взаимодействия для Gallery.xaml
    /// </summary>
    public partial class Gallery : Window
    {
        DRAWellEntities _context = new DRAWellEntities();
        public Gallery()
        {
            InitializeComponent();
            foreach (var user in _context.Users)
            {
                if (user.Login == Global.log)
                {
                    DrawList.ItemsSource = _context.UserImages.Where(b => b.UserID == user.ID).ToList();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            Hide();
        }
    }
}
