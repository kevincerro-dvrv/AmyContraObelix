using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public GameObject[] fruitPrefabs;
    public Player player;
    public GameObject nextLevelDoor;
    public AudioClip fruitSound;
    public Light sunlight;
    private AudioSource audioSource;

    private int nextLevelScore = 400;

    private bool levelLocked;
    public bool LevelLocked => levelLocked;

    //Mínima distancia al jugador cuando se espanea fruta
    private float minPlayerDistance = 4f;
   
    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnFruit());
        //GameData.instance.playerScore = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnFruit() {        
        while( ! levelLocked) {
            if(Random.Range(0f, 1f) < 0.05f) {
                Vector3 spawnPosition = new Vector3 (Random.Range(-8f, 8f), 1f, Random.Range(-8f, 8f));
                while(Vector3.Distance(spawnPosition, player.transform.position) < minPlayerDistance) {
                    spawnPosition = new Vector3 (Random.Range(-8f, 8f), 1f, Random.Range(-8f, 8f));
                }
                Instantiate(fruitPrefabs[Random.Range(0, fruitPrefabs.Length)], spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void HarvestFruit(int points) {
        GameData.instance.playerScore += points;
        audioSource.PlayOneShot(fruitSound);
        Debug.Log($"[GameManater] HarvestFruit {GameData.instance.playerScore}");

        if(GameData.instance.playerScore >= nextLevelScore) {
            nextLevelDoor.SetActive(true);
        }
    }

    

    public void NextLevel() {
        levelLocked = true;
        StartCoroutine(FadeOutLight());
    }

    private IEnumerator FadeOutLight() {
        while(sunlight.intensity > 0.02f) {
            sunlight.intensity -= 0.002f;
            yield return new WaitForSeconds(0.01f);
        }
        Invoke("LoadNextLevel", 2f);
    } 

    private void LoadNextLevel() {
        SceneManager.LoadScene("AmyYLasPlataformas");
        //SceneManager.LoadScene("AmyContraObelix");
    }
}
