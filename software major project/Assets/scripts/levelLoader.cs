using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    int sceneToLoad;

    public Animator transition;
    float transitionDelay = 1.3f;
    
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
                LoadMainScene();
            }
        }

        //for transition FROM maze minigame TO main scene
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            sceneToLoad = 1;
            
            //then loads the new scene if space bar is pressed
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("space key pressed");
                FindObjectOfType<mazeMinigameManager>().DataTransfer();
                LoadMainScene();
            }
        }
    }

    public void LoadMainScene()
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
