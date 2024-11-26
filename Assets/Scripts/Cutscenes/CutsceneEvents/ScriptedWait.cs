/*=================================================================================================
 * FILE     : ScriptedWait.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/8/24
 * UPDATED  : 11/25/24
 * 
 * DESC     : Delays cutscene execution for an assigned span.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wait", menuName = "Cutscene/Wait",
                 order = 4)]
public class ScriptedWait : CutsceneEvent
{
    #region VARIABLES

    // Input
    [SerializeField] float _waitSeconds;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Basically only serves to start the delay.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Prepares the script to wait for its completion signal
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Delays based on inputted time
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator WaitForEventEnd()
    {
        yield return new WaitForSeconds(_waitSeconds);

        eventComplete = true;
    }

    #endregion
}
