using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mazeMinigameManager : MonoBehaviour
{
    //static ensures that regardless of how many copies of this script there are, there is only one copy of this variable
    static mazeMinigameManager instance;

    int transferCheck = 5;
    bool gameWon = false;

    private void Start()
    {
        Debug.Log(transferCheck);
        Debug.Log(gameWon);
    }


    public void DataTransfer()
    {
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
        //implement any code from start function
        //https://www.youtube.com/watch?v=ncN4HVo7K28 for more info [11:00]

        Debug.Log(transferCheck);
        Debug.Log(gameWon);
    }
}
