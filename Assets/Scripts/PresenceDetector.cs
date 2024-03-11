using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresenceDetector : MonoBehaviour
{
    public string acceptedTag;

    public delegate void PresenceChangeDelegate(GameObject gameObject);
    public PresenceChangeDelegate OnObjectEnter;
    public PresenceChangeDelegate OnObjectExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(acceptedTag)) {
            Debug.Log("Entro");
            if (OnObjectEnter != null) {
                OnObjectEnter(other.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag(acceptedTag)) {
            Debug.Log("Salio");
            if (OnObjectExit != null) {
                OnObjectExit(other.gameObject);
            }
        }
    }
}
