using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundedNPC : MonoBehaviour
{
    //script attached to npc1 in MainScene

    private Vector3 directionVector; //NPC's position
    public float speed = 3f;
    private Rigidbody2D npcRigidbody;
    private Animator npcAnim; //NPC's animator
    int direction = 1;
    int tempDirection;

    private void Start()
    {
        npcAnim = GetComponent<Animator>();
        npcRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    private void Update()
    {
        npcRigidbody.MovePosition(transform.position + directionVector * speed * Time.deltaTime);
    }

    //called from Start and any time the NPC reaches a boundary
    void ChangeDirection()
    {
        direction = direction * -1;

        //could be done using if/else statements, but this will help if you add left/right later
        switch (direction)
        {
            case -1:
                //walking down
                directionVector = Vector3.down;
                npcAnim.Play("walkDown");
                break;
            case 1:
                //walking up
                directionVector = Vector3.up;
                npcAnim.Play("walkUp");
                break;
            default:
                break; //ensures there's always a way for the script to exit
        }

    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "npcBound")
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tempDirection = direction;
            npcAnim.Play("walkDown"); //ensures the NPC is facing towards the camera when talking to the player
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            //makes sure the NPC continues in the same way it was walking before
            direction = tempDirection * -1;
            ChangeDirection();

            npcRigidbody.constraints = RigidbodyConstraints2D.None;
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
