/*=================================================================================================
 * FILE     : NPCAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 11/2/24
 * 
 * DESC     : Controls NPCs' animations
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : CharacterAnimator
{
    #region VARIABLES

    // Objects
    GameObject _player;
    PlayerAnimator _playerAnim;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Init Vars
        _player = PlayerController.playerController.gameObject;
        _playerAnim = _player.GetComponent<PlayerAnimator>();

        // Init Direction
        _anim.Play(SetAnimState());
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    #endregion

    #region BASIC FUNCTIONS

    /// <summary>
    /// Sets the NPC's Facing Direction to be the opposite of the player's
    /// </summary>
    public void FacePlayer()
    {
        // Determines direction based on player's
        switch (_playerAnim.facingDirection)
        {
            case 0:
                facingDirection = 3;
                break;

            case 1:
                facingDirection = 2;
                break;

            case 2:
                facingDirection = 1;
                break;

            case 3:
                facingDirection = 0;
                break;
        }

        // Sets animation
        _anim.Play(SetAnimState());
    }

    #endregion
}
