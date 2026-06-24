/*=================================================================================================
 * FILE     : CouchFishingInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/24/26
 * UPDATED  : 6/24/26
 * 
 * DESC     : Runs interaction for couchfishing.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchFishingInteraction : InteractableObject
{
    #region VARIABLES

    #endregion

    #region FUNCTIONALITY

    /// <summary>
    /// performs couchfishing event
    /// </summary>
    public override void OnInteractedWith()
    {
        FishSaveManager.LogFishName();
        FishSaveManager.SavePermanentData();
    }

    #endregion

}
