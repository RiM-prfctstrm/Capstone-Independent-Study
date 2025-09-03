/*=================================================================================================
 * FILE     : ResetCollectibles.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/28/25
 * UPDATED  : 9/3/25
 * 
 * DESC     : Erases save files that store which collectibles have been picked up.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "ColResetter", menuName = "Cutscene/Data management/Collectible Reset",
    order = 5)]
public class ResetCollectibles : CutsceneEvent
{
    #region VARS

    // File list
    string _directoryPath;
    DirectoryInfo _directory;

    // Writes file
    StreamWriter _eraser;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Erases files containing data on which collectibles have been picked up
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Compiles list of files in the collectible directory
        _directoryPath = Application.dataPath + "/SaveData/Collectibles";
        _directory = new DirectoryInfo(_directoryPath);

        // Erases each file
        foreach(FileInfo i in _directory.GetFiles(".txt"))
        {
            _eraser = new StreamWriter(i.OpenWrite());
            _eraser.Write("");
        }

        // Signals completion
        eventComplete = true;
    }

    #endregion
}
