/*=================================================================================================
 * FILE     : SnailSaveManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/22/25
 * UPDATED  : 9/22/25
 * 
 * DESC     : Tracks which snails have been collected and synchronizes temporary saves to the
 *            permanent file.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SnailSaveManager
{
    #region VARIABLES

    // File data
    static string _tempSaveFile = Application.dataPath + "/SaveData/Collectibles/SnailsTemp.txt";

    // Working comparison data
    static List<int> _collectedSnails = new List<int>();
    public static List<int> collectedSnails => _collectedSnails;

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Sets the comparison list to match save file, usually when first booting the game
    /// </summary>
    public static void InitializeComparisonList()
    {
        // Reads save file
        using (var reader = new StreamReader(_tempSaveFile))
        {
            while (reader.Peek() != -1)
            {
                _collectedSnails.Add(int.Parse(reader.ReadLine()));
            }
        }
    }

    /// <summary>
    /// Updates temporary save data when a new snail is picked up.
    /// </summary>
    /// <param name="idNo">ID of the snail to save</param>
    public static void UpdateTempSave(int idNo)
    {
        // Prevents repeat listing
        if(!_collectedSnails.Contains(idNo))
        {
            // Writes saves
            using (var writer = new StreamWriter(_tempSaveFile, true))
            {
                writer.WriteLine(idNo);
            }

            // Updates working comparison list
            _collectedSnails.Add(idNo);
        }
    }

    #endregion
}
