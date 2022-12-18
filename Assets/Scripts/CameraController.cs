using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController player;
    public BoxCollider2D boundsBox;

    private float halfHeight, halfWidth;
    // Start is called before the first frame update
    void Start()
    {   
        //Find the player and connect the camera
        player = FindObjectOfType<PlayerController>();

        //Set the camera view at the beginning of the game
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;
        AudioManager.instance.PlayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) {
            transform.position = new Vector3(
                Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidth, boundsBox.bounds.max.x - halfWidth), 
                Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeight, boundsBox.bounds.max.y - halfHeight),
                transform.position.z);
        } else {
            player = FindObjectOfType<PlayerController>();
        }
    }
}
