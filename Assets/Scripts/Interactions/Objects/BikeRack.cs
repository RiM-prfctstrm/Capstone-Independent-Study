/*=================================================================================================
 * FILE     : BikeRack.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/14/23
 * UPDATED  : 11/20/24
 * 
 * DESC     : An interactable object that switches the player's movement state.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeRack : InteractableObject
{
    #region VARIABLES

    // Game Objects
    PlayerController _player;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Init Vars
        _player = PlayerController.playerController;
    }

    #endregion

    #region FUNCTIONALITY

    /// <summary>
    /// Changes whether the player is walking or biking
    /// </summary>
    public override void OnInteractedWith()
    {
        _player.ToggleBike();
    }

    #endregion
}
