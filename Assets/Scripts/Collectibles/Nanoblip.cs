/*=================================================================================================
 * FILE     : Nanoblip.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/22/25
 * UPDATED  : 9/16/25
 * 
 * DESC     : Behaviour for items that can be picked up off the ground.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nanoblip : Collectible
{
    #region COLLECTION FUNCTIONALITY 

    /// <summary>
    /// Updates nanoblip tracking.
    /// </summary>
    protected override void OnPickUp()
    {
        base.OnPickUp();

        // Updates Nanoblip Managers
        CollectibleManager.collectibleManager.AdjustCount(1);
        localTracker.UpdateDict(collectibleID);
    }

    #endregion
}
