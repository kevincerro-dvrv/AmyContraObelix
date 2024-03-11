using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public PresenceDetector playerDetector;
    protected bool doorMoving = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerDetector.OnObjectEnter += ActivateDoors;
        playerDetector.OnObjectExit += DeactivateDoors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateDoors(GameObject gameObject) {
        Debug.Log("Activate");
        doorMoving = true;
    }

    void DeactivateDoors(GameObject gameObject) {
        Debug.Log("Deactivate");
        doorMoving = false;
    }
}
