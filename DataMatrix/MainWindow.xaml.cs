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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                Image1.Source = null;

                try
                {
                    GeneratedBarcode myBarcode = BarcodeWriter.CreateBarcode(text, BarcodeWriterEncoding.DataMatrix);
                    IntPtr hBitmap = myBarcode.ToBitmap().GetHbitmap();
                    ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                    Image1.Source = wpfBitmap;
                }
                catch (Exception e)
                {
                    if (ErrorLabel != null)
                        ErrorLabel.Content = e.Message.ToString();
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
    }
}
