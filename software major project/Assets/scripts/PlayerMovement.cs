using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //script attached to player character

    float moveSpeed = 4.5f;
    public Rigidbody2D rbody;
    public Animator animator;
    public GameObject instructionsImage;

    //can store x and y values
    Vector2 movement;


    void Update()
    {
        //gives value between -1 and 1 depending on input (e.g. for horizontal, pressing left key gives -1 and pressing right key gives 1)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //sets the animator parameters (of horizontal/vertical/speed) to the values in this script
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        //disables the 'move with the arrow keys' instruction once the player starts moving
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Destroy(instructionsImage);
        }
    }


    void FixedUpdate()
    {
        //moves rigidbody to new position + makes it collide with anything in the way -> moves to current position plus movement
        rbody.MovePosition(rbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    
    }
}
