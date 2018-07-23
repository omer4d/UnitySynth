using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowpassMod : AudioModule {
    public AudioModuleInput source = new AudioModuleInput(0);
    public AudioModuleInput cutoffFrequency = new AudioModuleInput(400);
    public AudioModuleInput resonance = new AudioModuleInput(0);

    private ButterworthLowpassFilter filter;

    public void Awake()
    {
        filter = new ButterworthLowpassFilter(AudioSettings.GetConfiguration().sampleRate, 400, 0);
    }

    public override double NextSample(long tick, double time, double dt)
    {
        filter.SetParams(cutoffFrequency.ReadSample(tick, time, dt), resonance.ReadSample(tick, time, dt));
        return filter.Run(source.ReadSample(tick, time, dt));
    }
}
