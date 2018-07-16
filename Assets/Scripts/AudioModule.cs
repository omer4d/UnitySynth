using UnityEngine;

public abstract class AudioModule : MonoBehaviour {
    private double cachedSample;
    private long cacheTick;

    public abstract double NextSample(long tick, double time, double dt);

    public double ReadSample(long tick, double time, double dt)
    {
        if(tick > cacheTick)
        {
            cachedSample = NextSample(tick, time, dt);
            cacheTick = tick;
        }

        return cachedSample;
    }
}
