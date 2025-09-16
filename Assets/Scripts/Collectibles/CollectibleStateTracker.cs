/*=================================================================================================
 * FILE     : CollectibleStateTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/25
 * UPDATED  : 9/29/25
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
    string _saveFileName;

    // Controls
    int _initScene;

    // Incrementers
    int _IDNo = 0;

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
        _initScene = SceneManager.GetActiveScene().buildIndex;

        // Compiles collectibles in scene
        foreach (Collectible i in GetComponentsInChildren<Collectible>())
        {
            _collectibleObjects.Add(i);
            i.localTracker = this;
            i.collectibleID = _IDNo;
            _IDNo++;
        }

        // Loads state data
        LoadPickupState();

        // DEBUG Recompiles list after scene changed in editor
        // Determines whether to create a new collectible dictionary
        if (_collectibleObjects.Count != _collectibleStatus.Count)
        {
            CreateNewStatusDict();
        }
        // Removes picked up collectibles
        else
        {
            RemoveCollectedObjs();
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Update iscalled once per frame
    /// </summary>
    void Update()
    {
        // Saves and removes object when new scene is loadded
        if (SceneManager.GetActiveScene().buildIndex != _initScene)
        {
            SavePickupState();
            Destroy(gameObject);
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
        foreach (Nanoblip i in _collectibleObjects)
        {
            // Assigns unique ID number to each object
            _collectibleStatus.Add(IDNo, false);
            IDNo++;
        }

        // Saves status (DEBUG???)
        //SavePickupState();
    }

    /// <summary>
    /// Destroys all collectibles that have already been picked up
    /// </summary>
    void RemoveCollectedObjs()
    {
        foreach (KeyValuePair<int, bool> i in _collectibleStatus)
        {
            if (i.Value == true)
            {
                Destroy(_collectibleObjects[i.Key].gameObject);
            }
        }
    }

    /// <summary>
    /// Changes collected status for individual collectibles
    /// </summary>
    public void UpdateDict(int IDKey)
    {
        _collectibleStatus[IDKey] = true;
    }

    #region SAVE MANAGEMENT

    /// <summary>
    /// Writes a save file with pickup status in a unique folder
    /// </summary>
    void SavePickupState()
    {
        // Creates save file if none exists
        if (!File.Exists(_saveFileName))
        {
            File.Create(_saveFileName);
        }

        // Writes saves
        using (var writer = new StreamWriter(_saveFileName, false))
        {
            // Writes new data
            foreach (KeyValuePair<int, bool> i in _collectibleStatus)
            {
                writer.WriteLine(i.Key);
                writer.WriteLine(i.Value);
            }
        }
    }

    /// <summary>
    /// Presets the status dictionary with saved data.
    /// </summary>
    void LoadPickupState()
    {
        // Reads save file
        using (var reader = new StreamReader(_saveFileName))
        {
            // Loops through each line and updates dict with data
            while(reader.Peek() != -1)
            {
                _collectibleStatus.Add(int.Parse(reader.ReadLine()),
                    bool.Parse(reader.ReadLine()));
            }
        }

        // Delates trailing kv pair
        if (_collectibleStatus.ContainsKey(-1))
        {
            _collectibleStatus.Remove(-1);
        }
    }

    #endregion

    #endregion
}
