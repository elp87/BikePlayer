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
            Mp3File mp3 = new Mp3File(ofDialog.FileName);
            mp3.Play();
        }
    }
}
