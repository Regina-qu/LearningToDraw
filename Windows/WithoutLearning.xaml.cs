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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Рисовалка.Model;

namespace Рисовалка.Windows
{
    /// <summary>
    /// Логика взаимодействия для WithoutLearning.xaml
    /// </summary>
    public partial class WithoutLearning : Window
    {
        DRAWellEntities db = new DRAWellEntities();
        Users users = new Users();
        private string[] imgs;

        public WithoutLearning()
        {
            InitializeComponent();
            if (Global.ImageNumber == 0)
            {
                imgs = new string[] {"/Resources/Octopus/Octopus5.png"};
            }
            else if (Global.ImageNumber == 1)
            {
                imgs = new string[] {"/Resources/Cherry/Cherry5.png"};
            }
            else if (Global.ImageNumber == 2)
            {
                imgs = new string[] {"/Resources/Cat/Cat7.png"};
            }
            else if (Global.ImageNumber == 3)
            {
                imgs = new string[] { "/Resources/Snail/Snail4.png" };
            }
            else if (Global.ImageNumber == 4)
            {
                imgs = new string[] { "/Resources/Bear/Bear5.png" };
            }
            else if (Global.ImageNumber == 5)
            {
                imgs = new string[] { "/Resources/Rabbit/Rabbit5.png" };
            }
            else if (Global.ImageNumber == 6)
            {
                imgs = new string[] { "/Resources/Turtle/Turtle5.png" };
            }
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Height = 8;
            InkCanvas.DefaultDrawingAttributes.Width = 8;
            showImage(imgs[0], 0);
        }

        private void showImage(string img, int i)
        {
            BitmapImage b1 = new BitmapImage();
            b1.BeginInit();
            b1.UriSource = new Uri(img, UriKind.Relative);
            b1.EndInit();
            Image.Stretch = Stretch.Fill;
            Image.Source = b1;
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            this.InkCanvas.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas.Background = Brushes.White;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "picture.png";
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
            dlg.ShowDialog();

            var rtb = new RenderTargetBitmap((int)this.InkCanvas.ActualWidth, (int)this.InkCanvas.ActualHeight, 95, 97, PixelFormats.Pbgra32);
            rtb.Render(InkCanvas);

            PngBitmapEncoder BufferSave = new PngBitmapEncoder();
            BufferSave.Frames.Add((BitmapFrame.Create(rtb)));
            using (var fs = File.OpenWrite(dlg.FileName))
            {
                BufferSave.Save(fs);
            }
            InkCanvas.Background = null;
        }

        int ImageUserID;
        private void Sending_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Хотите отправить работу на проверку?", "Отправка на проверку", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.OK)
            {
                foreach (var user in db.Users)
                {
                    if (user.Login == Global.log)
                    {
                        ImageUserID = user.ID;
                    }
                }
                try
                {
                    MemoryStream memory = new MemoryStream();

                    var rtb = new RenderTargetBitmap((int)this.InkCanvas.ActualWidth, (int)this.InkCanvas.ActualHeight, 95d, 97d, PixelFormats.Pbgra32);
                    rtb.Render(InkCanvas);

                    BitmapEncoder pngEncoder = new PngBitmapEncoder();
                    pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                    using (var fs = memory)
                    {
                        pngEncoder.Save(fs);
                    }

                    byte[] arr = memory.ToArray();
                    db.UserImages.Add(new UserImages()
                    {
                        UserID = ImageUserID,
                        Image = arr,
                        Created = DateTime.Now.Date,
                        Status = "Ждёт оценку"
                    });
                    db.SaveChanges();
                    MessageBox.Show("Ваше изображение отправлено");
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка");
                }
            }
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Drawing drawing = new Drawing();
            drawing.Show();
            Hide();
        }

        private void Backwards_Click(object sender, RoutedEventArgs e)
        {
            Drawing drawing = new Drawing();
            drawing.Show();
            Hide();
        }
    }
}
