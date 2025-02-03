/*=================================================================================================
 * FILE     : GlobalVariableTracker.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 1/25/25
 * 
 * DESC     : Stores data that is meant to persist throughout the entire game. Variables are kept
 *            in an initialized state to easily create a new save. If a save is loaded, they are
 *            immediately overwritten before loading the game.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariableTracker
{
    #region VARIABLES

    #region GENERAL REFERENCE
    // Information that is commonly used in a variety of circumstances

    // General mission information
    public static int currentMission = 0;

    #endregion

    #region PERSISTANT FLAGS

    // Mission Complete Variable
    public static bool m0Complete = false; // Not used yet, will be when I go back to revise m0
    public static bool m1Complete = false;
    public static bool m2Complete = false;
    public static bool m3Complete = false;

    // Mission Specific Flags
    // Mission 3
    public static bool hasAccessCard = false;
    public static bool visitedReceptionist = false;

    #endregion

    #endregion
}
