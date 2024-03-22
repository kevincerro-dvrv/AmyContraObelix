using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelayMovementBase : MonoBehaviour
{
    public float period;
    
    public abstract void RelayToken(Vector3 velocity);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected float SquareWave(float t) {
        return Mathf.Clamp(PingPong(t) * 100000f - 50000f, -1f, 1f);
    }

    protected float PingPong(float t) {
        float frequency = t*2; // 1 second frecuency instead of 2 by default

        return Mathf.PingPong(frequency, 1f);
    }

    protected float SmoothStep(float t) {
        return Mathf.SmoothStep(0f, 1f, PingPong(t));
    }
}
