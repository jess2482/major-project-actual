using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundedNPC : MonoBehaviour
{
    private Vector3 directionVector; //NPC's position
    public float speed;
    private Rigidbody2D npcRigidbody; //NPC's Rigidbody
    private Animator npcAnim; //NPC's animator
    //public Collider2D boundsCollider;
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
        Move();
    }

    void Move()
    {
        npcRigidbody.MovePosition(transform.position + directionVector * speed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        //int direction = Random.Range(0, 2);
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
            npcAnim.Play("walkDown");
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            direction = tempDirection * -1;
            ChangeDirection();
            npcRigidbody.constraints = RigidbodyConstraints2D.None;
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
