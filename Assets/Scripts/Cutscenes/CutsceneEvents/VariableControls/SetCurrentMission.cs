/*=================================================================================================
 * FILE     : SetCurrentMission.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/8/25
 * UPDATED  : 2/8/25
 * 
 * DESC     : Sets the current mission.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Cutscene/Data management/Mission Select",
    order = 2)]
public class SetCurrentMission : CutsceneEvent
{
    #region VARIABLES

    // Mission to select
    [SerializeField] int _missionNo;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets the current mission
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();
        GlobalVariableTracker.currentMission = _missionNo;
    }

    #endregion
}
