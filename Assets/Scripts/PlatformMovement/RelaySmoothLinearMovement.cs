using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelaySmoothLinearMovement : RelayMovementBase {
    public RelayMovementBase nextRelay;
    public Transform startPoint;
    public Transform endPoint;
    private Vector3 lastFramePosition;

    private bool mustRotatePlatform;
    private Quaternion startRotation;

    // Start is called before the first frame update
    new void Start() {
        base.Start();     
        lastFramePosition = transform.position;   
        mustRotatePlatform = hasToken;
        if (mustRotatePlatform) {
            startRotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update() {
        if(! hasToken) {
            return;
        }

        AddTime();
        lastFramePosition = transform.position;   
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, SmoothStep(cyclingTime/period));

        if(CheckPhasePoint(0.25f)) {
            if(nextRelay != null) {
                Vector3 velocity = (transform.position-lastFramePosition) / Time.deltaTime;
                nextRelay.RelayToken(this, velocity);
                hasToken = false;
                Debug.Log("[RelaySmoothLinearMovement] Se da el relevo");
            }
        }
    }

    public override void RelayToken(RelayMovementBase yieldingRelay, Vector3 velocity) {
        hasToken = true;
        cyclingTime = 0.75f * period;
        if (mustRotatePlatform) {
            transform.rotation = startRotation;
        }
    }
}
