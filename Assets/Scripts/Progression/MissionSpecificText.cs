/*=================================================================================================
 * FILE     : MissionSpecificText.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/8/25
 * UPDATED  : 2/8/25
 * 
 * DESC     : Uses current mission to determine which text attached TMPro should display.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionSpecificText : MonoBehaviour
{
    #region VARIABLES

    // Text array
    [SerializeField] string[] _variableText;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is first called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = _variableText[GlobalVariableTracker.currentMission];
    }

    #endregion
}
