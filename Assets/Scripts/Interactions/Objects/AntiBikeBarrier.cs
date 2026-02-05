/*=================================================================================================
 * FILE     : AntiBikeBarrier.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/5/26
 * UPDATED  : 2/5/26
 * 
 * DESC     : Causes attached collider to only respond to the player's bike
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBikeBarrier : MonoBehaviour
{
    #region VARIABLES

    // Components
    BoxCollider2D _collider;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits Vars
        _collider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Matches activity to walking state
        if (PlayerController.playerController.isWalking && _collider.enabled)
        {
            _collider.enabled = false;
        }
        if (!PlayerController.playerController.isWalking && !_collider.enabled)
        {
            _collider.enabled = true;
        }
    }

    #endregion
}
