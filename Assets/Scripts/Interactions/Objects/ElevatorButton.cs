/*=================================================================================================
 * FILE     : ElevatorButton.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/9/25
 * UPDATED  : 1/9/25
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
    [SerializeField] float[] _bgSpeeds;
    [SerializeField] int _polarity = 1; // 1 upward, -1 downward

    // References
    Transform _player;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _player = PlayerController.playerController.transform;
    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Begins functionality
    /// </summary>
    public override void OnInteractedWith()
    {
        // Sets elevator Vars
        ElevatorProgression.polarity = _polarity;
        foreach (float i in _bgSpeeds)
        {
            _bgSpeeds[i] = Mathf.Abs(_bgSpeeds[i]) * _polarity;
        }
        

    }

    /// <summary>
    /// Moves player to elevator and shaft and begins shaft motion
    /// </summary>
    /// <returns>Delay for fade</returns>
    IEnumerator StartElevator()
    {
        // Fades out and delays
        // Fades out
        ScreenEffects.fadingOut = true;
        yield return new WaitUntil(() => ScreenEffects.fadingOut == false);

        // Moves Player
        _player.position = new Vector2(_player.position.x + (30 * _polarity), _player.position.y);
    }

    #endregion
}
