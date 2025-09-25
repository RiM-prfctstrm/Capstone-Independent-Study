/*=================================================================================================
 * FILE     : SaveLoadFunctions.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/25/25
 * UPDATED  : 9/25/25
 * 
 * DESC     : Writes and reads permanent save data.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadFunctions
{
    #region VARIABLES

    // IO Data
    static string _level;
    static string _progressToSave;
    static string _snailsToSave;

    // IO Operators
    static string _basePath = Application.dataPath + "/SaveData/ProgressFiles/Slot";
    static string _snailTempPath = Application.dataPath + "/SaveData/Collectibles/SnailsTemp.txt";
    static StreamWriter _saveWriter;
    static StreamReader _saveReader;

    #endregion

    #region SAVING

    /// <summary>
    /// Saves data to a specified save slot
    /// </summary>
    /// <param name="slot">The slot to save to</param>
    public static void SaveFile(int slot)
    {
        // Sets data to save
        _progressToSave = JsonUtility.ToJson(new DebugProgressInjector());
        _level = JsonUtility.ToJson(SceneManager.GetActiveScene());

        // Saves game progression
        using(_saveWriter = new StreamWriter(_basePath + slot + "/Progress.json"))
        {
            _saveWriter.Write(_progressToSave);
        }

        // Saves Snails
        // Gathers data from temp file
        using(_saveReader = new StreamReader(_snailTempPath))
        {
            _snailsToSave = _saveReader.ReadToEnd();
        }
        // Moves data to permanent file
        using (_saveWriter = new StreamWriter(_basePath + slot + "/Snails.txt"))
        {
            _saveWriter.Write(_snailsToSave);
        }

        // Saves current level
        using (_saveWriter = new StreamWriter(_basePath + slot + "/Level.json"))
        {
            _saveWriter.Write(_level);
        }
    }

    #endregion

    #region LOADING

    /// <summary>
    /// Reads save data, sets global variables, and warps player to the previous map
    /// </summary>
    /// <param name="slot">Save slot to read</param>
    public static void LoadFile(int slot)
    {
        // Loads progress vars
        DebugProgressInjector saveInjector =
            JsonUtility.FromJson<DebugProgressInjector>(_basePath + slot + "/Progress.json");
        saveInjector.InjectGlobalData();

        // Loads Snails
        // Gathers data from permanent file
        using (_saveReader = new StreamReader(_basePath + slot + "/Snails.txt"))
        {
            _snailsToSave = _saveReader.ReadToEnd();
        }
        // Moves data to temp file
        using (_saveWriter = new StreamWriter(_snailTempPath))
        {
            _saveWriter.Write(_snailsToSave);
        }

        // Loads Scene
        SceneManager.LoadScene(JsonUtility.FromJson<Scene>(_basePath + slot + "/Level.json").name);
    }

    #endregion
}
