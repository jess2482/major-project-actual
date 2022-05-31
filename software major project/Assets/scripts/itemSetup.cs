using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ensures any 'Item' GameObject has a collider
[RequireComponent(typeof(BoxCollider2D))]
public class itemSetup : MonoBehaviour
{
    public enum itemType { NONE, coin, pumpkin, strawberry, diamond}
    public itemType type;

    private void Reset()
    {
        //sets the collider to automatically be a trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    /*public void Interact()
    {
        switch (type)
        {
            case itemType.coin:
                Debug.Log("coin!");
                break;
            case itemType.pumpkin:
                Debug.Log("pumpkin");
                break;
            case itemType.strawberry:
                Debug.Log("strawberry");
                break;
            case itemType.egg:
                Debug.Log("egg");
                break;
        }
    }*/
}
