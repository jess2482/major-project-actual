using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mainInventory : MonoBehaviour
{
    //script attached to playerCharacter in the MainScene

    public List<GameObject> collectedItems = new List<GameObject>(); //list of items picked up

    public GameObject inventoryUI; //empty panel containing the inventory images
    public Image[] inventoryImages; //images within each of the item slots
    public GameObject newItem;
    bool inventoryOpen = false;
    Rigidbody2D playerRigidbody;
    public GameObject mazeToken;
    public GameObject rainToken;

    private void Start()
    {
        inventoryUI.SetActive(false);
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        mazeToken = GameObject.Find("mazeToken");
        rainToken = GameObject.Find("rainToken");

        if (FindObjectOfType<wholeGameManager>().mazeMinigameWon == true)
        {
            AddNewItem(mazeToken);
        }
        if (FindObjectOfType<wholeGameManager>().rainMinigameWon == true)
        {
            AddNewItem(rainToken);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            if (inventoryOpen == false)
            {
                Debug.Log("opening inventory...");
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
                inventoryUI.SetActive(true);
                inventoryOpen = true;
            }
            else
            {
                Debug.Log("closing inventory...");
                playerRigidbody.constraints = RigidbodyConstraints2D.None;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                inventoryUI.SetActive(false);
                inventoryOpen = false;
            }
        }
    }

    //every time an item is picked up, adds item to the list + disables the item + resets the UI 
    public void AddNewItem(GameObject item)
    {
        collectedItems.Add(item);
        updateUI();
    }


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


    //upon starting, hides the UI images of each item
    void hideAllImages()
    {
        foreach (var i in inventoryImages) { i.gameObject.SetActive(false); }
    }
}
