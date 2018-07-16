using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMod : AudioModule {
    System.Random rand = new System.Random();

    public override double NextSample(long tick, double time, double dt)
    {
        return rand.NextDouble() * 2 - 1;
    }
}
