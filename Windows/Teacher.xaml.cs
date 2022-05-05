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
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        DRAWellEntities _context = new DRAWellEntities();
        public Teacher()
        {
            InitializeComponent();
            DrawList.ItemsSource = _context.UserImage.ToList();
        }

        private void Mark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
