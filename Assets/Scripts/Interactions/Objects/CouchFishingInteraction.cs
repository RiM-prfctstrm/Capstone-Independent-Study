/*=================================================================================================
 * FILE     : CouchFishingInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/24/26
 * UPDATED  : 6/25/26
 * 
 * DESC     : Runs interaction for couchfishing.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchFishingInteraction : RepeatableEventObject
{
    #region VARIABLES

    // Events
    [SerializeField] Cutscene _outOfFishEvent;

    // Fish Display
    [SerializeField] CutsceneDialogue _flavorText;
    [SerializeField] GetDummyItem _displayEvent;
    [SerializeField] PlayerAnimator _animator;

    #endregion

    #region FUNCTIONALITY

    /// <summary>
    /// Performs couchfishing event.
    /// </summary>
    public override void OnInteractedWith()
    {
        if (FishSaveManager.gachaOrder < FishSaveManager.gachaTable.Count)
        {
            Couchfishing();
        }
        else
        {
            NoBitesEvent();
        }
        
    }

    /// <summary>
    /// Runs an event that gives the player a fish as a fake item.
    /// </summary>
    void Couchfishing()
    {
        // Vars
        FishData caughtFish = FishSaveManager.gachaTable[FishSaveManager.gachaOrder];

        // Sets display information
        _displayEvent._prize = caughtFish.sprite;
        _flavorText.cutsceneDialogue = caughtFish.pickUpText;

        // Triggers Couchfishing event
        FishSaveManager.gachaOrder++;
        FishSaveManager.SavePermanentData();
        CutsceneManager.cutsceneManager.StartCutscene(_objectEvent);
    }

    /// <summary>
    /// Runs a modified version of the Couchfishing event containing no fish.
    /// </summary>
    void NoBitesEvent()
    {
        CutsceneManager.cutsceneManager.StartCutscene(_outOfFishEvent);
    }

    #endregion

}
