using System;
using UnityEngine;

public class ControllerPoller : MonoBehaviour {
    private static double[] values = new double[Enum.GetNames(typeof(ControllerId)).Length];
    private static Vector3 lastMousePos;
    private ButterworthLowpassFilter filter;

    public double mouseSensitivity = 1;

    private static double clamp(double x, double min, double max)
    {
        return x < min ? min : (x > max ? max : x); 
    }

    private void Awake()
    {
        filter = new ButterworthLowpassFilter(60, 4, 0);
    }

    void Update ()
    {
        values[(int)ControllerId.None] = 0;
        values[(int)ControllerId.MouseX] = Input.mousePosition.x / Screen.width * 2 - 1;
        values[(int)ControllerId.MouseY] = Input.mousePosition.y / Screen.height * 2 - 1;
        values[(int)ControllerId.MouseDeltaX] = clamp((Input.mousePosition.x - lastMousePos.x) * mouseSensitivity / Screen.width, -1, 1);
        values[(int)ControllerId.MouseDeltaY] = clamp((Input.mousePosition.y - lastMousePos.y) * mouseSensitivity / Screen.height, -1, 1);
        values[(int)ControllerId.MouseAbsDeltaX] = clamp(filter.Run(Math.Abs(Input.mousePosition.x - lastMousePos.x) * mouseSensitivity) / Screen.width * 2 - 1, -1, 1);
        values[(int)ControllerId.MouseAbsDeltaY] = clamp(Math.Abs(Input.mousePosition.y - lastMousePos.y) * mouseSensitivity / Screen.height * 2 - 1, -1, 1);

        lastMousePos = Input.mousePosition;
    }

    public static double Poll(ControllerId id)
    {
        return values[(int)id];
    }
}
