using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC3interaction : MonoBehaviour
{
    //script attached to npc3 in MainScene

    public Image interactionNotif;
    public dialogue conversation;
    bool interaction;

    //essentially a restricted list of game dialogue
    private Queue<string> NPC3sentences;

    void Start()
    {
        NPC3sentences = new Queue<string>();

        //makes the alert disappear when the game begins
        interactionNotif.gameObject.SetActive(false);
    }

    void Update()
    {
        //if the player collides with the object, it triggers the dialogueManager
        if (interaction == true)
        {
            FindObjectOfType<dialogueManager>().startDialogue(conversation, NPC3sentences);
        }
    }

    //when something touches the object this is attached to
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if the object is the player, makes the alert appear
        if (collision.gameObject.tag == "Player")
        {
            interactionNotif.gameObject.SetActive(true);
            interaction = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //makes the alert disappear again once the player moves away
        interactionNotif.gameObject.SetActive(false);
        interaction = false;
    }
}
