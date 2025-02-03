/*=================================================================================================
 * FILE     : MissionGate.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 2/3/25
 * 
 * DESC     : Objects with this script attached will only be active during missions specified by
 *            the array _activeMissions
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGate : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] int[] _activeMissions;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        CheckMissionNo();
    }

    #endregion

    #region VARIABLE CHECKER

    /// <summary>
    /// Checks whether the current mission is one in which the attached object is valid. Otherwise,
    /// disables the object.
    /// </summary>
    void CheckMissionNo()
    {
        // Checks current mission against valid ones
        foreach (int i in _activeMissions)
        {
            if (i == GlobalVariableTracker.currentMission)
            {
                return;
            }

        }

        // Disables object
        gameObject.SetActive(false);
    }

    #endregion
}
