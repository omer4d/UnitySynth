using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterworthLowpassFilter
{
    private static double BUDDA_Q_SCALE = 6.0;

    private double t0, t1, t2, t3;
    private double coef0, coef1, coef2, coef3;
    private double history1, history2, history3, history4;
    private double gain;
    private double minCutoff, maxCutoff;

    public ButterworthLowpassFilter(double sampleRate, double cutoff, double resonance)
    {
        history1 = 0.0;
        history2 = 0.0;
        history3 = 0.0;
        history4 = 0.0;

        SetSampleRate(sampleRate);
        SetParams(cutoff, resonance);
    }

    public void SetSampleRate(double fs)
    {
        double pi = 4.0 * Math.Atan(1.0);

        t0 = 4.0 * fs * fs;
        t1 = 8.0 * fs * fs;
        t2 = 2.0 * fs;
        t3 = pi / fs;

        minCutoff = fs * 0.01;
        maxCutoff = fs * 0.45;
    }

    public void SetParams(double cutoff, double q)
    {
        cutoff = Math.Max(minCutoff, Math.Min(cutoff, maxCutoff));
        q = Math.Max(0, Math.Min(1.0, q));

        double wp = t2 * Math.Tan(t3 * cutoff);
        double bd, bd_tmp, b1, b2;

        q *= BUDDA_Q_SCALE;
        q += 1.0;

        b1 = (0.765367 / q) / wp;
        b2 = 1.0 / (wp * wp);

        bd_tmp = t0 * b2 + 1.0;

        bd = 1.0 / (bd_tmp + t2 * b1);

        gain = bd * 0.5;

        coef2 = (2.0 - t1 * b2);

        coef0 = coef2 * bd;
        coef1 = (bd_tmp - t2 * b1) * bd;

        b1 = (1.847759 / q) / wp;
        bd = 1.0 / (bd_tmp + t2 * b1);

        gain *= bd;
        coef2 *= bd;
        coef3 = (bd_tmp - t2 * b1) * bd;
    }

    public double Run(double input)
    {
        double output = input * gain;
        double new_hist;

        output -= history1 * coef0;
        new_hist = output - history2 * coef1;

        output = new_hist + history1 * 2.0;
        output += history2;

        history2 = history1;
        history1 = new_hist;

        output -= history3 * coef2;
        new_hist = output - history4 * coef3;

        output = new_hist + history3 * 2.0;
        output += history4;

        history4 = history3;
        history3 = new_hist;

        return output;
    }
}