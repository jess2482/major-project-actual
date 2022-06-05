using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionSystem : MonoBehaviour
{
    //script attached to playerCharacter in MazeMinigame

    bool itemDetected =false;
    public GameObject detectedItem;


    void Update()
    {
        //if the player is within range and presses the space button, the item is 'picked up'
        if (itemDetected == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<inventorySystem>().pickUpItem(detectedItem);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks whether the player has collided with an item
        if (collision.gameObject.tag == "item")
        {
            Debug.Log("collided with item");
            detectedItem = collision.gameObject; 
            itemDetected = true;
        }
    }

    //when player leaves the object's trigger area, can't pick up object
    private void OnTriggerExit2D(Collider2D collision)
    {
        itemDetected = false;
        detectedItem = null;
    }
}
