using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    //attached to empty LevelLoader object

    public Animator transition;
    float transitionDelay = 1.3f;
    
    // Update is called once per frame
    void Update()
    {
        //checks whether the player is still in the opening scene
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //then loads the new scene if space bar is pressed
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("space key pressed");
                LoadMainScene();
            }
        }
    }

    public void LoadMainScene()
    {
        //changes scene to 'MainScene' using Build Index
        StartCoroutine(LoadLevel(1));
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
