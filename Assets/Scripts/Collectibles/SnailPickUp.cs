/*=================================================================================================
 * FILE     : SnailPickUp.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/17/25
 * UPDATED  : 9/17/25
 * 
 * DESC     : Controls how Snails are saved and inventoried when picked up, as well as snail
 *            movement
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPickUp : Collectible
{
    #region VARS

    #endregion

    #region UNIVERSAL EVENTS

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region COLLECTION FUNCTIONALITY

    /// <summary>
    /// Adds snail to player's inventory and marks corresponding SO as collected.
    /// </summary>
    protected override void OnPickUp()
    {
        base.OnPickUp();
    }

    #endregion
}
