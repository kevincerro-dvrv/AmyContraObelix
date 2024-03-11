using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoorController : DoorController {
    public float amplitude;
    public float period;
    private float startYCoordinate;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        elapsedTime = 0;
        startYCoordinate = transform.localPosition.y;        
    }

    // Update is called once per frame
    protected override void  Update() {
        base.Update();

        
        if(doorMoving) {
            Vector3 newPosition = transform.localPosition;
            newPosition.y = startYCoordinate + CalculatePosition();
            transform.localPosition = newPosition;
        }
        
    }


    
    private float CalculatePosition() {
        float pinPon = Mathf.PingPong(elapsedTime * 2 / period, 1);

        float smoothStep = Mathf.SmoothStep(0, 1, pinPon);

        return (smoothStep)*amplitude;
    }
}