using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    //script attached to pauseMenu in MainScene, MazeMinigame, and RainMinigame

    public static bool gameIsPaused = false;
    public static bool instructionsOpen = false;
    int currentScene;

    //canvases containing pause menu UI elements
    public GameObject pauseMenuUI;
    public GameObject instructionsUI;

    private void Start()
    {
        Time.timeScale = 1f;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        gameIsPaused = false;
    }

    
    void Update()
    {
        //opens/closes the pause menu when player presses P
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

        //shifts between pause menu + instruction screen upon player input
        if (Input.GetKeyDown("a"))
        {
            if (gameIsPaused == true || instructionsOpen == true)
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
    }

    //called from Update
    //closes the pause menu
    void ResumeGame()
    {
        Debug.Log("resuming game");
        pauseMenuUI.SetActive(false);

        //resumes the game (1f is normal time/speed)
        Time.timeScale = 1f; 
        gameIsPaused = false;
    }

    //called from Update
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

    //called from Update
    void RestartScene()
    {
        Debug.Log("loading game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        /*FindObjectOfType<levelLoader>().sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        FindObjectOfType<levelLoader>().LoadScene();*/
    }

    //called from Update
    void ShowInstructions()
    {
        pauseMenuUI.SetActive(false);
        instructionsUI.SetActive(true);

        gameIsPaused = false;
        instructionsOpen = true;
    }

}
