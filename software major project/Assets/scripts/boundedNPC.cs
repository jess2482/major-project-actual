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
        
        /*Vector3 temp = transform.position + directionVector * speed * Time.deltaTime;

        //checks whether boundary has the point in it that the NPC 'wants' to move to
        //so checking whether the NPC is about to move outside the boundary + changing direction if it is
        if (boundsCollider.bounds.Contains(temp))
        {
            npcRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }*/
    }

    void ChangeDirection()
    {
        //int direction = Random.Range(0, 2);
        direction = direction * -1;
        Debug.Log("changing direction...");

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

        //UpdateAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision detected");
        if (collision.gameObject.tag == "npcBound")
        {
            Debug.Log("it's a boundary");
            ChangeDirection();
        }
        if (collision.gameObject.tag == "Player")
        {
            npcAnim.Play("walkDown");
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direction = 1;
            ChangeDirection();
            npcRigidbody.constraints = RigidbodyConstraints2D.None;
            npcRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    /*void UpdateAnimation()
    {
        npcAnim.SetFloat("moveX", directionVector.x);
        npcAnim.SetFloat("moveY", directionVector.y);
    }*/
}
