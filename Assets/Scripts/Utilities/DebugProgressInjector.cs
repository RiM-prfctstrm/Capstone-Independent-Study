/*=================================================================================================
 * FILE     : DebugProgressInjector.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 2/5/25
 * 
 * DESC     : Debug script to set progression variables by hand in inspector. Used to tell the game
 *            to play at a certain point. Works best before loading scene.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Mission 3
    [SerializeField] bool _checkedIn = false;
    [SerializeField] bool _hasAccessCard = false;
    [SerializeField] bool _visitedReceptionist = false;


    // Activation Switch
    [SerializeField] bool _fireInjector = false;

    #endregion

    #region UNIVERSAL EVENTs
    /*/// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Allows continued debugging across scenes
        DontDestroyOnLoad(gameObject);

        // Fires injector
        InjectGlobalData();
    }*/

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
    /// </summary>
    void InjectGlobalData()
    {
        // Mission Data
        // Overall Progression
        GlobalVariableTracker.currentMission = _currentMission;
        GlobalVariableTracker.progressionFlags["m0complete"] = _m0Complete;
        GlobalVariableTracker.progressionFlags["m1complete"] = _m1Complete;
        GlobalVariableTracker.progressionFlags["m2complete"] = _m2Complete;
        GlobalVariableTracker.progressionFlags["m3complete"] = _m3Complete;
        // Mission 3 Progression
        GlobalVariableTracker.progressionFlags["checkedIn"] = _checkedIn;
        GlobalVariableTracker.progressionFlags["hasAccessCard"] = _hasAccessCard;
        GlobalVariableTracker.progressionFlags["visitedReceptionist"] = _visitedReceptionist;

        // Prevent repeat fires
        _fireInjector = false;

        Debug.Log(GlobalVariableTracker.progressionFlags["checkedIn"]);
    }

    #endregion
}
