/*=================================================================================================
 * FILE     : CutsceneDialogue.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/5/24
 * UPDATED  : 11/5/24
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
    public override void PlayEventFunction()
    {
        // Starts dialogue
        DialogueManager.dialogueManager.StartDialogue(cutsceneDialogue);

        // Prepares the script to wait for its completion signal
        // I have no idea why I can't just do this from here
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Signals that the event is over to the cutscene manager
    /// </summary>
    /// <returns>Waits until the cutscene is over</returns>
    protected override IEnumerator WaitForEventEnd()
    {
        yield return new WaitUntil(() => !DialogueManager.dialogueInProgress);

        _eventComplete = true;
    }

    #endregion
}
