using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class dialogue
{
    //using this class as an object to be passed into the dialogueManager

    public string name; //name of NPC

    [TextArea(3, 10)]
    public string[] sentences;
}
