/*=================================================================================================
 * FILE     : CycleNewCutscene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/26
 * UPDATED  : 11/16/26
 * 
 * DESC     : Ends the current cutscene and immediately plays a new one. Used during transitions
 *            between levels so that loading a save from a mission transition plays the proper
 *            scene instead of prompting you to save first thing.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEvent", menuName = "Cutscene/Switch Cutscene", order = 11)]
public class CycleNewCutscene : CutsceneEvent
{
    #region VARIABLES

    // Next event to play.
    [SerializeField] Cutscene _nextEvent;

    #endregion

    #region EVENT FUNCTIONALITY

    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Ends previous Cutscene (rest should be handled in CutsceneManager)
        eventComplete = true;

        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Delays execution until previous cutscene completes to ensure new cutscene works correctly
    /// </summary>
    /// <returns>Delay controlled by CutsceneManager.inCutscene</returns>
    protected override IEnumerator WaitForEventEnd()
    {
        // Delay
        yield return new WaitWhile(() => CutsceneManager.inCutscene);

        // Starts new Cutscene
        CutsceneManager.cutsceneManager.StartCutscene(_nextEvent);
    }

    #endregion
}
