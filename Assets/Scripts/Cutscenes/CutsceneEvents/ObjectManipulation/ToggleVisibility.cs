/*=================================================================================================
 * FILE     : ToggleVisibilty.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/11/24 (Veteran's Day [USA] / Singles' Day [China])
 * UPDATED  : 11/11/24
 * 
 * DESC     : Sets whether a GameObject's sprite is visible.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Visibility Toggle",
    menuName = "Cutscene/Object Manipulation/Visibility Toggle", order = 1)]
public class ToggleVisibility : CutsceneEvent
{
    #region VARIABLES

    // Input
    [SerializeField] int _targetID;
    [SerializeField] bool _setVisible;

    // Object Refs
    GameObject _target;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets target's visibility
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the object the script acts on
        _target = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];

        // Sets visibility
        if (_setVisible)
        {
            _target.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            _target.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Signals completion
        _eventComplete = true;
    }


    #endregion
}
