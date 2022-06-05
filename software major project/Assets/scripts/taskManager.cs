using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class taskManager : MonoBehaviour
{
    //script attached to instructionScreen in MazeMinigame

    public GameObject taskCanvas;
    public GameObject instructionScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    Rigidbody2D playerRigidbody;
    public bool loseGame = false;
    public bool winGame = false;

    
    void Start()
    {
        //ensures correct screens are showing
        taskCanvas.SetActive(false);
        instructionScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        //stop player from moving until the game starts
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    
    //controls the game's status (not started / won / lost)
    void Update()
    {
        if (Input.GetKeyDown("space"))
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


    //before the game has started, freezes the player's position and shows the initial instruction screen
    void startGame()
    {
        instructionScreen.SetActive(false);
        taskCanvas.SetActive(true);
        playerRigidbody.constraints = RigidbodyConstraints2D.None;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    //once the game has been won, freezes the player's position and shows the winning screen
    void gameWon()
    {
        FindObjectOfType<levelLoader>().gameWon = true;
        playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        winScreen.SetActive(true);
        taskCanvas.SetActive(false);
    }


    //once the game has been lost, freezes the player's position and shows the losing screen, with an option to restart
    void gameLost()
    {
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
