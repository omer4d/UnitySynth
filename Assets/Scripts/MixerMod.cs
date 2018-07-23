using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerMod : AudioModule {
    public AudioModule[] inputs;

    public override double NextSample(long tick, double time, double dt)
    {
        double sum = 0.0;

        foreach(var input in inputs)
        {
            sum += input.ReadSample(tick, time, dt);
        }

        return sum;
    }
}
