using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstMod : AudioModule {
    private static ConstMod zero;
    private static ConstMod one;

    public double value;

    public override double NextSample(long tick, double time, double dt)
    {
        return value;
    }

    public static ConstMod Create(double value)
    {
        var go = new GameObject();
        var mod = go.AddComponent<ConstMod>();
        mod.value = value;
        return mod;
    }

    public static ConstMod Zero()
    {
        if(zero == null)
        {
            zero = Create(0);
        }

        return zero;
    }

    public static ConstMod One()
    {
        if (one == null)
        {
            one = Create(1);
        }

        return one;
    }
}
