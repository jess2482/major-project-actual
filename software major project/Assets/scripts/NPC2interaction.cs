using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC2interaction : MonoBehaviour
{
    //script attached to npc2 in MainScene

    public Image interactionNotif;
    public dialogue initialConversation;
    public dialogue successConversation; 
    bool interaction;

    wholeGameManager managerScript;

    //essentially a restricted list of game dialogue
    Queue<string> NPC2sentences;

    void Start()
    {
        NPC2sentences = new Queue<string>();
        managerScript = FindObjectOfType<wholeGameManager>();

        //makes the alert disappear when the game begins
        interactionNotif.gameObject.SetActive(false);
    }

    void Update()
    {
        //if the player collides with the object, it triggers the dialogueManager
        if (interaction == true)
        {
            if (managerScript.mazeMinigameWon == false)
            {
                FindObjectOfType<dialogueManager>().startDialogue(initialConversation, NPC2sentences);
            }
            else if (managerScript.mazeMinigameWon == true)
            {
                NPC2sentences = new Queue<string>();
                FindObjectOfType<dialogueManager>().startDialogue(successConversation, NPC2sentences);
            }
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
