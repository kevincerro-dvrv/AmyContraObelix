using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelayMovementBase : MonoBehaviour
{
    public float period;
    public bool hasToken;

    protected float cyclingTime;
    
    public abstract void RelayToken(RelayMovementBase yieldingComponent, Vector3 velocity);

    // Start is called before the first frame update
    public void Start()
    {
        cyclingTime = 0;
    }

    // Update is called once per frame
    public void Update()
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

    protected void AddTime()
    {
        cyclingTime += Time.deltaTime;
        
        if(cyclingTime >= period) {
            cyclingTime -= period;
        }
    }

    protected bool CheckPhasePoint(float phaseToCheck)
    {
        if (cyclingTime > (phaseToCheck * period)) {
            return cyclingTime - phaseToCheck * period < Time.deltaTime * 1.2f;
        }

        return false;
    }
}
