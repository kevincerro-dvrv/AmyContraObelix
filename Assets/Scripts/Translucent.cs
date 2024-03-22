using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translucent : MonoBehaviour {
    public Transform playerCamera;
    public Transform playerTarget;

    public LayerMask detectedLayers;

    private List<ChangeTransparency> transparentObjects;
    // Start is called before the first frame update
    void Start()  {
        transparentObjects =  new List<ChangeTransparency>();
    }

    // Update is called once per frame
    void Update() {
        RaycastHit[] hits =  Physics.RaycastAll(playerCamera.position, 
                                                playerTarget.position-playerCamera.position, 
                                                (playerTarget.position-playerCamera.position).magnitude, 
                                                detectedLayers, 
                                                QueryTriggerInteraction.Ignore);

        List<ChangeTransparency> objectsToNormalize = new List<ChangeTransparency>();
        bool ctEncontrado = false;
        foreach(ChangeTransparency ct in transparentObjects) {
            ctEncontrado = false;
            foreach(RaycastHit rh in hits) {
                ChangeTransparency hitCt  = rh.collider.GetComponent<ChangeTransparency>();
                if(hitCt.Equals(ct)) {
                    ctEncontrado = true;
                    break;
                }
            }

            if(! ctEncontrado) {
                ct.GetNormal();
                objectsToNormalize.Add(ct);
            }

        }

        foreach(ChangeTransparency ct in objectsToNormalize) {
            transparentObjects.Remove(ct);
        }
        objectsToNormalize.Clear();

        if(hits.Length > 0) {    

            foreach(RaycastHit hit in hits) {
                ChangeTransparency ct = hit.collider.GetComponent<ChangeTransparency>();

                if(ct != null) {
                    if( ! transparentObjects.Contains(ct)) {
                        ct.GetTransparent();
                        transparentObjects.Add(ct);
                    }
                }

            }



        }        
    }
}
