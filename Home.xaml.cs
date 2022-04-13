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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private DRAWellEntities _context;

        public Home()
        {
            InitializeComponent();
            showImage(imgs[0], 0);
        }

        string[] imgs = new string[] {
            "/Resources/Octopus/Octopus5.png",
            "/Resources/Cherry/Cherry5.png",
            "/Resources/Cat/Cat7.png"};
        private int selected = 0;

        private void showImage(string img, int i)
        {
            BitmapImage b1 = new BitmapImage();
            b1.BeginInit();
            b1.UriSource = new Uri(img, UriKind.Relative);
            b1.EndInit();
            Image.Stretch = Stretch.Fill;
            Image.Source = b1;
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (selected == 0)
            {
                selected = imgs.Length - 1;
                showImage(imgs[selected], selected);
            }
            else
            {
                selected = selected - 1; 
                showImage(imgs[selected], selected);
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (selected == imgs.Length - 1)
            {
                selected = 0;
                showImage(imgs[selected], selected);
            }
            else
            {
                selected = selected + 1; 
                showImage(imgs[selected], selected);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selected == 0)
            {
                Octopus octopus = new Octopus();
                octopus.Show();
                Hide();
            }
            else if (selected == 1)
            {
                Cherry cherry = new Cherry();
                cherry.Show();
                Hide();
            }
            else if (selected == 2)
            {
                Cat cat = new Cat();
                cat.Show();
                Hide();
            }
        }

        //private void Gallery_Click(object sender, RoutedEventArgs e)
        //{
        //    Gallery gallery = new Gallery();
        //    gallery.Show();
        //    Hide();
        //}

        //private void Login_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Login.Content = "Добро пожаловать, " + Global.log;
        //}
    }
}
