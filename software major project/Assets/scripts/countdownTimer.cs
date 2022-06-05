using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdownTimer : MonoBehaviour
{
    //script attached to timerText in MazeMinigame

    public float timeLeft = 40;
    public Text timeText;
    public GameObject instructionScreen;
    taskManager taskScript;


    void Start()
    {
        taskScript = instructionScreen.GetComponent<taskManager>();
    }


    void Update()
    {
        //counts down while the player still has time left, then stops and ends the game once the time runs out
        if (timeLeft > 0)
        {
            //subtracts the duration of the previous frame from the time left
            //deltaTime ensures it counts down evenly/smoothly
            timeLeft -= Time.deltaTime;

        }
        else
        {
            timeLeft = 0;
            taskScript.loseGame=true;
        }

        displayTime(timeLeft);
    }


    void displayTime(float timeToDisplay) 
    {
        //because deltaTime will cause the time to go slightly below zero, ensures GUI doesn't glitch/display that
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            //ensures timer never shows 00:00 while the player can still continue
            timeToDisplay += 1;
        }

        //Mathf.FloorToInt rounds down, so no decimals
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        //{} values are parameters - essentially '{minutes:two digits}:{seconds:two digits}'
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
