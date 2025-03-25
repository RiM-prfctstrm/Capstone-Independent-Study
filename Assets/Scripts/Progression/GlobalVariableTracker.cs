/*=================================================================================================
 * FILE     : GlobalVariableTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 3/25/25
 * 
 * DESC     : Stores data that is meant to persist throughout the entire game. Variables are kept
 *            in an initialized state to easily create a new save. If a save is loaded, they are
 *            immediately overwritten before loading the game.
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
}
