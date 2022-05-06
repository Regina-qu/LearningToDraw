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


namespace Рисовалка.Windows
{
    /// <summary>
    /// Логика взаимодействия для Drawing.xaml
    /// </summary>
    public partial class Drawing : Window
    {
        DRAWellEntities db = new DRAWellEntities();
        Users users = new Users();
        UserImage userImage = new UserImage();
        private string[] imgs;
        int selected;

        public Drawing()
        {
            InitializeComponent();
            if (Global.ImageNumber == 0)
            {
                imgs = new string[] {
            "/Resources/Octopus/Octopus1.png",
            "/Resources/Octopus/Octopus2.png",
            "/Resources/Octopus/Octopus3.png",
            "/Resources/Octopus/Octopus4.png",
            "/Resources/Octopus/Octopus5.png"};
            }
            else if (Global.ImageNumber == 1)
            {
                imgs = new string[] {
            "/Resources/Cherry/Cherry1.png",
            "/Resources/Cherry/Cherry2.png",
            "/Resources/Cherry/Cherry3.png",
            "/Resources/Cherry/Cherry4.png",
            "/Resources/Cherry/Cherry5.png"};
            }
            else if (Global.ImageNumber == 2)
            {
                imgs = new string[] {
            "/Resources/Cat/Cat1.png",
            "/Resources/Cat/Cat2.png",
            "/Resources/Cat/Cat3.png",
            "/Resources/Cat/Cat4.png",
            "/Resources/Cat/Cat5.png",
            "/Resources/Cat/Cat6.png",
            "/Resources/Cat/Cat7.png"};
            }
            else if (Global.ImageNumber == 3)
            {
                imgs = new string[] {
            "/Resources/Snail/Snail1.png",
            "/Resources/Snail/Snail2.png",
            "/Resources/Snail/Snail3.png",
            "/Resources/Snail/Snail4.png"};
            }
            else if (Global.ImageNumber == 4)
            {
                imgs = new string[] {
            "/Resources/Bear/Bear1.png",
            "/Resources/Bear/Bear2.png",
            "/Resources/Bear/Bear3.png",
            "/Resources/Bear/Bear4.png",
            "/Resources/Bear/Bear5.png"};
            }
            else if (Global.ImageNumber == 5)
            {
                imgs = new string[] {
            "/Resources/Rabbit/Rabbit1.png",
            "/Resources/Rabbit/Rabbit2.png",
            "/Resources/Rabbit/Rabbit3.png",
            "/Resources/Rabbit/Rabbit4.png",
            "/Resources/Rabbit/Rabbit5.png"};
            }
            else if (Global.ImageNumber == 6)
            {
                imgs = new string[] {
            "/Resources/Turtle/Turtle1.png",
            "/Resources/Turtle/Turtle2.png",
            "/Resources/Turtle/Turtle3.png",
            "/Resources/Turtle/Turtle4.png",
            "/Resources/Turtle/Turtle5.png"};
            }

            selected = 0;
            InkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            InkCanvas.DefaultDrawingAttributes.Height = 8;
            InkCanvas.DefaultDrawingAttributes.Width = 8;
            showImage(imgs[0], 0);
            Number.Content = (selected + 1) + "/" + (imgs.Length);
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

            if(selected == imgs.Length - 1)
            {
                WithoutLearn.Visibility = Visibility.Visible;
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            this.InkCanvas.Strokes.Clear();
        }

        int ImageUserID;
        string ImageTitle;
        string ImagePath;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (var user in db.Users)
            {
                if (user.Login == Global.log)
                {
                    InkCanvas.Background = Brushes.White;
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "picture.png";
                    dlg.DefaultExt = ".png";
                    dlg.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG";
                    
                    bool? result = dlg.ShowDialog();

                    string UserName = user.Name;
                    string filename = dlg.FileName;

                    ImageTitle = UserName + "_" + System.IO.Path.GetFileName(filename);
                    ImagePath = "/Resources/UserImage/" + UserName + "_" + System.IO.Path.GetFileName(filename);

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
                    try
                    {
                        File.Copy(filename, $"{projectDirectory}/Рисовалка/Resources/UserImage/{ImageTitle}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                        return;
                    }
                    InkCanvas.Background = null;

                    ImageUserID = user.ID;
                    //userImage.Mark = null;
                }
            }
            db.UserImage.Add(new UserImage()
            {
                UserID = ImageUserID,
                Title = ImageTitle,
                Path = ImagePath,
                Created = DateTime.Now.Date,
                Status = "Ждёт оценку"
            });
            db.SaveChanges();
        }

        public static string workingDirectory = Environment.CurrentDirectory;
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        //private void Sending_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
        //    {
        //        DefaultExt = ".png",
        //        FileName = "picture.png",
        //        Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG",
        //        InitialDirectory = projectDirectory + @"\Popryzhenok_Subbotina\agents\"
        //    };
        //    bool? result = openFileDialog.ShowDialog();

        //    if (result == true)
        //    {
        //        string filename = openFileDialog.FileName;
        //        string u = "/UserImage/Image" + System.IO.Path.GetFileName(filename);
        //        //userImage.Path = "/Resources/UserImage" + System.IO.Path.GetFileName(filename);
        //        try
        //        {
        //            File.Copy(filename, $"{projectDirectory}/Рисовалка/Resources{u}");
        //            // TODO: RESCAN AGENTS FOLDER
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message.ToString());
        //            return;
        //        }
        //    }
        //}

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

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            WithoutLearning withoutLearning = new WithoutLearning();
            withoutLearning.Show();
            Hide();
        }

        

