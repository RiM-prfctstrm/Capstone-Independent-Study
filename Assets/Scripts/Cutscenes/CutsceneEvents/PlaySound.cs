/*=================================================================================================
 * FILE     : PlaySound.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/25/25
 * UPDATED  : 3/25/25
 * 
 * DESC     : Plays a scripted sound effect
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Cutscene/Sound Effect", order = 7)]
public class PlaySound : CutsceneEvent
{
    #region VARIABLES

    // Sound effect to play
    [SerializeField] AudioClip _soundEffect;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Plays Sound Effect
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Plays sound
        PlayerController.playerController.playerAudioSource.PlayOneShot(_soundEffect);

        // Signals event completion
        eventComplete = true;
    }

    #endregion
}
