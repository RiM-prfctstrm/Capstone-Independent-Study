/*=================================================================================================
 * FILE     : SaveOptions
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/28/25
 * UPDATED  : 10/28/25
 * 
 * DESC     : Contains functions to write options to a save file and automatically load saved
              save settings when the game boots.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveOptions
{
    #region VARIABLES

    #endregion

    #region SAVE/LOAD

    /// <summary>
    /// Writes current settings to save file
    /// </summary>
    public static void SaveSettings()
    {
        using (var writer = new StreamWriter(Application.dataPath + "/SaveData/Settings.txt"))
        {
            // Writes options to save
            writer.WriteLine(GlobalVariableTracker.masterVolume);
            writer.WriteLine(GlobalVariableTracker.menuVolume);
            writer.WriteLine(GlobalVariableTracker.musicVolume);
            writer.WriteLine(GlobalVariableTracker.sfxVolume);
            writer.WriteLine(GlobalVariableTracker.windowedMode);
            writer.WriteLine(GlobalVariableTracker.windowScale);
        }
    }

    /// <summary>
    /// Writes setting data to current storage mode
    /// </summary>
    public static void LoadSettings()
    {
        // Reads data
        using (var reader = new StreamReader(Application.dataPath + "/SaveData/Settings.txt"))
        {
            GlobalVariableTracker.masterVolume = float.Parse(reader.ReadLine());
            GlobalVariableTracker.menuVolume = float.Parse(reader.ReadLine());
            GlobalVariableTracker.musicVolume = float.Parse(reader.ReadLine());
            GlobalVariableTracker.sfxVolume = float.Parse(reader.ReadLine());
            GlobalVariableTracker.windowedMode = bool.Parse(reader.ReadLine());
            GlobalVariableTracker.windowScale = int.Parse(reader.ReadLine());
        }

        // Sets options
    }

    #endregion
}