        //UserImage userImage;
        //private void Sending_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.InitialDirectory = "c:\\";
        //    dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
        //    dlg.RestoreDirectory = true;

        //    string appPath = Directory.GetCurrentDirectory();
        //    string imageFolder = Directory.GetParent(appPath).Parent.FullName + "\\Images\\";
        //    if (Directory.Exists(imageFolder) == false)
        //    {
        //        Directory.CreateDirectory(imageFolder);
        //    }
        //    Nullable<bool> result = dlg.ShowDialog();
        //    if (result == true)
        //    {
        //        string selectedFileName = dlg.FileName;
        //        string[] iName = dlg.SafeFileName.Split('.');
        //        string extension = GetExtension(selectedFileName);
        //        if (IsImageExtension(extension))
        //        {
        //            string newPath = imageFolder + iName[0];
        //            CropImage(selectedFileName, newPath);
        //            userImage.Path = newPath + "." + iName[1];
        //            userImage.Data = ImageService.LoadImage(newPath + "." + iName[1]);
        //            using (var context = new DRAWellEntities())
        //            {
        //                try
        //                {
        //                    var dbImages = context.UserImage.Where(i => i.UserId == user.Id);
        //                    foreach (var image in dbImages)
        //                    {
        //                        images.Add(new ImageModel
        //                        {
        //                            Id = image.Id,
        //                            Created = image.Created,
        //                            Description = image.Description,
        //                            Path = image.Path,
        //                            Title = image.Title,
        //                            UserId = image.UserId,
        //                            Data = LoadImage(image.Path),
        //                        });
        //                    }
        //                }
        //                catch (Exception e)
        //                {
        //                    Trace.WriteLine(e.Message);
        //                }

        //                return images;
        //            }
        //        }
        //    }
        //}

        //private string GetExtension(string path)
        //{
        //    return path.Split('.').Last();
        //}
        //private bool IsImageExtension(string ext)
        //{
        //    string[] _validExtensions = { "jpg", "bmp", "gif", "png" };
        //    return _validExtensions.Contains(ext.ToLower());
        //}
        //private void CropImage(string source, string dest)
        //{
        //    ImageBuilder.Current.Build(new ImageJob(source, dest, new Instructions("width=274;height=143;format=jpg;"), false, true));
        //}
    }
}
