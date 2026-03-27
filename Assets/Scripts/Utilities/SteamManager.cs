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

public class SteamManager : MonoBehaviour
{
    #region VARS

    // App Info
    public uint appId = 4450980;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this);

        // Activates Steam Client
        try
        {
            SteamClient.Init(appId);
            Debug.Log("Mmmm... Steamed Hams");
        }
        catch (System.Exception e)
        {
            // Couldn't init for some reason (steam is closed etc)
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion
}
