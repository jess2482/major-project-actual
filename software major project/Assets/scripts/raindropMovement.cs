 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raindropMovement : MonoBehaviour
{
    [SerializeField] //allows the private variable to be viewed in the inspector
    private float raindropSpeed = 3f;
    public GameObject instructionScreen;
    public rainTaskManager taskScript;

    private void Start()
    {
        instructionScreen = GameObject.Find("instructionScreen");
        taskScript = instructionScreen.GetComponent<rainTaskManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (taskScript.notStartedYet == false)
        {
            transform.position += Vector3.down * raindropSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "cloneDestroyer")
        {
            Destroy(this.gameObject); //maybe change to SetActive(false) later
        }
    }
}
