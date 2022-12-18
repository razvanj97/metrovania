using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{

    public static RespawnController instance;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    private Vector3 respawnPoint;
    public float waitToRespawn;
    private GameObject thePlayer;

    public GameObject deathEffect;
    public SpriteRenderer[] playerSprites;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.gameObject;
        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Creates a checkpoint for respawin
    public void SetSpawn(Vector3 newPosition) {
        respawnPoint = newPosition;
    }
    
    // Creates coroutine (happens in parallel to the game play)
    public void Respawn() {
        StartCoroutine(RespawnCo());
    }

    // Coroutine - starts when the player dies
    IEnumerator RespawnCo() {
        // Disables the player character
        thePlayer.SetActive(false);
        if(deathEffect != null) {
            Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);
        }

        // Fixes a bug when sometimes the player would be invisible after respawn
        foreach(SpriteRenderer sr in playerSprites) {
                    sr.enabled = true;
        }
        // Counts down the respawn time
        yield return new WaitForSeconds(waitToRespawn);
        // Gets the current scene and reloads it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // The player gets 'sent' back to the original spawn position;
        thePlayer.transform.position = respawnPoint;
        // The player gets active again
        thePlayer.SetActive(true);
        // The player gets full health again - calling the full health function from PlayerHealthController script file
        PlayerHealthController.instance.FillHealth();
    }
}
