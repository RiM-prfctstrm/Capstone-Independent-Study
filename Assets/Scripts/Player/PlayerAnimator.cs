/*=================================================================================================
 * FILE     : PlayerAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/11/24
 * UPDATED  : 10/11/24
 * 
 * DESC     : Controls player character's animation
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    #region VARIABLES

    // Components
    PlayerController _playerController;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Sets components
        _playerController = GetComponentInParent<PlayerController>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        base.Update();

        // Plays animation
        anim.Play("Base Layer." + SetAnimState());
    }

    #endregion

    #region ANIMATION

    /// <summary>
    /// Determines which animation to play
    /// </summary>
    /// <returns>Name of animation to play</returns>
    public override string SetAnimState()
    {
        // Creates base string for walking anims.
        animState = base.SetAnimState();

        // Adds bike animations
        if (!_playerController.isWalking)
        {
            animState = "Bike" + animState;
        }


        // Returns animation to play
        return animState;
    }

    #endregion
}
