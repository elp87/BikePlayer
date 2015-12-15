namespace elp87.VeloAudio
{
    public class VolumeInfo
    {
        public VolumeInfo()
        {
            Value = 0.5f;
        }

        public float MinValue { get { return 0; } }
        public float MaxValue { get { return 1; } }
        public float Value { get; set; }
    }
}
