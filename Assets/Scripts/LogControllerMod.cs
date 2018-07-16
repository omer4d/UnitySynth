using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogControllerMod : AudioModule {
    public double stepRatio = 1.05946309436;
    public int steps = 12;

    public ControllerId controllerId = ControllerId.None;

    public override double NextSample(long tick, double time, double dt)
    {
        return Math.Pow(stepRatio, (ControllerPoller.Poll(controllerId) + 1) / 2 * steps);
    }
}
