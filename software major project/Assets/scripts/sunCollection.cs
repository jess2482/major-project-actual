using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sunCollection : MonoBehaviour
{
    //script attached to playerCharacter in RainMinigame

    int sunScore = 0;
    public sunSpawner spawnScript;
    public rainTaskManager taskScript;
    public Text scoreText;

    private void Update()
    {
        displayScore(sunScore);
        if (sunScore >= 20)
        {
            taskScript.winGame = true;
        }
    }

    void displayScore(int score)
    {
        scoreText.text = string.Format("{0}", score);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Destroy(collision.gameObject);
            sunScore += 1;
            Debug.Log(sunScore);
            spawnScript.SpawnNewSun();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "raindrop")
        {
            if (sunScore > 0)
            {
                sunScore -= 1;
                Debug.Log(sunScore);
            }
        }
    }
}
