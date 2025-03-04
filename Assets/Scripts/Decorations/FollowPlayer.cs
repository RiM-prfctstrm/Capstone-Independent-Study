/*=================================================================================================
 * FILE     : AttachToPlayer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/3/25
 * UPDATED  : 3/3/25
 * 
 * DESC     : Attaches current object to player, to track their position.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlayer : MonoBehaviour
{
    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets parent to player.
        transform.SetParent(PlayerController.playerController.transform);
    }

    #endregion
}
