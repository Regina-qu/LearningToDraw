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
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        DRAWellEntities db = new DRAWellEntities();
        UserImages userImages = new UserImages();
        public Teacher()
        {
            InitializeComponent();
            DrawList.ItemsSource = db.UserImages.ToList(); CbFilter.Items.Add("Статус");
            CbFilter.Items.Add("Оценено");
            CbFilter.Items.Add("Ждёт оценку");
            CbFilter.SelectedIndex = 0;

            CbSort.Items.Add("Дата выполнения");
            CbSort.Items.Add("Сначала новые");
            CbSort.Items.Add("Сначала старые");
            CbSort.SelectedIndex = 0;

            GetImages();
        }

        private void GetImages()
        {

            List<UserImages> list = db.UserImages.ToList();
            switch (CbSort.SelectedIndex)
            {
                case 0:; break;
                case 1: list = list.OrderByDescending(x => x.Created).ToList(); break;
                case 2: list = list.OrderBy(x => x.Created).ToList(); break;
            }

            switch (CbFilter.SelectedIndex)
            {
                case 0:; break;
                case 1: list = list.Where(x => x.Status == "Оценено").ToList(); break;
                case 2: list = list.Where(x => x.Status == "Ждёт оценку").ToList(); break;
            }

            DrawList.ItemsSource = list;
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

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetImages();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetImages();
        }
    }
}
