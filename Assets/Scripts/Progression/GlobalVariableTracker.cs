/*=================================================================================================
 * FILE     : GlobalVariableTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 2/5/25
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
    // Information that is commonly used in a variety of circumstances

    // General mission information
    public static int currentMission = 0;

    #endregion

    #region PERSISTANT FLAGS

    // Dictionary of all bools used as flags for progress
    public static Dictionary<string, bool> progressionFlags = new Dictionary<string, bool>
    {
        // Mission Complete Variable
        { "m0complete", false }, // Not used yet, will be when I go back to revise m0
        { "m1complete", false },
        { "m2complete", false },
        { "m3complete", false },

        // Mission Specific Flags
        // Mission 3
        { "checkedIn", false },
        { "hasAccessCard", false },
        { "visitedReceptionist", false }
    };

    #endregion

    #endregion
}
