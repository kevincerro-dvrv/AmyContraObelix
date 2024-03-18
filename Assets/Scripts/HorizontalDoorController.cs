using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoorController : DoorController {
    public float amplitude;
    public float period;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        elapsedTime = period / 4;
    }

    // Update is called once per frame
    protected override void  Update() {
        base.Update();
        if(GameManager.instance.LevelLocked) {
            return;
        }

        if(doorMoving) {
            Vector3 newPosition = transform.localPosition;
            newPosition.z = CalculatePosition();
            transform.localPosition = newPosition;
        }
        
    }

    private float CalculatePosition() {
        float pinPon = Mathf.PingPong(elapsedTime * 2 / period, 1);

        float smoothStep = Mathf.SmoothStep(0, 1, pinPon);

        return (smoothStep-0.5f)*amplitude;
    }
}
