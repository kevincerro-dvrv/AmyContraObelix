using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoorController : DoorController
{
    public float amplitude;
    public float period;

    private float elapsedTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorMoving) {
            Vector3 newPosition = transform.localPosition;
            newPosition.z = CalculatePosition();
            transform.position = newPosition;
        }
    }

    float CalculatePosition()
    {
        float pingPong = Mathf.PingPong(elapsedTime * 2 / period, 1);

        float smoothStep = Mathf.SmoothStep(0, 1, pingPong);

        return (smoothStep - 0.5f) * amplitude;
    }
}
