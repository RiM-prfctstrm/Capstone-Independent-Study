/*=================================================================================================
 * FILE     : AnimateCharacter.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/7/24
 * UPDATED  : 11/7/24
 * 
 * DESC     : Forces a character into a specified animation, overriding their default programming.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : CutsceneEvent
{
    #region VARIABLES

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets a character's animation and direction
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();
    }

    #endregion
}
