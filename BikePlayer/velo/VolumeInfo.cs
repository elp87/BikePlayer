namespace elp87.VeloAudio
{
    public class VolumeInfo
    {
        public VolumeInfo()
        {
            Value = 50;
        }

        public int MinValue { get { return 0; } }
        public int MaxValue { get { return 100; } }
        public int Value { get; set; }
    }
}
