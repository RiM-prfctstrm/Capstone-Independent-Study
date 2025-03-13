/*=================================================================================================
 * FILE     : ChoiceButton.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 3/13/25
 * 
 * DESC     : A button used to branch cutscenes, with a variable cutscene assigned by event that
 *            brings up the choice Menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton : MonoBehaviour
{
    #region VARIABLES

    // Event that plays when button is pressed
    public Cutscene resultEvent;

    // Dummy dialogue
    DialogueEvent _dummyDialogue = new DialogueEvent(new Dialogue(""));

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Exits prior cutscene, closes out choice UI, and begins the assigned branch event sequence
    /// </summary>
    public void PlayBranchEvent()
    {
        // Hides Dialogue UI
        DialogueManager.dialogueManager.CancelDialogue();

        // Exits prior Cutscene
        CutsceneManager.cutsceneManager.EndCutscene();

        // Removes player cancel ability
        PlayerController.playerController.cancel.Disable();

        // If there is a branch event to go to, begins that
        if (resultEvent != null)
        {
            CutsceneManager.cutsceneManager.StartCutscene(resultEvent);
        }

        // Disables choice UI (Done last so as not to break this function
        DialogueManager.dialogueManager.choiceMenu.SetActive(false);
    }

    #endregion
}
