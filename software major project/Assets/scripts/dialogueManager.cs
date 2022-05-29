using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    bool conversationOver;
    public Image textbox;
    Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        //hides the dialogue box/text because no conversation is happening
        textbox.enabled = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
    }


    //called from playerInteraction script
    public void startDialogue(dialogue conversation, Queue<string> sentences)
    {

        //changes the text on the screen to be the character's name
        nameText.text = conversation.name;
        conversationOver = false;

        //loops through each sentence for object/character and adds to the Queue
        foreach (string sentence in conversation.characterSentences)
        {
            //Debug.Log(sentence);
            sentences.Enqueue(sentence);
        }

        if (Input.GetKeyDown("space"))
        {
            //stops the conversation from continuing in the background once space is pressed
            if (conversationOver == true)
            {
                return;
            }
            else
            {
                //stops the player from moving while the conversation is still in progress
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

                //textbox appears (if not already visible) and conversation begins or moves to the next line
                FindObjectOfType<NPC1interaction>().interactionNotif.enabled = false;
                textbox.enabled = true;
                nameText.enabled = true;
                dialogueText.enabled = true;
                displayNextSentence(sentences);
            }
        }

        if (Input.GetKeyDown("x"))
        {
            endDialogue();
            return;
        }
    }


    public void displayNextSentence(Queue<string> sentences)
    {
        //gets next sentence in the queue
        string currentSentence = sentences.Dequeue();
        //changes the text on the screen to be the next sentence
        dialogueText.text = currentSentence;
    }


    void endDialogue()
    {
        //ends the conversation and removes the conversation assets (text + pictures) from view
        Debug.Log("End of conversation.");
        conversationOver = true;
        textbox.enabled = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
        //lets the player move around again
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
