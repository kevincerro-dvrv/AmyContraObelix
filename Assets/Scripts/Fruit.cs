using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
    public int points;
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 30f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            GameManager.instance.HarvestFruit(points);
            Destroy(gameObject);
        }
    }
}
