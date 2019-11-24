using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using IronBarCode;

namespace DataMatrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextInput_Loaded(object sender, RoutedEventArgs e)
        {
            DoMyJob();
        }
        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            DoMyJob();
        }

        private void DoMyJob()
        {
            if (ErrorLabel != null)
            {
                ErrorLabel.Content = null;
            }

            string text = new TextRange(TextInput.Document.ContentStart, TextInput.Document.ContentEnd).Text.Trim();

            if (!string.IsNullOrEmpty(text) && Image1 != null)
            {
                try
                {
                    Image1.Source = null;

                    GeneratedBarcode myBarcode = BarcodeWriter.CreateBarcode(text, BarcodeWriterEncoding.DataMatrix);
                    Image1.Source = Convert(myBarcode.ToBitmap());
                }
                catch (Exception e)
                {
                    if (ErrorLabel != null)
                        ErrorLabel.Content = e.Message;
                }

            }
            else
            {
                if (text == "" && Image1 != null)
                {
                    Image1.Source = null;
                }
            }

        }

        private static BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp); // What is happening here?
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
