using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RelayMovementBase : MonoBehaviour {

    public float period;
    public bool hasToken;

    protected float cyclingTime;

    public abstract void RelayToken(RelayMovementBase yieldingRelay, Vector3 velocity);
    // Start is called before the first frame update
    public void Start() {
        cyclingTime = 0;        
    }

    protected float SquareWave(float t) {
        return Mathf.Clamp(PingPong(t) * 100000f - 50000f, -1f, 1f);
    }

    protected float SmoothStep(float t) {
        return Mathf.SmoothStep(0f, 1f, PingPong(t));
    }

    //Función PingPong con periodo 1
    protected float PingPong(float t) {
        return Mathf.PingPong(t*2f, 1f);
    }

    protected void AddTime() {
        cyclingTime += Time.deltaTime;

        if(cyclingTime >= period) {
            cyclingTime -= period;
        }
        
    }


    protected bool CheckPhasePoint(float phaseToCheck) {
        if(cyclingTime > phaseToCheck * period) {
            return cyclingTime - phaseToCheck * period < Time.deltaTime * 1.2f;
        }
        return false;
    }
}
