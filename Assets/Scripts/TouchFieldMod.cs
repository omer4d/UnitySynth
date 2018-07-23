using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TouchFieldMod : AudioModule {
    public double stepRatio = 1.05946309436;
    public int steps = 12;
    public double minValue = 1;
    public GameObject[] contactMarkers;

    private Collider collider;
    private Bounds colliderBounds;
    private double signalValue;
    private int contacts;

    void Start()
    {
        collider = GetComponent<Collider>();
        colliderBounds = collider.bounds;
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        ++contacts;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(Input.GetAxisRaw("LeftTrigger") < 0.1)
        {
            return;
        }

        float contactY = 0;

        foreach(ContactPoint p in collision.contacts)
        {
            contactY += p.point.y;
            //Debug.DrawLine(p.point, new Vector3(0, 0, 0), Color.red);
            //Debug.DrawLine(p.point - new Vector3(0, 1, 0), p.point + new Vector3(0, 1, 0), Color.red);
        }

        //contactMarkers[0].transform.position = collision.contacts[0].point;

        contactY /= collision.contacts.Length;
        double tmp = (contactY - colliderBounds.min.y) / (colliderBounds.max.y - colliderBounds.min.y);

        signalValue = minValue * Math.Pow(stepRatio, Math.Floor(tmp * steps));
    }

    private void OnCollisionExit(Collision collision)
    {
        --contacts;
    }

    public override double NextSample(long tick, double time, double dt)
    {
        return contacts > 0 ? signalValue : 0;
    }
}
