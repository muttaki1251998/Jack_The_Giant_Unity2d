using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 14.88f;
    public float maxSpeed = 30f;

    private Rigidbody2D rb2g;
    private Animator anim;

	// Use this for initialization
	void Start () {

        rb2g = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        PlayerMovement();

    }

    void PlayerMovement()
    {
        float forceX = 0f;
        float velocity = Mathf.Abs(rb2g.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");

        if(h < 0) // If movement is Left
        {
            if(velocity < maxSpeed)
            {
                forceX = -speed;
                anim.SetBool("isWalk", true);

                Vector3 temp = transform.localScale;
                temp.x = -1.0f;
                transform.localScale = temp;
            }
            
        }

        else if(h > 0) // if movement is Right
        {
           if(velocity < maxSpeed)
            {
                forceX = speed;
                anim.SetBool("isWalk", true);

                Vector3 temp = transform.localScale;
                temp.x = 1.0f;
                transform.localScale = temp;
            }
        }

            
        else
        {
            anim.SetBool("isWalk", false);
        }

        // Makes the player to move
        rb2g.AddForce(new Vector2(forceX, 0));
    }
}
