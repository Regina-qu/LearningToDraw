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

            CbFilter.Items.Add("Статус");
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
            foreach (var user in _context.Users)
            {
                if (user.Login == Global.log)
                {
                    DrawList.ItemsSource = _context.UserImages.Where(b => b.UserID == user.ID).ToList();

                    List<UserImages> list = user.UserImages.ToList();
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
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            Hide();
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
