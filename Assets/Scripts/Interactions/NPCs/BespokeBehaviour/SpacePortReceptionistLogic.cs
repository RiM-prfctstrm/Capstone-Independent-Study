/*=================================================================================================
 * FILE     : SpacePortReceptionistLogic
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/3/25
 * UPDATED  : 2/4/25
 * 
 * DESC     : Controls behaviour of the Spaceport receptionist, based on the current state of
 *            external variables.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePortReceptionistLogic : NPCInteraction
{
    #region VARIABLES

    // Cutscenes
    [SerializeField] Cutscene _firstGrantAccess;
    [SerializeField] Cutscene _firstNoAccess;
    [SerializeField] Cutscene _repeatGrantAccess;

    // Dialogues for repeated interactions
    [SerializeField] DialogueEvent _directToElevator;
    [SerializeField] DialogueEvent _directToMailbox;

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Determines what to do when the player interacts with the NPC. Handles logic to determine
    /// which event to play before performing normal NPC functionality
    /// </summary>
    public override void OnInteractedWith()
    {
        // Logic that determines which event and dialogue can play
        if (GlobalVariableTracker.progressionFlags["visitedReceptionist"] &&
            GlobalVariableTracker.progressionFlags["hasAccessCard"])
        {
            _NPCCutscene = _repeatGrantAccess;
            _NPCLines[0] = _directToElevator;
        }
        else if (GlobalVariableTracker.progressionFlags["hasAccessCard"])
        {
            _NPCCutscene = _firstGrantAccess;
            _NPCLines[0] = _directToElevator;
        }
        else
        {
            _NPCCutscene = _firstNoAccess;
            _NPCLines[0] = _directToMailbox;
        }


        // Normal NPC Functionality
        base.OnInteractedWith();
    }

    #endregion
}
