using System;
using UnityEngine;

public class GeneratorMod : AudioModule {
    public AudioModuleInput frequency = new AudioModuleInput(0);
    public AudioModuleInput amplitude = new AudioModuleInput(1);

    private double localTime = 0;
    private ButterworthLowpassFilter filter;

    public void Awake()
    {
        //Debug.Log(AudioSettings.GetConfiguration().sampleRate);
        //filter = new ButterworthLowpassFilter(AudioSettings.GetConfiguration().sampleRate, 2000, 0);
    }

    public override double NextSample(long tick, double time, double dt)
    {
        double baseFreq = 1;
        localTime += frequency.ReadSample(tick, time, dt) / baseFreq * dt;

        return amplitude.ReadSample(tick, time, dt) * (((localTime * baseFreq) % 1) * 2 - 1);
        //return amplitude.ReadSample(tick, time, dt) * Math.Sin(localTime * baseFreq * 2 * Math.PI);
    }
}
