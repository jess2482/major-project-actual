 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raindropMovement : MonoBehaviour
{
    //attached to raindrop prefab (used in RainMinigame)

    [SerializeField] //allows the private variable to be viewed in the inspector
    private float raindropSpeed = 3f;
    public GameObject instructionScreen;
    public rainTaskManager taskScript;

    private void Start()
    {
        instructionScreen = GameObject.Find("instructionScreen");
        taskScript = instructionScreen.GetComponent<rainTaskManager>();
    }

    
    void Update()
    {
        //once the minigame starts, any raindrops that are spawned immediately fall downwards
        if (taskScript.notStartedYet == false)
        {
            transform.position += Vector3.down * raindropSpeed * Time.deltaTime;
        }
    }

    //each raindrop destroyed once it hits the player OR falls out of view of the screen
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "cloneDestroyer")
        {
            Destroy(this.gameObject);
        }
    }
}
