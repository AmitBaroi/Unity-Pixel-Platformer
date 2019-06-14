using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField][Range(1, 10)]
    float jumpSpeed = 5f;
    [SerializeField]
    float fallMultiplier = 2.5f;
    [SerializeField]
    float lowJumpMultiplier = 2f;

    Rigidbody2D rb;
    Animator anim;

	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}

	void Update ()
    {
        HorMove();
        Jump();
	}


    private void HorMove()
    {
        // Getting user input
        float horizontal = Input.GetAxis("Horizontal");

        // To trigger walk animation
        if (horizontal > 0 || horizontal < 0)
        {
            anim.SetBool("Walking", true);
            if (horizontal < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
            else if (horizontal > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        // Getting current position
        Vector3 pos = transform.position;
        // Changing X axis
        pos.x += horizontal * moveSpeed * Time.deltaTime;
        // Updating to new position
        transform.position = pos;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        }
        // When falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // When in air
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
