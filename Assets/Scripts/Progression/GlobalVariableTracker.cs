/*=================================================================================================
 * FILE     : GlobalVariableTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 4/1/25
 * 
 * DESC     : Stores data that is meant to persist throughout the entire game. Variables are kept
 *            in an initialized state to easily create a new save. If a save is loaded, they are
 *            immediately overwritten before loading the game.
 *            Flags are ordered by when in the game they are used rather than alphabetically.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableTracker
{
    #region VARIABLES

    #region GENERAL REFERENCE
    // Options parameters
    public static float masterVolume = 1;
    public static float menuVolume = .25f;
    public static float musicVolume = .8f;
    public static float sfxVolume = 1;
    public static int windowScale = 1;

    // General peogress information
    public static float collectiblesInPocket = 0;
    public static int currentMission = 0;

    #endregion

    #region PERSISTANT FLAGS
    // Flags that remain set for the remainder of gameplay.

    // Dictionary of all bools used as flags for progress
    public static Dictionary<string, bool> progressionFlags = new Dictionary<string, bool>
    {
        // Mission Complete Variable
        { "m0complete", false }, // Not used yet, will be when I go back to revise m0
        { "m1complete", false },
        { "m2complete", false },
        { "m3complete", false },

        // Mission Specific Flags
        // Mission 0
        { "tutorialPlayed", false },
        // Mission 2
        { "m2bonusStarted", false },
        { "m2BonusNotifPlayed", false },
        // Mission 3
        { "spaceportBarrierDown", false },
        { "checkedIn", false },
        { "hasAccessCard", false },
        { "visitedReceptionist", false },

        // Special Completion Flags
        { "m0specialComplete", false },
        { "m2specialComplete", false },
        { "m3specialComplete", false }
    };

    #endregion

    #endregion

    #region CONTROL FUNCTIONS

    /// <summary>
    /// Resets all progress in the game
    /// </summary>
    public static void ResetAllProgress()
    {
        // Resets numerical values
        collectiblesInPocket = 0;
        currentMission = 0;

        // Resets flags
        // Puts keys into a list so they can be modified
        List<string> flagResetter = new List<string>();
        foreach (string s in progressionFlags.Keys)
        {
            flagResetter.Add(s);
        }
        // Performs reset
        foreach (string s in flagResetter)
        {
            progressionFlags[s] = false;
        }
        // Clears list
        flagResetter.Clear();
    }

    #endregion
}
