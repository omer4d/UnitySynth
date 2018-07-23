using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerPoller : MonoBehaviour {
    private static double[] values = new double[Enum.GetNames(typeof(ControllerId)).Length];

    private static Vector3 lastMousePos;
    private static Vector3 lastLeftControllerPos;
    private static Vector3 lastRightControllerPos;

    private ButterworthLowpassFilter filter;

    public double mouseSensitivity = 1;
    public double leftControllerSensitivity = 100000;
    public double rightControllerSensitivity = 100000;

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
        // Mouse:
        values[(int)ControllerId.None] = 0;
        values[(int)ControllerId.MouseX] = Input.mousePosition.x / Screen.width * 2 - 1;
        values[(int)ControllerId.MouseY] = Input.mousePosition.y / Screen.height * 2 - 1;
        values[(int)ControllerId.MouseDeltaX] = clamp((Input.mousePosition.x - lastMousePos.x) * mouseSensitivity / Screen.width, -1, 1);
        values[(int)ControllerId.MouseDeltaY] = clamp((Input.mousePosition.y - lastMousePos.y) * mouseSensitivity / Screen.height, -1, 1);
        values[(int)ControllerId.MouseAbsDeltaX] = clamp(filter.Run(Math.Abs(Input.mousePosition.x - lastMousePos.x) * mouseSensitivity) / Screen.width * 2 - 1, -1, 1);
        values[(int)ControllerId.MouseAbsDeltaY] = clamp(Math.Abs(Input.mousePosition.y - lastMousePos.y) * mouseSensitivity / Screen.height * 2 - 1, -1, 1);

        // VR controllers:
        Vector3 leftControllerPos = lastLeftControllerPos;
        Vector3 rightControllerPos = lastRightControllerPos;

        List<XRNodeState> nodeStates = new List<XRNodeState>();
        InputTracking.GetNodeStates(nodeStates);

        foreach (XRNodeState state in nodeStates)
        {
            if (state.nodeType == XRNode.RightHand)
            {
                state.TryGetPosition(out rightControllerPos);
            }
            else if (state.nodeType == XRNode.LeftHand)
            {
                state.TryGetPosition(out leftControllerPos);
            }
        }

        values[(int)ControllerId.RightControllerDeltaMag] = clamp((rightControllerPos - lastRightControllerPos).magnitude * rightControllerSensitivity * 2 - 1, -1, 1);
        values[(int)ControllerId.LeftControllerDeltaMag] = clamp((leftControllerPos - lastLeftControllerPos).magnitude * leftControllerSensitivity * 2 - 1, -1, 1);

        values[(int)ControllerId.RightTriggerAnalog] = Input.GetAxisRaw("RightTrigger") * 2 - 1;
        values[(int)ControllerId.LeftTriggerAnalog] = Input.GetAxisRaw("LeftTrigger") * 2 - 1;

        //Debug.Log("LD: " + Poll(ControllerId.LeftControllerDeltaMag));

        lastLeftControllerPos = leftControllerPos;
        lastRightControllerPos = rightControllerPos;
        lastMousePos = Input.mousePosition;
    }

    public static double Poll(ControllerId id)
    {
        return values[(int)id];
    }
}
