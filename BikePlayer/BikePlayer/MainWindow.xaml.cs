using System.Windows;
using elp87.VeloAudio;
using Microsoft.Win32;

namespace BikePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        Mp3File _mp3;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog
            {
                DefaultExt = ".mp3",
                Filter = "файл портфеля (*.mp3)|*.mp3"
            };
            bool? result = ofDialog.ShowDialog();
            if (result == null || !result.Value) return;
            _mp3 = new Mp3File(ofDialog.FileName);
            _mp3.Play();
            ContentLabel.Content = _mp3.Artist + " - " + _mp3.Title;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mp3.Stop();
        }
    }
}
