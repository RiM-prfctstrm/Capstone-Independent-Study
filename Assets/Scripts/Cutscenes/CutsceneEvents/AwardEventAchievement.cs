/*=================================================================================================
 * FILE     : AwardEventAchievement.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/17/26
 * UPDATED  : 6/22/26
 * 
 * DESC     : Awards an achievement as part of an event. CUrrently only supports steam, but can be
 *            modified to support other external achievement systems.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

[CreateAssetMenu(fileName = "SteamAchievement", menuName = "Cutscene/Achievement", order = 10)]
public class AwardEventAchievement : CutsceneEvent
{
    #region VARIABLES

    [SerializeField] string achName;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Awards achievement for appropriate interface
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Skips event if Steam can't be founs
        if (!SteamAPI.Init())
        {
            eventComplete = true;
            return;
        }

        // Awards Achievement
        AchievementManager.AwardAchievement(achName);
        eventComplete = true;
    }

    #endregion
}
