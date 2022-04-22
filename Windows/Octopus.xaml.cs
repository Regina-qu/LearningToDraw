using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Рисовалка.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Octopus : Window
    {
        DRAWellEntities db = new DRAWellEntities();
        Users users = new Users();
        public Octopus()
        {
            InitializeComponent();
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Height = 8;
            InkCanvas.DefaultDrawingAttributes.Width = 8;
            showImage(imgs[0], 0);
            Number.Content = (selected + 1) + "/" + (imgs.Length);
        }

        string[] imgs = new string[] {
            "/Resources/Octopus/Octopus1.png",
            "/Resources/Octopus/Octopus2.png",
            "/Resources/Octopus/Octopus3.png",
            "/Resources/Octopus/Octopus4.png",
            "/Resources/Octopus/Octopus5.png"};
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
            if (selected - 1 < 0)
            {
                return;
            }
            else if (selected == 0)
            {
                selected = imgs.Length - 1;
                showImage(imgs[selected], selected);
            }
            else
            {
                selected = selected - 1;
                showImage(imgs[selected], selected);
            }
            Number.Content = (selected + 1) + "/" + (imgs.Length);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (selected + 1 == imgs.Length)
            {
                return;
            }
            selected = selected + 1;
            showImage(imgs[selected], selected);
            Number.Content = (selected + 1) + "/" + (imgs.Length);
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            this.InkCanvas.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.Background = Brushes.White;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "savedimage";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image (.jpg)|*.jpg";
            dlg.ShowDialog();
            var rtb = new RenderTargetBitmap((int)this.InkCanvas.ActualWidth, (int)this.InkCanvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            
            InkCanvas.Measure(new Size((int)this.InkCanvas.ActualWidth, (int)this.InkCanvas.ActualHeight));
            InkCanvas.Arrange(new Rect(new Size((int)this.InkCanvas.ActualWidth, (int)this.InkCanvas.ActualHeight)));
            rtb.Render(InkCanvas);

            PngBitmapEncoder BufferSave = new PngBitmapEncoder();
            BufferSave.Frames.Add((BitmapFrame.Create(rtb)));
            using (var fs = File.OpenWrite(dlg.FileName))
            {
                BufferSave.Save(fs);
            }
            InkCanvas.Background = null;



            //foreach (var user in db.Users)
            //{
            //    if (user.Login == Global.log)
            //    {
                    
            //    }
            //}
        }

        private void Size_MouseLeave(object sender, MouseEventArgs e)
        {
            InkCanvas.DefaultDrawingAttributes.Height = Size.Value;
            InkCanvas.DefaultDrawingAttributes.Width = Size.Value;
        }

        private void RedColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(255, 0, 0);
        }

        private void OrangeColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(255, 165, 0);
        }

        private void YellowColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(255, 255, 0);
        }

        private void GreenColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(0, 255, 0);
        }

        private void BlueColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Color = Color.FromRgb(0, 0, 255);
        }

        private void ChooseColor_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InkCanvas.DefaultDrawingAttributes.Color = System.Windows.Media.Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                ChooseColor.Background = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            Hide();
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
    }
}
