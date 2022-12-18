using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    //Create the points the enemy will walk between
    public Transform[] patrolPoints;
    private int currentPoint;
    public float moveSpeed, waitAtPoints;
    private float waitCounter;
    public float jumpForce;
    public Rigidbody2D theRB;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {   
        //Character will wait 1 second at the point at the beginning
        waitCounter = waitAtPoints;

        // Ensures that the patrol points don't move with the character
        foreach(Transform pPoint in patrolPoints) {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        // Executes the moving direction
        if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2) {
            if (transform.position.x < patrolPoints[currentPoint].position.x) {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
                transform.localScale = Vector3.one;
            }
            if(transform.position.y < patrolPoints[currentPoint].position.y - .5f && theRB.velocity.y == 0) {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
        } else {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            waitCounter -= Time.deltaTime;
            if(waitCounter <= 0) 
            {
                waitCounter = waitAtPoints;
                currentPoint++;

                if(currentPoint >= patrolPoints.Length) {
                    currentPoint = 0;
                }
            }
        }
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}
