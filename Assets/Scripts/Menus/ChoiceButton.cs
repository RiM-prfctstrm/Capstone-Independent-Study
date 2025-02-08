/*=================================================================================================
 * FILE     : ChoiceButton.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 2/8/25
 * 
 * DESC     : A button used to branch cutscenes, with a variable cutscene assigned by event that
 *            brings up the choice Menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// Hides choice UI
    /// </summary>
    public void HideUI()
    {
        // Hides UI
        DialogueManager.dialogueManager.dialogueOutline.SetActive(false);
        DialogueManager.dialogueManager.choiceMenu.SetActive(false);
    }

    /// <summary>
    /// Plays resultEvent, or ends functionality if resultEvent is null
    /// </summary>
    public void PlayCutsceneEvent()
    {
        // Resets dialogue to not automatically skip
        DialogueManager.dialogueManager.advancing = false;

        // Stops function if result event is null
        if (resultEvent != null)
        {
            CutsceneManager.cutsceneManager.StartCutscene(resultEvent);
        }
    }

    #endregion
}
