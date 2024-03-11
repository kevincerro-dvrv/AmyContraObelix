using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoorController : DoorController {
    public float rotatingSpeed;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void  Update() {
        base.Update();

        if(doorMoving) {
            transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);
        }
        
    }
}
