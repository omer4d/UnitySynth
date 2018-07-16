[System.Serializable]
public class AudioModuleInput {
    public AudioModule module;
    public double defaultValue = 0;
    public double preOffset = 0;
    public double postOffset = 0;
    public double scale = 1;

    public AudioModuleInput()
    {
    }

    public AudioModuleInput(double defaultValue)
    {
        this.defaultValue = defaultValue;
    }

    public double ReadSample(long tick, double time, double dt)
    {
        return module != null ? (preOffset + module.ReadSample(tick, time, dt)) * scale + postOffset : defaultValue;
    }
}
