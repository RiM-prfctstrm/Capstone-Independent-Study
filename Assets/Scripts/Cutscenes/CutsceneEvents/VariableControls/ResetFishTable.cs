/*=================================================================================================
 * FILE     : ResetFishTable.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/25/26
 * UPDATED  : 6/25/26
 * 
 * DESC     : Generates and sets new table for couchfishing, while also resetting order for table
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishGen", menuName = "Cutscene/Data management/Fish Reset",
    order = 6)]
public class ResetFishTable : CutsceneEvent
{
    #region VARIABLES

    [SerializeField] FishLootTable _newTable;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Generates new fish table
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets new table
        FishSaveManager.SetRandomTable(_newTable);
        FishSaveManager.gachaOrder = 0;
        FishSaveManager.SaveOrder();

        // Signals completion
        eventComplete = true;
    }

    #endregion
}
