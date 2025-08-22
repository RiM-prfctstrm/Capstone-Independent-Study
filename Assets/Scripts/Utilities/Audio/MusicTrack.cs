/*=================================================================================================
 * FILE     : MusicTrack.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/22/25
 * UPDATED  : 8/22/25
 * 
 * DESC     : Stores data for music to control playback by the song
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicTrack : ScriptableObject
{
    #region VARS

    // Song Audio
    [SerializeField] AudioClip _song;
    public AudioClip song => _song;

    // Control parameters
    //[SerializeField] bool _loops = true;
    [SerializeField] int _loopBeat = 0;
    public int loopBeat => _loopBeat;

    // Information used to calculate precise timing
    [SerializeField] int _bpm; // can be left at 0 if _loopBeat is also 0
    public int bpm => _bpm;

    #endregion
}
