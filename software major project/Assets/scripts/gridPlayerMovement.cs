using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridPlayerMovement : MonoBehaviour
{
    //script attached to playerCharacter in RainMinigame scene

    float moveSpeed = 4f; //how fast the player moves to the next point
    public Transform movePoint; //point that the player is moving to
    float horizontalInput;
    float verticalInput;

    public LayerMask stopsMovement;
    rainTaskManager taskScript;


    void Start()
    {
        //stops the movePoint from being a player of the playerCharacter as soon as the game starts
        //(prevents the movePoint from constantly moving when the player moves, while still keeping the hierarchy organised)
        movePoint.parent = null;

        taskScript = FindObjectOfType<rainTaskManager>();
    }


    void Update()
    {
        //moves the player character to the point it's meant to go to
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed*Time.deltaTime);

        //ensures that a new grid destination can only be inputted if:
        //    - the minigame is in play
        //    - the character has already reached its destination
        if (taskScript.notStartedYet == false && taskScript.winGame == false)
        {
            if (Vector3.Distance(transform.position, movePoint.position) <= 0.01f)
            {
                GetMovementInput();
            }
        }

    }

    //called from Update function
    void GetMovementInput()
    {
        //checks for arrow key input + sets the place for the player character to move to
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput == -1f || horizontalInput == 1f)
        {
            //ensures that there are no colliders in the area around the player before telling to move to that area
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontalInput, 0f, 0f), .1f, stopsMovement))
            {
                movePoint.position += new Vector3(horizontalInput, 0f, 0f);
            }
        }
        else if (verticalInput == -1f || verticalInput == 1f)
        {
            //ensures that there are no colliders in the area around the player before telling to move to that area
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, verticalInput, 0f), .1f, stopsMovement))
            {
                movePoint.position += new Vector3(0f, verticalInput, 0f);
            }
        }
    }
}
