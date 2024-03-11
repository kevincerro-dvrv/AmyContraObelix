using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public PresenceDetector playerDetector;

    protected bool doorMoving;
    protected float elapsedTime;
    // Start is called before the first frame update
    protected virtual void Start() {
        playerDetector.OnObjectEnter += ActivateDoors;
        playerDetector.OnObjectExit += DeactivateDoors;
        doorMoving = false;
    }

    // Update is called once per frame
    protected virtual void Update() {
        if(doorMoving) {
            elapsedTime += Time.deltaTime;
        }        
    }

    public void ActivateDoors(GameObject activatingObject) {
        Debug.Log("[DoorController] activar");
        doorMoving = true;
    }

    public void DeactivateDoors(GameObject activatingObject) {
        //Debug.Log("[DoorController] desactivar");
        doorMoving = false;
    }

}
