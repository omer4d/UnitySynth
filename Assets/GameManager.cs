﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Input.GetAxis("RightTrigger"));
	}
}
