using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;
using System.Windows;
using elp87.VeloAudio;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Timer = System.Timers.Timer;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace BikePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Mp3File _mp3;
        private readonly ObservableCollection<Mp3File> _mp3List;

        public MainWindow()
        {
            InitializeComponent();
            _mp3List = new ObservableCollection<Mp3File>();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog
            {
                DefaultExt = ".mp3",
                Filter = "файл портфеля (*.mp3)|*.mp3"
            };
            bool? result = ofDialog.ShowDialog();
            if (result == null || !result.Value) return;
            if (_mp3 != null) _mp3.Stop();
            _mp3 = new Mp3File(ofDialog.FileName);
            _mp3.Play();
            
            Timer timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;

            TimeSlider.Maximum = _mp3.Length.TotalSeconds;
            _mp3.Stopped += _mp3_Stopped;
        }

        private void _mp3_Stopped(object sender, StopEventArgs e)
        {
            
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                TimeSlider.Value = _mp3.CurrentTime.TotalSeconds;
                string timeString = _mp3.CurrentTime.ToString(@"mm\:ss");
                ContentLabel.Content = _mp3.Artist + " - " + _mp3.Title + " (" + timeString + ")";
            }));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mp3.Stop();
        }

        private void TimeSlider_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _mp3.CurrentTime = new TimeSpan(0, 0, (int)TimeSlider.Value);
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] filenames = Directory.GetFiles(folderDialog.SelectedPath, "*.mp3");
                foreach (string filename in filenames)
                {
                    Mp3File newMp3 = new Mp3File(filename);
                    _mp3List.Add(newMp3);
                }
                PlaylistListBox.ItemsSource = _mp3List;
            }
        }
    }
}
