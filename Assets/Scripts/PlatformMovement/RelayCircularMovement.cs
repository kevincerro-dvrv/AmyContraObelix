using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayCircularMovement : RelayMovementBase {

    public Transform rotationCenter;
    private Vector3 rotationAxis;
    public Transform rotationStart;
    public Transform rotationEnd;

    public RelayMovementBase startRelay;
    public RelayMovementBase endRelay;

    private bool isStartMovement;
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
        transform.RotateAround(rotationCenter.position, rotationAxis, Time.deltaTime * 360 / period);
        
        
        if(isStartMovement) {
            if(Vector3.Angle(transform.position-rotationCenter.position, rotationEnd.position-rotationCenter.position) < 1f) {
                endRelay.RelayToken(this, Vector3.zero);
                hasToken = false;
            }
        } else {
            if(Vector3.Angle(transform.position-rotationCenter.position, rotationStart.position-rotationCenter.position) < 1f) {
                startRelay.RelayToken(this, Vector3.zero);
                hasToken = false;
            }
        }

    }

    public override void RelayToken(RelayMovementBase yieldingRelay, Vector3 velocity) {
        hasToken = true;
        cyclingTime = 0;
        if(yieldingRelay.Equals(startRelay)) {
            rotationAxis = Vector3.Cross(rotationStart.position - rotationCenter.position, rotationEnd.position - rotationCenter.position);
            isStartMovement = true;
        } else {
            rotationAxis = Vector3.Cross(rotationEnd.position - rotationCenter.position, rotationStart.position - rotationCenter.position);
            isStartMovement = false;
        }
        rotationAxis = rotationAxis.normalized;
    }
}
