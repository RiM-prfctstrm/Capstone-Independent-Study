/*=================================================================================================
 * FILE     : TimerStartTrigger.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 4/19/25
 * 
 * DESC     : Starts countdown timer when player enters.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStartTrigger : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] int _minutes;
    [SerializeField] int _seconds;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        _seconds += _minutes * 60;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If timer is not already running, begins timer
        if(!TimerController.timerInProgress)
        {
            TimerController.timerController.BeginTimer(_seconds);
        }

        // Destroys object to prevent repeat collisions
        Destroy(gameObject);
    }

    #endregion
}
