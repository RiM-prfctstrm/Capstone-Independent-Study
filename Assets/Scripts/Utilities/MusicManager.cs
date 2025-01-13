/*=================================================================================================
 * FILE     : MusicManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/6/24
 * UPDATED  : 1/13/25
 * 
 * DESC     : Controls which music is currently playing.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region VARIABLES
    // Manager Singleton
    public static MusicManager musicManager;

    // Music
    AudioClip _activeSong;

    // Object References
    [SerializeField] AudioSource _musicSource;

    #endregion

    #region PLAYBACK CONTROLS

    /// <summary>
    /// Used to change which music is playing, with safeties in place to prevent restarting the
    /// same song.
    /// </summary>
    /// <param name="song">The desired song to set</param>
    /// <param name="fadeout">Whether or not to fade out the original song</param>
    public void SwapSong(AudioClip song, bool fadeout)
    {
        // Cancels if the song would restart the one currently playing
        if (song == null || song == _activeSong)
        {
            return;
        }

        // Fades out old song before playing new one
        if (fadeout)
        {
            StartCoroutine(FadeOutSong(song));
            return;
        }

        // Starts new song
        BeginSong(song);
    }

    /// <summary>
    /// Begins a new song and informs the game which song is playing
    /// </summary>
    /// <param name="song">The song to play</param>
    void BeginSong(AudioClip song)
    {
        // Plays new song and sets it as active
        _activeSong = song;
        _musicSource.volume = .8f;
        _musicSource.clip = song;
        _musicSource.Play();
    }

    /// <summary>
    /// Fades out song by gradually incrementally lowering volume
    /// </summary>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeOutSong()
    {
        // Incrementally lowers volume
        while (_musicSource.volume > 0)
        {
            _musicSource.volume -= .008f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// Fades out song by gradually incrementally lowering volume, then starts a new one.
    /// </summary>
    /// <param name="song">Song to start once fade is complete</param>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeOutSong(AudioClip song)
    {
        Debug.Log("fading");
        // Incrementally lowers volume
        while (_musicSource.volume > 0)
        {
            _musicSource.volume -= 2 * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Starts new song
        BeginSong(song);
    }


    #endregion
}
