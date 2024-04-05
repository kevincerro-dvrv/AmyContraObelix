using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLinearMovement : MonoBehaviour {
    public Transform startPoint;
    public Transform endPoint;
    public float period;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(period != 0) {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, SmoothStep(Time.time/period));
        }
    }

    private float SmoothStep(float t) {
        return Mathf.SmoothStep(0f, 1f, PingPong(t));
    }

    //Función PingPong con periodo 1
    private float PingPong(float t) {
        return Mathf.PingPong(t*2f, 1f);
    }
}
