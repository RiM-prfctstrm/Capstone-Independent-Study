/*=================================================================================================
 * FILE     : ElevatorButton.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/9/25
 * UPDATED  : 5/3/25
 * 
 * DESC     : Starts Elevator Movement
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : InteractableObject
{
    #region VARIABLES

    // Parameters
    [SerializeField] ParallaxScroll[] _bgScrollers;
    [SerializeField] float[] _bgSpeeds;
    [SerializeField] Vector2[] _bgStarts;
    [SerializeField] float[] _bgStops;
    [SerializeField] int _polarity = 1; // 1 upward, -1 downward
    [SerializeField] int _startingLevel = -1;

    // References
    [SerializeField] AudioClip _ambience;
    Transform _player;


    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits vars
        _player = PlayerController.playerController.transform;
    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Begins functionality
    /// </summary>
    public override void OnInteractedWith()
    {
        StartCoroutine(StartElevator());
    }

    /// <summary>
    /// Moves player to elevator and shaft and begins shaft motion
    /// </summary>
    /// <returns>Delay for fade</returns>
    IEnumerator StartElevator()
    {
        // Sets direction and startpoint
        ElevatorProgression.polarity = _polarity;
        ElevatorProgression.level = _startingLevel;

        // Fades out and delays
        ScreenEffects.fadingOut = true;
        yield return new WaitUntil(() => ScreenEffects.fadingOut == false);

        // Starts ambience
        MusicManager.musicManager.SwapSong(_ambience, false);

        // Moves Player
        _player.position = new Vector2(_player.position.x + (50 * _polarity), _player.position.y);

        // Sets backgorund objects in motion
        int j = 0;
        foreach (ParallaxScroll i in _bgScrollers)
        {
            // Sets motion params
            i.rate = _bgSpeeds[j] * -_polarity;
            i.tpDest = _bgStarts[j];
            i.maximum = _bgStops[j];

            // Starts movement
            i.inMotion = true;

            // Iterates loop
            j++;
        }

        // Fades back in
        ScreenEffects.fadingIn = true;
    }

    #endregion
}
