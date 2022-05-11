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
        DRAWellEntities db = new DRAWellEntities();
        UserImages userImages = new UserImages();
        public Teacher()
        {
            InitializeComponent();
            DrawList.ItemsSource = db.UserImages.ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //object tag = ((Button)e.OriginalSource).Tag;
            //MessageBox.Show((string)tag);
        }

        private void Mark1_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите поставить 1 балл?", "Баллы", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                userImages = (sender as Button).DataContext as UserImages;
                userImages.Mark = 1;
                userImages.Status = "Оценено";
                db.SaveChanges();
                DrawList.Items.Refresh();
            }
        }

        private void Mark2_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите поставить 2 балла?", "Баллы", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                userImages = (sender as Button).DataContext as UserImages;
                userImages.Mark = 2;
                userImages.Status = "Оценено";
                db.SaveChanges();
                DrawList.Items.Refresh();
            }
        }

        private void Mark3_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите поставить 3 балла?", "Баллы", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                userImages = (sender as Button).DataContext as UserImages;
                userImages.Mark = 3;
                userImages.Status = "Оценено";
                db.SaveChanges();
                DrawList.Items.Refresh();
            }
        }

        private void Mark4_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите поставить 4 балла?", "Баллы", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                userImages = (sender as Button).DataContext as UserImages;
                userImages.Mark = 4;
                userImages.Status = "Оценено";
                db.SaveChanges();
                DrawList.Items.Refresh();
            }
        }

        private void Mark5_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите поставить 5 баллов?", "Баллы", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                userImages = (sender as Button).DataContext as UserImages;
                userImages.Mark = 5;
                userImages.Status = "Оценено";
                db.SaveChanges();
                DrawList.Items.Refresh();
            }
        }
    }
}
