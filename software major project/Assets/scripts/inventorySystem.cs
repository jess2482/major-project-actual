using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySystem : MonoBehaviour
{
    //script attached to playerCharacter in MazeMinigame

    public List<GameObject> collectedItems = new List<GameObject>(); //list of items picked up

    public GameObject inventoryUI; //empty panel containing the inventory images
    public Image[] inventoryImages; //images within each of the item slots

    public GameObject instructionScreen;
    mazeTaskManager taskScript;


    void Start()
    {
        taskScript = instructionScreen.GetComponent<mazeTaskManager>();
    }


    void Update()
    {
        //if all four items collected, player wins
        if (collectedItems.Count == 4)
        {
            taskScript.winGame = true;
        }
    }

    //called from interactionSystem script
    //every time an item is picked up, adds item to the list + disables the item + resets the UI 
    public void pickUpItem(GameObject item)
    {
        Debug.Log("picked up an item!");
        collectedItems.Add(item);
        item.SetActive(false);
        updateUI();
    }

    //called from pickUpItem function
    void updateUI()
    {
        hideAllImages();
        //for each item in the list, shows it in the inventory slots on the UI
        for (int i = 0; i < collectedItems.Count; i++)
        {
            Debug.Log(collectedItems[i]);
            //changes the image in each itemSlot to be the collected image's sprite
            inventoryImages[i].sprite = collectedItems[i].GetComponent<SpriteRenderer>().sprite;
            //makes the image in the inventory visible
            inventoryImages[i].gameObject.SetActive(true);
        }
    }

    //hides the UI images of each item (upon starting + whenever UI is being updated)
    void hideAllImages() {
        foreach (var i in inventoryImages) { i.gameObject.SetActive(false); }
    }
    

}
