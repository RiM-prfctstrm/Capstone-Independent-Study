/*=================================================================================================
 * FILE     : AnimateCharacter.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/7/24
 * UPDATED  : 11/8/24
 * 
 * DESC     : Forces a character into a specified animation, overriding their default programming.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation Command",
    menuName = "Cutscene/Character Controls/Animation", order = 1)]
public class AnimateCharacter : CutsceneEvent
{
    #region VARIABLES

    // Inputs
    [SerializeField] int _targetID;
    [SerializeField] string _animName;
    [SerializeField] int _facingDirection;

    // Object Refs
    GameObject _targetCharacter;
    CharacterAnimator _targetAnimator;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets a character's animation and direction
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the character the script acts on
        _targetCharacter = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];
        _targetAnimator = _targetCharacter.GetComponent<CharacterAnimator>();
        

        // Sets direction and animation
        _targetAnimator.facingDirection = _facingDirection;
        _targetAnimator.PlayScriptedAnimation(_animName);

        // Signal completion
        _eventComplete = true;
    }

    #endregion
}
