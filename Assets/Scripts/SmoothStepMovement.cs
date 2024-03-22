using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothStepMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float period;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period != 0) {
            transform.position = Interpolate();
        }

    }

    private float PingPong(float t) {
        float frequency = t*2; // 1 second frecuency instead of 2 by default

        return Mathf.PingPong(frequency, 1f);
    }

    private float SmoothStep(float t) {
        return Mathf.SmoothStep(0f, 1f, PingPong(t));
    }

    private Vector3 Interpolate() {
        float t = Time.time;
        float frequency = t / period;

        return Vector3.Lerp(startPoint.position, endPoint.position, SmoothStep(frequency));
    }
}
