using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wholeGameManager : MonoBehaviour
{
    //attached to dataTransfer object (moved between scenes)

    //static ensures that regardless of how many copies of this script there are, there is only one copy of this variable
    static wholeGameManager instance;

    public int mostRecentScene;
    public bool mazeMinigameWon = false;
    public bool rainMinigameWon = false;
    

    private void Start()
    {
        //sets up this object to be moved between scenes
        DataTransfer();
    }

    
    private void Update()
    {
        //ensures that the levelLoader's minigame booleans always match the minigame results
        FindObjectOfType<levelLoader>().mazeGameWon = mazeMinigameWon;
        FindObjectOfType<levelLoader>().rainGameWon = rainMinigameWon;
    }


    public void DataTransfer()
    {
        Debug.Log("transferring data");
        //ensures that this is the first copy of the gameObject before saving it
        if (instance == null)
        {
            instance = this;

            //stops this gameobject (dataTransfer) from being destroyed when the scene changes
            DontDestroyOnLoad(this.gameObject);

            //collects another reference from any code from the start() function, so that it will be executed again when the scene changes
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SceneChanged(Scene arg0, Scene arg1)
    {
        //insert anything from the start function
        //(go to 11:00 of tutorial 6 for example)
    }
}
