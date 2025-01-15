/*=================================================================================================
 * FILE     : SetEventMusic.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/6/24
 * UPDATED  : 1/15/25
 * 
 * DESC     : Plays music for a cutscene
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "Cutscene/Music", order = 5)]
public class SetEventMusic : CutsceneEvent
{
    #region VARIABLES

    // Music Parameters
    [SerializeField] AudioClip _song;
    [SerializeField] bool _useFadeOut = false;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets the cutscene music
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        if (_song != null)
        {
            // Swaps new song in
            MusicManager.musicManager.SwapSong(_song, _useFadeOut);
        }
        else
        {
            // Ends all music
            CutsceneManager.cutsceneManager.StartCoroutine(
                MusicManager.musicManager.FadeOutSong());
        }

        eventComplete = true;
    }

    #endregion
}
