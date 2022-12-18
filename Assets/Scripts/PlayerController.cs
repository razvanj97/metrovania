using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;
    public float jumpForce;
    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;
    // Fire ability
    public BulletController shotToFire;
    public Transform shotPoint;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {   
        if(canMove && Time.timeScale != 0) {
            // Move Sideways
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

            // Handle Direction Change
            if(theRB.velocity.x < 0) {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            } else if (theRB.velocity.x > 0) {
                transform.localScale = Vector3.one;
            }

            // Checking if on the ground
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

            // Jumping
            if(Input.GetButtonDown("Jump") && isOnGround) 
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                AudioManager.instance.PlaySFXAdjusted(12);
            }

            if (Input.GetButtonDown("Fire1")) {
            // Instantiate(shotToFire, shotPoint.position, shotPoint.rotation);
                Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
                anim.SetTrigger("shotFired");
                AudioManager.instance.PlaySFXAdjusted(14);
            }
        } else {
            theRB.velocity = Vector2.zero;
        }

        anim.SetBool("IsOnGround", isOnGround);
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
    }
}
