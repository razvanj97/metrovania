using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public Animator anim;
    public float distanceToOpen;
    private PlayerController thePlayer;
    private bool playerExiting;

    public Transform exitPoint;
    public float movePlayerSpeed;
    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = PlayerHealthController.instance.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, thePlayer.transform.position) < distanceToOpen) {
            anim.SetBool("doorOpen", true);
        } else {
            anim.SetBool("doorOpen", false);
        }
        if(playerExiting) {
            thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            if(!playerExiting) {
                thePlayer.canMove = false;
                StartCoroutine(UseDoorCo());
            }
        }
    }

    IEnumerator UseDoorCo() {
        playerExiting = true;
        //Freezes the player
        thePlayer.anim.enabled = false;
        //Starts the fade to black
        UIController.instance.StartFadeToBlack();
        // Waits 1.5 seconds to go to the next level
        yield return new WaitForSeconds(1f);
        // Sets the new respawn point to the door exit point
        RespawnController.instance.SetSpawn(exitPoint.position);
        // Allows the player to move again
        thePlayer.canMove = true;
        // Enables back the player animations effects
        thePlayer.anim.enabled = true;
        // Goes from black to transparent on the loading screen
        UIController.instance.StartFadeFromBlack();
        // // Saves the players progress to the map they quit the game
        // PlayerPrefs.SetString("ContinueLevel", levelToLoad);
        // // Saves the coordinates of the player on the X, Y and Z axis
        // PlayerPrefs.SetFloat("PosX", exitPoint.position.x);
        // PlayerPrefs.SetFloat("PosX", exitPoint.position.y);
        // PlayerPrefs.SetFloat("PosX", exitPoint.position.z);
        // Loads the new level scene
        SceneManager.LoadScene(levelToLoad);
    }
}
