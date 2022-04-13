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
    /// Логика взаимодействия для Gallery.xaml
    /// </summary>
    public partial class Gallery : Window
    {
        public Gallery()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            Hide();
        }
    }
}
