using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using elp87.VeloAudio;
using Timer = System.Timers.Timer;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace BikePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Fields
        private Mp3File _mp3;
        private readonly ObservableCollection<Mp3File> _mp3List;

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            _mp3List = new ObservableCollection<Mp3File>();
            VolumeLevelSlider.DataContext = new VolumeInfo();
        } 
        #endregion

        #region Event handlers
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mp3 != null) _mp3.Stop();
            _mp3 = PlaylistListBox.SelectedItem as Mp3File;
            if (_mp3 == null) return;
            PlayTrack();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _mp3.Stop();
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

        private void PlaylistListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayButton_Click(sender, new RoutedEventArgs());
        }

        private void TimeSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _mp3.CurrentTime = new TimeSpan(0, 0, (int)TimeSlider.Value);
        }

        private void _mp3_Stopped(object sender, StopEventArgs e)
        {
            if (e.StopCase != StopEventArgs.StopCases.Finished) return;
            Mp3File nextFile = _mp3List.ElementAtOrDefault(_mp3List.IndexOf(_mp3) + 1);
            if (nextFile != null)
            {
                _mp3 = nextFile;
                PlaylistListBox.SelectedItem = _mp3;
                PlayTrack();
            }
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
        #endregion

        #region Methods
        private void PlayTrack()
        {
            _mp3.Play();

            Timer timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;

            TimeSlider.Maximum = _mp3.Length.TotalSeconds;
            _mp3.Stopped += _mp3_Stopped;
        } 
        #endregion
    }
}
