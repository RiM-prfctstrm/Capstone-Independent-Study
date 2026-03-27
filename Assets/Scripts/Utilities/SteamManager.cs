/*=================================================================================================
 * FILE     : SteamManagers.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/27/26
 * UPDATED  : 3/27/26
 * 
 * DESC     : CManages Steam Functionality
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public enum eAchievements { ACH_FIRST_SNAIL }
public class SteamManager : MonoBehaviour
{
    #region VARS

    // Class Singleton
    public static SteamManager steamManager;

    // App Info
    uint appId = 4450980;
    bool _connectedToSteam = false;

    // Achievement info
    //int _achTotal = 2;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Prepares single instance
        if (steamManager == null)
        {
            steamManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Activates Steam Client
        try
        {
            SteamClient.Init(appId);
            _connectedToSteam = true;
            Debug.Log("Mmmm... Steamed Hams");
        }
        catch (System.Exception e)
        {
            // Couldn't init for some reason (steam is closed etc)
            _connectedToSteam = false;
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Calls Steam client
        if (_connectedToSteam)
        {
            SteamClient.RunCallbacks();
        }
    }

    /// <summary>
    /// This function is called when the behavior becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        // Shuts down steam client
        SteamClient.Shutdown();
    }

    #endregion

    #region ACHIEVEMENTS

    /// <summary>
    /// Unlocks a Steam Achievement
    /// </summary>
    /// <param name="ach">The Acheivement to Unlock</param>
    public void UnlockAchievement(string achName)
    {
        // Ensures Steam is Running
        if (_connectedToSteam)
        {
            // Sets achievement
            var ach = new Steamworks.Data.Achievement(achName);
            
            // Awards achievement
            if (!ach.State)
            {
                Debug.Log("alreadyObtained");
            }
            else
            {
                ach.Trigger();
                Debug.Log(achName + " Achieved!");
            }
        }

    }

    #endregion
}
