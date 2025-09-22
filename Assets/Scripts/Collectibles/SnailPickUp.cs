/*=================================================================================================
 * FILE     : SnailPickUp.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/17/25
 * UPDATED  : 9/22/25
 * 
 * DESC     : Controls how Snails are saved and inventoried when picked up, as well as snail
 *            movement
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SnailPickUp : Collectible
{
    #region VARIABLES

    // State management
    [SerializeField] int _ID;

    // Movement

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    private void Awake()
    {
        // Checks whether snail has already been collected and destroys if so
        if (SnailSaveManager.collectedSnails.Contains(_ID))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
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

        // Adds to total
        GlobalVariableTracker.snailTotal++;

        // Updates Save Data
        SnailSaveManager.UpdateTempSave(_ID);
    }

    #endregion

    #region STATE TRACKER

    #endregion
}
