using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    //script attached to the Main Camera in every scene

    GameObject thePlayer;
    private Transform playerTransform;

    //not used for now, but could be used later to change where the playerCharacter is in the camera's view
    public float offset=0f; 

    void Start()
    {
        //gets the player's current position
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = thePlayer.transform;
    }


    //updates after Update() and FixedUpdate() functions, so that the camera will not lag + will always follow the player correctly
    void LateUpdate()
    {
        //storing camera's current position in temporary variable
        Vector3 cameraPosition = transform.position;

        //sets the temporary position to always match/align with the player's
        cameraPosition.x = playerTransform.position.x;
        cameraPosition.x += offset;
        cameraPosition.y = playerTransform.position.y;

        //sets the camera's position to temporary values (because 'transform.position.x' cannot be accessed directly)
        transform.position = cameraPosition;
    }
}
