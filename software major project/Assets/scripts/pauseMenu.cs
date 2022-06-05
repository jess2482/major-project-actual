using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    //script attached to pauseMenu in MazeMinigame

    public static bool gameIsPaused = false;
    public static bool instructionsOpen = false;
    int currentScene = 1;

    //canvases containing pause menu UI elements
    public GameObject pauseMenuUI;
    public GameObject instructionsUI;

    private void Start()
    {
        Debug.Log("starting...");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (gameIsPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown("r"))
        {
            if (gameIsPaused == true)
            {
                RestartScene();
            }
        }

        if (Input.GetKeyDown("a"))
        {
            if (instructionsOpen == true)
            {
                PauseGame();
            }
            else
            {
                ShowInstructions();
            }
        }
    }

    //closes the pause menu
    void ResumeGame()
    {
        Debug.Log("resuming game");
        pauseMenuUI.SetActive(false);

        //resumes the game (1f is normal time/speed)
        Time.timeScale = 1f; 
        gameIsPaused = false;
    }

    //opens the pause menu
    void PauseGame()
    {
        Debug.Log("pausing game");
        pauseMenuUI.SetActive(true);
        instructionsUI.SetActive(false);

        //freezes the game (stops time) while the menu is open
        Time.timeScale = 0f;
    
        gameIsPaused = true;
        instructionsOpen = false;
    }

    void RestartScene()
    {
        Debug.Log("loading game");
        SceneManager.LoadScene(currentScene);
    }

    void ShowInstructions()
    {
        pauseMenuUI.SetActive(false);
        instructionsUI.SetActive(true);

        gameIsPaused = false;
        instructionsOpen = true;
    }

}
