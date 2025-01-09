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
    [SerializeField] int _polarity = 1; // 1 upward, -1 downward

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Handles Player interaction
    /// </summary>
    public override void OnInteractedWith()
    {
        // Tells which direction for elevator to go
        ElevatorProgression.polarity = _polarity;

        

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator StartElevator()
    {

    }

    #endregion
}
