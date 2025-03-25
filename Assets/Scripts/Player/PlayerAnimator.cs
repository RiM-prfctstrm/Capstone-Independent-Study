/*=================================================================================================
 * FILE     : PlayerAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/11/24
 * UPDATED  : 3/25/25
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

    // Control Parameters
    bool _overrideState = false;

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
        if (!CutsceneManager.inCutscene && !_overrideState)
        {
            _anim.Play("Base Layer." + SetAnimState());
        }
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

    /// <summary>
    /// Puts the player in a spin out animation for a duration based on force of impact
    /// </summary>
    /// <returns>Delay while the player must keep spinning</returns>
    public IEnumerator SpinOut(float intensity)
    {
        // Overrides ordinary animation and gameplay
        _overrideState = true;

        // Sets animation
        _anim.Play("Base Layer.SpinOut");

        // Tells how long to keep state
        yield return new WaitForSeconds(intensity);

        // Disables helpless state
        _overrideState = false;
    }

    /// <summary>
    /// Plays a scripted animation controlled outside normal context, and sets interaction hitbox
    /// to match it.
    /// </summary>
    /// <param name="name">Name of the animation that plays</param>
    public override void PlayScriptedAnimation(string name)
    {
        base.PlayScriptedAnimation(name);

        // Updates detection hitbox
        _playerController.UpdateDetection();
    }

    #endregion
}
