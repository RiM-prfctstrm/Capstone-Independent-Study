/*=================================================================================================
 * FILE     : ResetProgress.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/17/25
 * UPDATED  : 4/1/25
 * 
 * DESC     : Used to reset GlobalVariableTracker to its default state
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reset", menuName = "Cutscene/Data management/Reset Progress",
    order = 3)]
public class ResetProgress : CutsceneEvent
{
    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Resets all variables in GlobalVariableTracker using the debug injector.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Resets variables
        GlobalVariableTracker.ResetAllProgress();

        // Marks completion
        eventComplete = true;
    }

    #endregion
}
