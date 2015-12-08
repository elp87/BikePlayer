using System;

namespace elp87.VeloAudio
{
    public interface IAudioFile
    {
        void Play();
        void Pause();
        void Stop();

        string Artist { get; }
        string Album { get; }
        string Year { get; }
        string Title { get; }

        TimeSpan Length { get; }
        TimeSpan CurrentTime { get; }

        event EventHandler<StopEventArgs> Stopped;
    }
}
