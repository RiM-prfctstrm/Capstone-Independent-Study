/*=================================================================================================
 * FILE     : CycleNewCutscene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/26
 * UPDATED  : 11/4/26
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

        // Starts new Cutscene
        CutsceneManager.cutsceneManager.StartCutscene(_nextEvent);
    }

    #endregion
}
