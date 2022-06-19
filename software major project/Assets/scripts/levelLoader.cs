using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    //script is attached to the InterfaceManager in each scene
    //(also attached to playerCharacter in MainScene)

    public int sceneToLoad;
    public Animator transition;
    float transitionDelay = 1.3f;

    public GameObject moveCheckUI;
    bool checkingMovement = false; //set to true when the player is being asked whether to move to minigame or not
    Collision2D boxCollision;

    //used to control the final end of the game
    public bool mazeGameWon;
    public bool rainGameWon;
    public bool endGame = false;

    //used to control whether the player can access a minigame through its box
    public bool npc2interaction = false;
    public bool npc3interaction = false;


    void Update()
    {
        //for transition FROM opening screen TO main scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            sceneToLoad = 1;

            if (Input.GetKeyDown("space"))
            {
                Debug.Log("space key pressed");
                LoadScene();
            }
        }

        //for transition FROM main scene TO any minigame
        if (checkingMovement == true)
        {
            //player is presented with options + can either move to minigame or stay in main scene
            if (Input.GetKeyDown("space"))
            {
                Time.timeScale = 1f;
                LoadScene();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1f;
                moveCheckUI.SetActive(false);
                checkingMovement = false;
            }
        }

        //for transition FROM maze minigame TO main scene
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            sceneToLoad = 1;

            //loads the new scene if space bar is pressed once the game is over
            if (Input.GetKeyDown("space"))
            {
                if (mazeGameWon == true)
                {
                    LoadScene();
                }
            }
        }

        //for transition FROM rain minigame TO main scene
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            sceneToLoad = 1;

            if (Input.GetKeyDown("space"))
            {
                if (rainGameWon == true)
                {
                    LoadScene();
                }
            }
        }

        //transition FROM main scene (or technically any scene but shouldn't be called unless in main scene) TO ending screen
        if (endGame == true)
        {
            sceneToLoad = 4;
            LoadScene();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        boxCollision = collision;

        //for transitions FROM main scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //transition TO maze minigame
            if (boxCollision.gameObject.tag == "mazeMinigameBox" && npc2interaction == true)
            {
                sceneToLoad = 2;

                //checks whether player wants to move to minigame
                moveCheckUI.SetActive(true);
                Time.timeScale = 0f;
                checkingMovement = true;
            }

            //transition TO rain minigame
            if (boxCollision.gameObject.tag == "rainMinigameBox" && npc3interaction == true)
            {
                sceneToLoad = 3;

                //checks whether player wants to move to minigame
                moveCheckUI.SetActive(true);
                Time.timeScale = 0f;
                checkingMovement = true;
            }
        }
    }

    public void LoadScene()
    {
        Debug.Log("loading scene");

        //records the scene that the player is moving out of, to be used in NPC interactions
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            FindObjectOfType<wholeGameManager>().mostRecentScene = SceneManager.GetActiveScene().buildIndex;
        }

        //changes scene to the desired scene using Build Index
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    //coroutine -> stops scene change from happening immediately, so transition can be shown
    IEnumerator LoadLevel(int levelIndex) 
    {
        Debug.Log(levelIndex);

        //plays the transition animation
        transition.SetTrigger("startTransition");

        Debug.Log("about to wait");
        //pauses this coroutine for certain amount of time before continuing
        yield return new WaitForSeconds(transitionDelay);

        Debug.Log("loading new scene");
        SceneManager.LoadScene(levelIndex);
    }
}
