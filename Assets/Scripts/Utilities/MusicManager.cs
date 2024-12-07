/*=================================================================================================
 * FILE     : MusicManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/6/24
 * UPDATED  : 12/7/24
 * 
 * DESC     : Keeps track of which songs are in circulation using a tiered system to determine
 *            which music is most important to play.
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

    /* Tier System for Later Use 
    // List of songs for that may currently be active. Each position in the array corresponds to a
    // different tier. Tier 0 is used for events, 1 for location-specific music, and 2 for generic
    // overworld music.
    //[SerializeField] AudioClip[] _songBank = new AudioClip[3]; */
    

    // Tier Controls
    //public static int activeTier;

    // Object References
    [SerializeField] AudioSource _musicSource;

    #endregion

    #region PLAYBACK CONTROLS

    /// <summary>
    /// Begins a new song
    /// </summary>
    /// <param name="song">The desired song to set</param>
    /// <param name="tier">The tier to play the song on</param>
    public void BeginSong(AudioClip song, bool fadeout)
    {
        // Cancels if the song would restart the one currently playing
        if (song != null && song == _activeSong)
        {
            return;
        }

        // Fades out old song before playing new one
        if (fadeout || song == null)
        {
            while (_musicSource.volume > 0)
            {
                _musicSource.volume -= .008f * Time.fixedDeltaTime;
            }
        }

        // Stops music if song is null
        if (song == null)
        {
            _musicSource.Stop();
            _activeSong = null;
            return;
        }

        // Plays new song and sets it as active
        _activeSong = song;
        _musicSource.volume = .8f;
        _musicSource.clip = song;
        _musicSource.Play();
    }

    #endregion
}
