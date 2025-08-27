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
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleStateTracker : MonoBehaviour
{
    #region VARS

    // Data Containers
    Dictionary<int, bool> _collectibleStatus = new Dictionary<int, bool>();
    List<Collectible> _collectibleObjects = new List<Collectible>();

    // Save containers
    // File Names
    string _saveFileName;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Inits vars
        _saveFileName = Application.dataPath + "/SaveData/Collectibles/" +
            SceneManager.GetActiveScene().name + ".txt";

        // Compiles collectibles in scene
        foreach (Collectible i in GetComponentsInChildren<Collectible>())
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

        // Loop to gather and set IDs for each collectible
        foreach (Collectible i in _collectibleObjects)
        {
            // Assigns unique ID number to each object
            i.collectibleID = IDNo;
            i.localTracker = this;
            _collectibleStatus.Add(IDNo, false);
            IDNo++;
        }

        // Saves status (DEBUG???)
        SavePickupState();
    }

    /// <summary>
    /// Changes collected status for individual collectibles
    /// </summary>
    public void UpdateDict(int IDKey)
    {
        _collectibleStatus[IDKey] = true;
    }

    /// <summary>
    /// Destroys all collectibles that have already been picked up
    /// </summary>
    void RemoveCollectedObjs()
    {
        foreach(KeyValuePair<int, bool> i in _collectibleStatus)
        {
            if (i.Value == true)
            {
                Destroy(_collectibleObjects[i.Key].gameObject);
            }
        }
    }

    #region SAVE MANAGEMENT

    /// <summary>
    /// Writes a save file with pickup status in a unique folder
    /// </summary>
    void SavePickupState()
    {
        // Logic to control whether to make a new file
        if (!File.Exists(_saveFileName))
        {
            File.Create(_saveFileName);
            File.WriteAllText(_saveFileName, _collectibleStatus.ToString());
        }
        else
        {
            using (var writer = new StreamWriter(_saveFileName, false))
            {
                writer.WriteLine(_collectibleStatus.ToString());
            }
        }
    }

    /// <summary>
    /// Presets the status dictionary with saved data.
    /// </summary>
    void LoadPickupState()
    {

    }

    #endregion

    #endregion
}
