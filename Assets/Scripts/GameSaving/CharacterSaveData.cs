using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//since we want to reference this data for every save file
// this script is not a monobehavior and is instance serializable
public class CharacterSaveData 
{
    [Header("Character Name")]
    public string CharacterName;

    [Header("Time Played")]
    public float secondPlayed;

    // can not use vector3 for saving because only save data form basic variable type (float, int, string, bool)
    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
