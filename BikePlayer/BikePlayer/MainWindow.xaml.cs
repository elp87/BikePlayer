using System;
using System.Timers;
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
        private Mp3File _mp3;
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
