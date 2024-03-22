using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    public static GameData instance;

    public int playerScore;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }


}
