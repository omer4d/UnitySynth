using System;
using UnityEngine;

public class GeneratorMod : AudioModule {
    public enum Shape
    {
        Saw,
        Sine
    }

    public AudioModuleInput frequency = new AudioModuleInput(0);
    public AudioModuleInput amplitude = new AudioModuleInput(1);
    public AudioModuleInput vibrato = new AudioModuleInput(1);
    public Shape shape = Shape.Sine;

    private double localTime = 0;
    private ButterworthLowpassFilter filter;

    public void Awake()
    {
        //Debug.Log(AudioSettings.GetConfiguration().sampleRate);
        filter = new ButterworthLowpassFilter(AudioSettings.GetConfiguration().sampleRate, 3000, 0);
    }

    public override double NextSample(long tick, double time, double dt)
    {
        double baseFreq = 1;
        localTime += frequency.ReadSample(tick, time, dt) / baseFreq * AudioUtils.RaiseSemitones(vibrato.ReadSample(tick, time, dt)) * dt;

        switch (shape)
        {
            case Shape.Saw:
                return amplitude.ReadSample(tick, time, dt) * (((localTime * baseFreq) % 1) * 2 - 1);
            case Shape.Sine:
                return amplitude.ReadSample(tick, time, dt) * Math.Sin(localTime * baseFreq * 2 * Math.PI);
            default:
                return 0;
        }


        //return amplitude.ReadSample(tick, time, dt) * Math.Sin(localTime * baseFreq * 2 * Math.PI);
    }
}
