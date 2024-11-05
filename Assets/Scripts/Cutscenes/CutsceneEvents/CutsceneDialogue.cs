/*=================================================================================================
 * FILE     : CutsceneDialogue.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 11/4/24
 * 
 * DESC     : A modified version of DialogueEvent.cs for use in cutscenes
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Cutscene/Dialogue", order = 2)]
public class CutsceneDialogue : CutsceneEvent
{
    #region VARIABLES
    
    // The Dialogue that plays
    [SerializeField] DialogueEvent cutsceneDialogue;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Plays the dialogue and tells the cutscene manager when it is complete
    /// </summary>
    public override void EventFunction()
    {
        DialogueManager.dialogueManager.PlayDialogue(cutsceneDialogue);
    }

    #endregion
}
