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

    wholeGameManager managerScript;
    GameObject endingPodium;
    GameObject podiumTokens;
    public GameObject tokenInstruction;
    bool readyToPlace = false; //checks whether the player can place the tokens to end the game


    private void Start()
    {
        managerScript = FindObjectOfType<wholeGameManager>();

        //ensures the podium where player can place tokens to win the game is not visible
        endingPodium = GameObject.Find("endingPodium");
        podiumTokens = GameObject.Find("podiumTokens");
        endingPodium.SetActive(false);
        podiumTokens.SetActive(false); 
        tokenInstruction.SetActive(false);

        inventoryUI.SetActive(false);
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        mazeToken = GameObject.Find("mazeToken");
        rainToken = GameObject.Find("rainToken");

        //automatically assigns the player a token once they win a minigame
        if (managerScript.mazeMinigameWon == true)
        {
            AddNewItem(mazeToken);
        }
        if (managerScript.rainMinigameWon == true)
        {
            AddNewItem(rainToken);
        }
    }

    private void Update()
    {
        //shows the podium if both minigame tokens have been collected)
        if (managerScript.mazeMinigameWon == true && managerScript.rainMinigameWon == true)
        {
            endingPodium.SetActive(true);
        }

        //ends the game once the player pressed space to place tokens on the podium
        if(readyToPlace == true && Input.GetKeyDown("space")) 
        {
            TokenEnding();
        }

        //opens/closes the inventory
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        //allows the player to place items on the token at the end of the game (once prompted to by the podium)
        if (collision.gameObject.transform.parent.gameObject == endingPodium)
        {
            tokenInstruction.SetActive(true);
            readyToPlace = true; 
        }
    }

    void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.transform.parent.gameObject == endingPodium)
        {
            tokenInstruction.SetActive(false);
            readyToPlace = false;
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
        //clears all current UI images so that there are no double-ups when new ones are added
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


    //hides the UI images of each item (upon starting + every time UI is updated + once tokens are placed on podium)
    void hideAllImages()
    {
        foreach (var i in inventoryImages) { i.gameObject.SetActive(false); }
    }

    //called from Update
    //shows tokens on the podium + starts move to ending screen
    void TokenEnding()
    {
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        hideAllImages();
        tokenInstruction.SetActive(false);
        podiumTokens.SetActive(true);

        StartCoroutine(WaitForTokens(2f));
        
    }

    //called from TokenEnding -> waits so that the player can see the tokens being placed, then transitions to ending scene
    IEnumerator WaitForTokens(float time)
    {
        Debug.Log("about to wait in mainInventory");
        //pauses this coroutine for certain amount of time before continuing
        yield return new WaitForSeconds(time);
        FindObjectOfType<levelLoader>().endGame = true;
    }
}
