/*=================================================================================================
 * FILE     : BranchScene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 2/21/25
 * 
 * DESC     : Lets the player choose between two branches for the scene to follow, A on yes, B on 
 *            no.
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

        // Delay
        yield return new WaitForFixedUpdate();

        // Selects button
        PlayerController.playerController.OpenMenu(DialogueManager.dialogueManager.choiceMenu,
            DialogueManager.dialogueManager.choiceNo.GetComponent<Button>());

        // Allows use of cancel button
        /*PlayerController.playerController.cancel.performed += ctx =>
            DialogueManager.dialogueManager.choiceNo.GetComponent<Button>().onClick.Invoke();*/

        // Event complete should never equal true, and this type of event should only be used at
        // the end of a cutscene script.
    }

    #endregion
}
