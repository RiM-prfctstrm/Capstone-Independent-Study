/*=================================================================================================
 * FILE     : ChangeActiveState.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/12/25
 * UPDATED  : 2/12/25
 * 
 * DESC     : Sets whether a GameObject is active.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Active State Toggle",
    menuName = "Cutscene/Object Manipulation/Active State Toggle", order = 1)]
public class ChangeActiveState : CutsceneEvent
{
    #region VARIABLES

    // Input
    [SerializeField] int _targetID;
    [SerializeField] bool _setActive;

    // Object Refs
    GameObject _target;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets target's active state
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the object the script acts on
        _target = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];

        // Sets active state
        _target.SetActive(_setActive);

        // Signals completion
        eventComplete = true;
    }

    #endregion
}
