/*=================================================================================================
 * FILE     : CollectibleStateTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/25
 * UPDATED  : 8/27/25
 * 
 * DESC     : Tracks whether collectibles in the current scene have been picked up or not.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleStateTracker : MonoBehaviour
{
    #region VARS

    // Data Storage
    Dictionary<int, bool> _collectibleStatus;
    List<Collectible> _collectibleObjects;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Compiles collectibles in scene
        foreach (Collectible i in FindObjectsOfType<Collectible>())
        {
            _collectibleObjects.Add(i);
        }

        // Loads dict data

        // DEBUG Recompiles list after scene changed in editor
        // Determines whether to create a new collectible dictionary
        if (_collectibleObjects.Count != _collectibleStatus.Count)
        {
            CreateNewStatusDict();
        }
    }

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Rebuilds dictionary of collectible status
    /// </summary>
    void CreateNewStatusDict()
    {
        // Vars
        int IDNo = 0;

        // Destroys old dict
        _collectibleStatus.Clear();

        // 
    }

    #region SAVE MANAGEMENT

    #endregion

    #endregion
}
