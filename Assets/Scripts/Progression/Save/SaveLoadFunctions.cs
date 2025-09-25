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

public class SaveLoadFunctions
{
    #region VARIABLES

    // IO Data
    static string _progressToSave;

    // IO Operators
    static string _basePath = Application.dataPath + "/SaveData/ProgressFiles/Slot";
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
        _progressToSave = JsonUtility.ToJson(GlobalVariableTracker.globalVariableTracker);

        // Saves game progression
        using(_saveWriter = new StreamWriter(_basePath + slot + "/Progress.json"))
        {
            _saveWriter.Write(_progressToSave);
        }
    }

    #endregion

    #region LOADING

    #endregion
}
