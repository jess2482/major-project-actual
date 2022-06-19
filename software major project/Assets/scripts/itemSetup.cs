using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ensures any 'Item' GameObject has a collider
[RequireComponent(typeof(BoxCollider2D))]
public class itemSetup : MonoBehaviour
{
    //script attached to any item in MazeMinigame

    public enum itemType { NONE, coin, pumpkin, strawberry, diamond}
    public itemType type;

    private void Reset()
    {
        //sets the collider to automatically be a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

}
