using System;

namespace elp87.VeloAudio
{
    public class StopEventArgs : EventArgs
    {
        public enum StopCases
        {
            UserStoped,
            Finished
        }

        public StopCases StopCase;
    }
}
