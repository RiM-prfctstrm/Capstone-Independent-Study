/*=================================================================================================
 * FILE     : SetGlobalFlag.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 2/4/25
 * 
 * DESC     : Script that sets a specified global flag.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flag Switch", menuName = "Cutscene/Data management/Set Flag",
    order = 0)]
public class SetGlobalFlag : CutsceneEvent
{
    #region VARIABLES

    // Inputs 
    [SerializeField] string _flagName;
    [SerializeField] bool _value;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets flag specified in inspector
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        GlobalVariableTracker.progressionFlags[_flagName] = _value;

        eventComplete = true;
    }

    #endregion
}
