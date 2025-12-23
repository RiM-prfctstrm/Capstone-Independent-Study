/*=================================================================================================
 * FILE     : ResetCollectibles.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/28/25
 * UPDATED  : 12/23/25
 * 
 * DESC     : Erases save files that store which collectibles have been picked up.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "ColResetter", menuName = "Cutscene/Data management/Collectible Reset",
    order = 5)]
public class ResetCollectibles : CutsceneEvent
{
    #region VARS

    // Parameters
    [SerializeField] bool _eraseSnails = false;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Erases files containing data on which collectibles have been picked up
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Resets collectibles
        if (_eraseSnails)
        {
            SnailSaveManager.collectedSnails.Clear();
        }
        CollectibleManager.collectibleManager.ResetObjectStatus(_eraseSnails);

        // Signals completion
        eventComplete = true;
    }

    #endregion
}
