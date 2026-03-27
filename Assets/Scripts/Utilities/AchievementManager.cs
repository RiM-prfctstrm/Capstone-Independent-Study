/*=================================================================================================
 * FILE     : AchievementManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/27/26
 * UPDATED  : 3/26/26
 * 
 * DESC     : Tracks and awards Steam Achievements
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchievementManager : MonoBehaviour
{
    #region VARIABLES

    public bool Check_It;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        SteamUserStats.StoreStats();
        Debug.Log("Achievement: " + SteamUserStats.GetAchievement("ACH_FIRST_SNAIL", out Check_It));
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region ACHIEVEMENT AWARDING

    /// <summary>
    /// Checks whether a steam achievement has been awarded, and if not unlocks it
    /// </summary>
    /// <param name="achName">Name of the achievement to award</param>
    public static void AwardAchievement(string achName)
    {
        bool _achieved;

        // Tests whether ach is already earned
        SteamUserStats.GetAchievement(achName, out _achieved);
        if (_achieved)
        {
            Debug.Log(achName + " has already been achieved");
        }
        // Awards Achievement
        else
        {
            SteamUserStats.SetAchievement(achName);
            SteamUserStats.GetAchievement(achName, out _achieved);
            Debug.Log("Successfully awarded achievement: " + _achieved);
        }

        SteamUserStats.StoreStats();
    }

    #endregion

    #region DEBUG

    #endregion
}
