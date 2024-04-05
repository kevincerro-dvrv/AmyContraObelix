using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaySmoothLinearMovement : RelayMovementBase {
    public RelayMovementBase nextRelay;
    public Transform startPoint;
    public Transform endPoint;



    
    // Start is called before the first frame update
    new void Start() {
        base.Start();        
    }

    // Update is called once per frame
    void Update() {
        if(! hasToken) {
            return;
        }

        AddTime();
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, SmoothStep(cyclingTime/period));

        if(CheckPhasePoint(0.25f)) {
            if(nextRelay != null) {
                //TODO pasar la velocidad real
                nextRelay.RelayToken(this, Vector3.zero);
                hasToken = false;
                Debug.Log("[RelaySmoothLinearMovement] Se da el relevo");
            }
        }
    }

    public override void RelayToken(RelayMovementBase yieldingRelay, Vector3 velocity) {
        hasToken = true;
        cyclingTime = 0.75f * period;
    }
}
