/*=================================================================================================
 * FILE     : RepeatableEventObject.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 2/7/25
 * 
 * DESC     : Objects with this attached will play a cutscene whenever the player interacts with
 *            them. Currently only supports a single event.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatableEventObject : InteractableObject
{
    #region VARIABLES

    // Cutscene to play
    [SerializeField] Cutscene _objectEvent;

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Plays attached event
    /// </summary>
    public override void OnInteractedWith()
    {
        CutsceneManager.cutsceneManager.StartCutscene(_objectEvent);
    }

    #endregion
}
