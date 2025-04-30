/*=================================================================================================
 * FILE     : AlterCollectibleCount.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 2/4/25
 * 
 * DESC     : Script that sets a specified global flag.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Alter Count", menuName = "Cutscene/Data management/Collectible" +
    " Modifier", order = 4)]
public class AlterCollectibleCount : CutsceneEvent
{
    #region VARIABLES

    // Amount to change collectible count
    [SerializeField] int _amount;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Removes collectibles from player.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Ensures the player can't lose more than they have
        _amount = Mathf.Clamp(_amount, -(int)GlobalVariableTracker.collectiblesInPocket, 9999);

        // Performs modification
        CollectibleManager.collectibleManager.AdjustCount(_amount);
        eventComplete = true;
    }

    #endregion
}
