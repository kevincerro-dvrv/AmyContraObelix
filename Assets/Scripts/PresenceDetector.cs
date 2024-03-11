using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresenceDetector : MonoBehaviour {
    public delegate void PresenceChangeDelegate(GameObject objectDetected);
    public PresenceChangeDelegate OnObjectEnter;
    public PresenceChangeDelegate OnObjectExit;

    public string aceptedTag;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag(aceptedTag)) {
            Debug.Log("[PresenceDetector] entró");
            if(OnObjectEnter !=  null) {
                OnObjectEnter(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag(aceptedTag)) {
            //Debug.Log("[PresenceDetector] salió");
            if(OnObjectExit != null) {
                OnObjectExit(other.gameObject);
            }
        }
    }
}
