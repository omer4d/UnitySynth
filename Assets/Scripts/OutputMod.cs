using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMod : AudioModule {
    public AudioModuleInput source;

    private long tick;
    private double time;
    private double dt;

    public void Start()
    {
        tick = 0;
        time = 0;
        dt = 1.0 / AudioSettings.GetConfiguration().sampleRate;
    }

    public override double NextSample(long tick, double time, double dt)
    {
        return source.ReadSample(tick, time, dt);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        int i = 0;

        while(i < data.Length)
        {
            float val = (float)ReadSample(tick, time, dt);

            for (int j = 0; j < channels; ++j)
            {
                data[i] = val;
                ++i;
            }

            ++tick;
            time += dt;
        }
    }
}
