using UnityEngine;

public class RelaySmoothLinearMovement : RelayMovementBase
{
    public Transform startPoint;
    public Transform endPoint;
    public RelayMovementBase nextRelay;

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();   
    }

    // Update is called once per frame
    public new void Update()
    {
        if (!hasToken) {
            return;
        }

        AddTime();
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, SmoothStep(cyclingTime/period));

        if (CheckPhasePoint(0.25f)) {
            if (nextRelay != null) {
                //TODO
                nextRelay.RelayToken(this, Vector3.zero);
                hasToken = false;
                Debug.Log("[RelaySmoothLinearMovement] Se da el relevo");
            }
        }
    }

    public override void RelayToken(RelayMovementBase yieldingRelay, Vector3 velocity)
    {
        hasToken = true;
        cyclingTime = period * 0.75f;
        Debug.Log("[RelaySmoothLinearMovement] Coge el relevo");
    }
}
