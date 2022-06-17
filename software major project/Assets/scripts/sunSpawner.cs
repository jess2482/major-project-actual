using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunSpawner : MonoBehaviour
{
    //script attached to the sunSpawner object in RainMinigame

    [SerializeField]
    private float[] xPositions;
    [SerializeField]
    private float[] yPositions;
    [SerializeField]
    private GameObject sunPrefab;

    List<float> remainingXPositions = new List<float>();
    List<float> remainingYPositions = new List<float>();
    float xPos = 0f;
    float yPos = 0f;
    int rand;

    private void Start()
    {
        remainingXPositions.AddRange(xPositions);
        remainingYPositions.AddRange(yPositions);

        //starts the game with three sun objects
        for (int i = 0; i < 2; i++)
        {
            SpawnNewSun();
        }
    }

    //called from the sunCollection script
    public void SpawnNewSun()
    {
        FindSunPosition();
        Collider2D movementBlocked = Physics2D.OverlapCircle(new Vector2(xPos, yPos), 0.2f);
        if (!movementBlocked)
        {
            GameObject sunObject = Instantiate(sunPrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);
        }
        else
        {
            SpawnNewSun();
        }
    }

    void FindSunPosition()
    {
        //finds random position on the grid (in terms of x/y) + assigns it to the new sun
        rand = Random.Range(0, remainingXPositions.Count);
        xPos = remainingXPositions[rand];
        remainingXPositions.RemoveAt(rand);

        rand = Random.Range(0, remainingYPositions.Count);
        yPos = remainingYPositions[rand];
        remainingYPositions.RemoveAt(rand);

        //resets the position lists
        remainingXPositions = new List<float>();
        remainingXPositions.AddRange(xPositions);

        remainingYPositions = new List<float>();
        remainingYPositions.AddRange(yPositions);
    }
}
