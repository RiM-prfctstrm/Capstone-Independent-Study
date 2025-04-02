/*=================================================================================================
 * FILE     : BranchScene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 4/2/25
 * 
 * DESC     : Lets the player choose between two branches for the scene to follow, A on yes, B on 
 *            no.
 *            IN EDITOR MAKE SURE NOT TO LEAVE EITHER CHOICE BLANK, USE DUMMY EVENT INSTEAD!
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Branch", menuName = "Cutscene/Data management/Branch", order = 1)]
public class BranchScene : CutsceneEvent
{
    #region VARIABLES

    // Choice text
    [SerializeField] Dialogue _choiceDialogue;
    DialogueEvent _event;

    // Branch Cutscenes
    [SerializeField] Cutscene _branchA = null;
    [SerializeField] Cutscene _branchB = null;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets up dialogue display and prepares player to make choice.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Prepares choice menu
        _event = new DialogueEvent(_choiceDialogue);
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());

    }

    /// <summary>
    /// Gives a small delay so that player doesn't automatically hit button.
    /// </summary>
    protected override IEnumerator WaitForEventEnd()
    {
        // Sets choice outcomes
        DialogueManager.dialogueManager.choiceYes.GetComponent<ChoiceButton>().resultEvent
            = _branchA;
        DialogueManager.dialogueManager.choiceNo.GetComponent<ChoiceButton>().resultEvent
            = _branchB;

        // Sets up explanation text
        DialogueManager.dialogueManager.dialogueOutline.SetActive(true);
        DialogueManager.dialogueManager.DisplayDialogue(_choiceDialogue);

        // Ensures dialogueEvent exists
        if (DialogueManager.dialogueManager.dialogRoutine == null)
        {
            DialogueManager.dialogueManager.dialogRoutine =
                DialogueManager.dialogueManager.PlayDialogue(_event);
        }

        // Delay
        yield return new WaitForFixedUpdate();

        // Selects button
        PlayerController.playerController.OpenMenu(DialogueManager.dialogueManager.choiceMenu,
            DialogueManager.dialogueManager.choiceNo.GetComponent<Button>());
    }

    #endregion
}
