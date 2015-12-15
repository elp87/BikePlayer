using System;
using System.Drawing;

namespace elp87.VeloAudio
{
    public interface IAudioFile
    {
        void Play();
        void Pause();
        void Stop();

        void SetVolumeLevel(float volume);

        string Filename { get; }

        string Artist { get; }
        string Album { get; }
        string Year { get; }
        string Title { get; }

        Image CoverArt { get; }

        TimeSpan Length { get; }
        TimeSpan CurrentTime { get; set; }

        event EventHandler<StopEventArgs> Stopped;
    }
}
