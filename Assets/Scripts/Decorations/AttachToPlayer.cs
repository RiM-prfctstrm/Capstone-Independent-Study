/*=================================================================================================
 * FILE     : FollowPlayer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/3/25
 * UPDATED  : 3/3/25
 * 
 * DESC     : Matches attached object's position with player's.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    #region VARIABLES

    // Offset Vector
    [SerializeField] Vector3 _offset;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Update is called oncer per frame.
    /// </summary>
    void Update()
    {
        // Matches attached object's position with player's. I know it's more efficient to just set
        // the player as its parent, but I'm too lazy to figure out how to deal with it carrying
        // into other scenes right now.
        transform.position = PlayerController.playerController.transform.position + _offset;
    }

    #endregion
}
