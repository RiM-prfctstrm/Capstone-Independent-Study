/*=================================================================================================
 * FILE     : DebugProgressInjector.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 4/23/26
 * 
 * DESC     : Debug script to set progression variables by hand in inspector. Used to tell the game
 *            to play at a certain point. Works best before loading scene.
 *            Flags are ordered by when in the game they are used rather than alphabetically.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DebugProgressInjector : MonoBehaviour
{

    #region VARIABLES

    // Mission status
    [SerializeField] int _currentMission;
    [SerializeField] bool _m0Complete = false;
    [SerializeField] bool _m1Complete = false;
    [SerializeField] bool _m2Complete = false;
    [SerializeField] bool _m3Complete = false;

    // Mission-specific flags
    // Mission 0
    [SerializeField] bool _introPlayed = false;
    // Mission 1
    [SerializeField] bool _saltSpoken = false;
    [SerializeField] bool _metTamago = false;
    // Mission 2
    [SerializeField] bool _m2bonusStarted = false;
    [SerializeField] bool _m2BonusNotifPlayed = false;
    // Mission 3
    [SerializeField] bool _spaceportBarrierDown = false;
    [SerializeField] bool _checkedIn = false;
    [SerializeField] bool _hasAccessCard = false;
    [SerializeField] bool _visitedReceptionist = false;

    // Special Completion Flags
    [SerializeField] bool _m0specialComplete = false;
    [SerializeField] bool _m2specialComplete = false;
    [SerializeField] bool _m3specialComplete = false;

    // Game States
    [SerializeField] bool _inDelivery = false;

    // Collectibles
    [SerializeField] int _collectiblesInPocket;
    [SerializeField] int _snailTotal;

    // Activation Switch
    [SerializeField] bool _fireInjector = false;

    #endregion

    #region CLASS CONSTRUCTORS

    /// <summary>
    /// Matches injector values to GlobalVariableTracker. Used to save otherwise static data to
    /// JSON.
    /// </summary>
    public void ReverseInjection()
    {
        // Mission Data
        // Overall Progression
        _currentMission = GlobalVariableTracker.currentMission;
        _m0Complete = GlobalVariableTracker.progressionFlags["m0complete"];
        _m1Complete = GlobalVariableTracker.progressionFlags["m1complete"];
        _m2Complete = GlobalVariableTracker.progressionFlags["m2complete"];
        _m3Complete = GlobalVariableTracker.progressionFlags["m3complete"];

        // Within-mission progression
        // Mission 0 Progression
        _introPlayed = GlobalVariableTracker.progressionFlags["introPlayed"];
        _saltSpoken = GlobalVariableTracker.progressionFlags["SaltSpoken"];
        _metTamago = GlobalVariableTracker.progressionFlags["metTamago"];
        // Mission 2 Progression
        _m2bonusStarted = GlobalVariableTracker.progressionFlags["m2bonusStarted"];
        _m2BonusNotifPlayed = GlobalVariableTracker.progressionFlags["m2BonusNotifPlayed"];
        // Mission 3 Progression
        _spaceportBarrierDown = GlobalVariableTracker.progressionFlags["spaceportBarrierDown"];
        _checkedIn = GlobalVariableTracker.progressionFlags["checkedIn"];
        _hasAccessCard = GlobalVariableTracker.progressionFlags["hasAccessCard"];
        _visitedReceptionist = GlobalVariableTracker.progressionFlags["visitedReceptionist"];

        // Special Completion Progression
        _m0specialComplete = GlobalVariableTracker.progressionFlags["m0specialComplete"];
        _m2specialComplete = GlobalVariableTracker.progressionFlags["m2specialComplete"];
        _m3specialComplete = GlobalVariableTracker.progressionFlags["m3specialComplete"];

        _inDelivery = GlobalVariableTracker.progressionFlags["inDelivery"];

        // Collectibles
        _collectiblesInPocket = (int)GlobalVariableTracker.collectiblesInPocket;
        _snailTotal = GlobalVariableTracker.snailTotal;
    }

    #endregion

    #region UNIVERSAL EVENTs

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Fires injector
        if (_fireInjector)
        {
            InjectGlobalData();
        }
    }

    #endregion

    #region SETTER FUNCTION

    /// <summary>
    /// Sets global variables to equal parameters set in script
    /// Also used to reset progression variables when the player quits the game, because I already
    /// have code for that here, and there should be no way that these variables are altered from
    /// initial states in build.
    /// </summary>
    public void InjectGlobalData()
    {
        // Mission Data
        // Overall Progression
        GlobalVariableTracker.currentMission = _currentMission;
        GlobalVariableTracker.progressionFlags["m0complete"] = _m0Complete;
        GlobalVariableTracker.progressionFlags["m1complete"] = _m1Complete;
        GlobalVariableTracker.progressionFlags["m2complete"] = _m2Complete;
        GlobalVariableTracker.progressionFlags["m3complete"] = _m3Complete;

        // Within-mission progression
        // Mission 0 Progression
        GlobalVariableTracker.progressionFlags["introPlayed"] = _introPlayed;
        // Mission 1 progression
        GlobalVariableTracker.progressionFlags["SaltSpoken"] = _saltSpoken;
        GlobalVariableTracker.progressionFlags["metTamago"] = _metTamago;
        // Mission 2 Progression
        GlobalVariableTracker.progressionFlags["m2bonusStarted"] = _m2bonusStarted;
        GlobalVariableTracker.progressionFlags["m2BonusNotifPlayed"] = _m2BonusNotifPlayed;
        // Mission 3 Progression
        GlobalVariableTracker.progressionFlags["spaceportBarrierDown"] = _spaceportBarrierDown;
        GlobalVariableTracker.progressionFlags["checkedIn"] = _checkedIn;
        GlobalVariableTracker.progressionFlags["hasAccessCard"] = _hasAccessCard;
        GlobalVariableTracker.progressionFlags["visitedReceptionist"] = _visitedReceptionist;

        // Special Completion Progression
        GlobalVariableTracker.progressionFlags["m0specialComplete"] = _m0specialComplete;
        GlobalVariableTracker.progressionFlags["m2specialComplete"] = _m2specialComplete;
        GlobalVariableTracker.progressionFlags["m3specialComplete"] = _m3specialComplete;

        // Game States
        GlobalVariableTracker.progressionFlags["inDelivery"] = _inDelivery;

        // Collectibles
        GlobalVariableTracker.collectiblesInPocket = _collectiblesInPocket;
        GlobalVariableTracker.snailTotal = _snailTotal;

        // Prevent repeat fires
        _fireInjector = false;

        // Logs successful update
        Debug.Log("Fire!");
    }

    #endregion
}
