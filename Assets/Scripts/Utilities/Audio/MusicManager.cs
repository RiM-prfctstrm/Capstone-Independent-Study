/*=================================================================================================
 * FILE     : MusicManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/6/24
 * UPDATED  : 8/22/25
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
    [SerializeField] MusicTrack[] _missionThemes = new MusicTrack[4];

    // Object References
    [SerializeField] AudioSource _musicSource;

    // Playback Data
    bool _isLoop;
    float _startTime;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Update is called once every frame
    /// </summary>
    private void Update()
    {
        // loops song
        if (!_musicSource.isPlaying)
        {
            _musicSource.time = _startTime;
            _musicSource.Play();
        }
    }

    #endregion

    #region PLAYBACK CONTROLS

    /// <summary>
    /// Used to change which music is playing, with safeties in place to prevent restarting the
    /// same song.
    /// </summary>
    /// <param name="song">The desired song to set</param>
    /// <param name="fadeout">Whether or not to fade out the original song</param>
    /// <param name="useMissionTheme">Whether the specific song played varies based on the current
    /// mission</param>
    public void SwapSong(MusicTrack song, bool fadeout, bool useMissionTheme)
    {
        // Sets overworld music to match current mission
        if (useMissionTheme)
        {
            song = _missionThemes[GlobalVariableTracker.currentMission];
        }

        // Cancels if the song would restart the one currently playing
        if ((song == null || song.song == _activeSong))
        {
            return;
        }

        // Fades out old song before playing new one
        if (fadeout)
        {
            StartCoroutine(FadeOutSong(song.song));
            return;
        }

        // Starts new song
        BeginSong(song.song);
        SetLoopPoint(song);
    }


    /// <summary>
    /// Begins a new song and informs the game which song is playing
    /// </summary>
    /// <param name="song">The song to play</param>
    void BeginSong(AudioClip song)
    {
        // Ensures volume is at correct level
        if (_musicSource.volume != 1)
        {
            _musicSource.volume = 1;
        }

        // Plays new song and sets it as active
        _activeSong = song;
        _musicSource.clip = song;
        _musicSource.Play();
    }

    /// <summary>
    /// Fades out song by gradually incrementally lowering volume
    /// </summary>
    /// <returns>Framerate delay for fading</returns>
    public IEnumerator FadeOutSong()
    {
        // Incrementally lowers volume
        while (_musicSource.volume > 0)
        {
            _musicSource.volume -= .5f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Tells game that no song is playing
        _activeSong = null;
        _musicSource.clip = _activeSong;
    }

    /// <summary>
    /// Fades out song by gradually incrementally lowering volume, then starts a new one.
    /// </summary>
    /// <param name="song">Song to start once fade is complete</param>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeOutSong(AudioClip song)
    {
        // Incrementally lowers volume
        while (_musicSource.volume > 0)
        {
            _musicSource.volume -= 2 * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Starts new song
        BeginSong(song);
    }

    #region LOOPING CONTROLS

    /// <summary>
    /// Sets whether the song
    /// </summary>
    /// <param name="active"></param>
    public void SetLooping(bool active)
    {
        _isLoop = active;
    }

    /// <summary>
    /// Tells the audio source where it should start a track's loop.
    /// </summary>
    /// <param name="trackData">container for bpm and loop point data</param>
    void SetLoopPoint(MusicTrack trackData)
    {
        // Resets to full loop when there is no intro
        if (trackData.bpm == 0)
        {
            _musicSource.time = 0;
            return;
        }

        // Vars
        float secondsPerBeat = 60f / trackData.bpm;
        _startTime = secondsPerBeat * trackData.loopBeat;
    }

    #endregion

    #endregion
}
