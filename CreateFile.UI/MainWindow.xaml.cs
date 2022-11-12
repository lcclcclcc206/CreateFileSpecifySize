using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace CreateFile.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private async void CreateFileAsync(object sender, RoutedEventArgs e)
        {
            string path = PathTextBox.Text;
            if (string.IsNullOrWhiteSpace(path))
                path = ".";
            if(!Directory.Exists(path))
            {
                InfoTextBox.Foreground = Brushes.Red;
                InfoTextBox.Text = "The path is not exist!";
                return;
            }

            string fileName = FileNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(fileName) || (fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0))
            {
                InfoTextBox.Foreground = Brushes.Red;
                InfoTextBox.Text = "The file name is incorrect!";
                return;
            }

            int size;
            if (!int.TryParse(SizeTextBox.Text, out size))
            {
                InfoTextBox.Foreground = Brushes.Red;
                InfoTextBox.Text = "The size value is incorrect!";
                return;
            }

            Unit unit;
            string unitStr = UnitComboBox.Text;
            unit = (Unit)Enum.Parse(typeof(Unit), unitStr);

            string result = await CreateFileHelper.CreateFileAsync(new FileInfo(Path.Combine(path,fileName)), size, unit);
            InfoTextBox.Foreground = Brushes.Green;
            InfoTextBox.Text = result;
        }
    }
}
