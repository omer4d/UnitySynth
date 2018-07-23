using System;

public class AudioUtils {
    public static double RaiseSemitones(double semi)
    {
        return Math.Pow(1.05946309436, semi);
    }
}
