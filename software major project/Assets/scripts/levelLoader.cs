using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    //script is attached to the InterfaceManager in each scene

    int sceneToLoad;
    public Animator transition;
    float transitionDelay = 1.3f;

    public GameObject moveCheckUI;
    bool checkingMovement = false; //set to true when the player is being asked whether to move or not
    Collision2D boxCollision;

    public bool gameWon = false;

    
    // Update is called once per frame
    void Update()
    {
        //for transition FROM opening screen TO main scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            sceneToLoad = 1;
            //then loads the new scene if space bar is pressed
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("space key pressed");
                LoadScene();
            }
        }

        //for transition FROM main scene TO any minigame
        if (checkingMovement == true)
        {
            //MAY BE A LATER ISSUE HERE with never setting checkingMovement back to false, but should be ok since scene changes
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

            //then loads the new scene if space bar is pressed once the game is over
            if (Input.GetKeyDown("space"))
            {
                if (gameWon == true)
                {
                    FindObjectOfType<mazeMinigameManager>().DataTransfer();
                    LoadScene();
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        boxCollision = collision;

        //for transitions FROM main scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //transition TO maze minigame
            if (boxCollision.gameObject.tag == "mazeMinigameBox")
            {
                sceneToLoad = 2;

                //checks whether player wants to move to minigame
                moveCheckUI.SetActive(true);
                Time.timeScale = 0f;
                checkingMovement = true;
            }
        }
    }

    public void LoadScene()
    {
        //changes scene to 'MainScene' using Build Index
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    //coroutine -> stops scene change from happening immediately, so transition can be shown
    IEnumerator LoadLevel(int levelIndex) 
    {
        //plays the transition animation
        transition.SetTrigger("startTransition");

        //pauses this coroutine for certain amount of time before continuing
        yield return new WaitForSeconds(transitionDelay);

        SceneManager.LoadScene(levelIndex);
    }
}
