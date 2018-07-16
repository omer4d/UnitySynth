using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMod : AudioModule {
    public ControllerId controllerId = ControllerId.None;
    public double scale = 1;
    public bool clamp = true;

    public override double NextSample(long tick, double time, double dt)
    {
        return ControllerPoller.Poll(controllerId);
    }
}
