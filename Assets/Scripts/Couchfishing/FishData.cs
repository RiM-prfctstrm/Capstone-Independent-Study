/*=================================================================================================
 * FILE     : FishData.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/11/26
 * UPDATED  : 6/11/26
 * 
 * DESC     : ScriptableObject that stores data for fish.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "Couchfishing/Fish", order = 1)]
public class FishData : ScriptableObject
{
    #region VARIABLES

    // Display Details
    public string fishName;
    public Sprite sprite;

    // Collection Effects
    public MusicTrack pickUpJingle;
    public DialogueEvent pickUpText;
    public Vector2 pickUpOffset;

    #endregion
}
