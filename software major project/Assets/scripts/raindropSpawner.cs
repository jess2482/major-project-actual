using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raindropSpawner : MonoBehaviour
{
    //public static raindropSpawner instance;

    [SerializeField]
    private float xLimit; //used when spawning one raindrop
    [SerializeField]
    private float[] xPositions;
    [SerializeField]
    private GameObject[] rainPrefabs;
    [SerializeField]
    private Wave[] possibleWaves;

    //private float currentTime; //time passed when the wave starts
    List<float> remainingPositions = new List<float>();
    private int waveIndex; //chosen randomly, determines which wave to spawn (so how many raindrops)
    float xPos = 0;
    int rand;

    rainTaskManager taskScript;
    public Canvas instructionScreen;

    private IEnumerator waitCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        taskScript = instructionScreen.GetComponent<rainTaskManager>();
        waitCoroutine = WaitToSpawn();
        StartCoroutine(waitCoroutine);

        //currentTime = 0f; //wave will start as soon as the game starts
        remainingPositions.AddRange(xPositions); //adds the values -5.5 to 5.5 to the list
    }

    // Update is called once per frame
    void Update()
    {
        if (taskScript.notStartedYet == false && taskScript.winGame == false)
        {
            WaitToSpawn();
        }
    }

    IEnumerator WaitToSpawn()
    {
        while (taskScript.winGame == false)
        {
            yield return new WaitForSeconds(1);
            SelectWave();
        }
    }


    //called from SelectWave function
    void SpawnEnemy(float xPos)
    {
        // spawns the enemy at the same position as the enemySpawner, with no rotation
        GameObject rainObject = Instantiate(rainPrefabs[0], new Vector3(xPos, transform.position.y, 0), Quaternion.identity); 

        //go to 11:30 of https://www.youtube.com/watch?v=9YEd4Fq943s if you want to include diff. types of raindrops/enemies
    }

    void SelectWave()
    {
        waveIndex = Random.Range(0, possibleWaves.Length);

        //currentTime = possibleWaves[waveIndex].delayTime; //?

        //if only one enemy is going to be spawned
        if (possibleWaves[waveIndex].spawnAmount == 1)
        {
            xPos = Random.Range(-xLimit, xLimit);
            SpawnEnemy(xPos);
        }
        else if (possibleWaves[waveIndex].spawnAmount > 1)
        {
            rand = Random.Range(0, remainingPositions.Count);
            xPos = remainingPositions[rand];
            remainingPositions.RemoveAt(rand); //ensures that once a position is used, it won't be used again

            for (int i = 0; i < possibleWaves[waveIndex].spawnAmount; i++)
            {
                SpawnEnemy(xPos);
                rand = Random.Range(0, remainingPositions.Count);
                xPos = remainingPositions[rand];
                remainingPositions.RemoveAt(rand);
            }
        }

        //resets the possible positions that the raindrop can fall in (after they were removed above)
        remainingPositions = new List<float>();
        remainingPositions.AddRange(xPositions); 

    }

}

[System.Serializable]
public class Wave
{
    //wave of multiple raindrops at once

    public float delayTime;
    public float spawnAmount;
}
