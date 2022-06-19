using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mazeTaskManager : MonoBehaviour
{
    //script attached to instructionScreen in MazeMinigame

    public GameObject taskCanvas;
    public GameObject instructionScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    Rigidbody2D playerRigidbody;
    public bool loseGame = false;
    public bool winGame = false;
    public bool notStartedYet = true;

    void Start()
    {
        //ensures correct screens are showing
        taskCanvas.SetActive(false);
        instructionScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        //stops player from moving until the minigame starts
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

        if (loseGame == true)
        {
            gameLost();
        }
        else if (winGame == true)
        {
            gameWon();
        }

    }

    //once the game has started, unfreezes the player's position and hides the initial instruction screen
    void startGame()
    {
        Debug.Log("game starting...");
        notStartedYet = false;
        instructionScreen.SetActive(false);
        taskCanvas.SetActive(true);
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //called from Update
    //once the game has been won, freezes the player's position and shows the winning screen
    void gameWon()
    {
        Debug.Log("you won!");
        FindObjectOfType<wholeGameManager>().mazeMinigameWon = true;
        Debug.Log("mazeMinigameWon is " + FindObjectOfType<wholeGameManager>().mazeMinigameWon);
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        winScreen.SetActive(true);
        taskCanvas.SetActive(false);
    }

    //called from Update
    //once the game has been lost, freezes the player's position and shows the losing screen, with an option to restart
    void gameLost()
    {
        Debug.Log("you lost :(");
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        loseScreen.SetActive(true);
        taskCanvas.SetActive(false);

        if (Input.GetKeyDown("space"))
        {
            //reloads the current scene, to restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
