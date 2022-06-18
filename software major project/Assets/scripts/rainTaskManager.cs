using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rainTaskManager : MonoBehaviour
{
    //script attached to instructionScreen in RainMinigame

    public GameObject taskCanvas;
    public GameObject instructionScreen;
    public GameObject winScreen;
    Rigidbody2D playerRigidbody;
    public bool winGame = false;
    public bool notStartedYet = true;
    
    void Start()
    {
        //ensures correct screens are showing
        taskCanvas.SetActive(false);
        instructionScreen.SetActive(true);
        winScreen.SetActive(false);

        //stops player + raindrop from moving until the game starts
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        
    }


    //controls the game's status (not started / won / lost)
    void Update()
    {
        if (Input.GetKeyDown("space") && notStartedYet == true)
        {
            startGame();
        }

        if (winGame == true)
        {
            gameWon();
        }

    }


    //after the game has started, unfreezes the player's position + removes the initial instruction screen
    void startGame()
    {
        Debug.Log("game starting...");
        notStartedYet = false;
        instructionScreen.SetActive(false);
        taskCanvas.SetActive(true);
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    //once the game has been won, freezes the player's position and shows the winning screen
    void gameWon()
    {
        Debug.Log("you won!");
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        FindObjectOfType<wholeGameManager>().rainMinigameWon = true;
        //Time.timeScale = 0f;
        winScreen.SetActive(true);
        taskCanvas.SetActive(false);
    }

}
