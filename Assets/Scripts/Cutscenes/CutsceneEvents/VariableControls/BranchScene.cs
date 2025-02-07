/*=================================================================================================
 * FILE     : BranchScene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/7/25
 * UPDATED  : 2/7/25
 * 
 * DESC     : Lets the player choose between two branches for the scene to follow, A on yes, B on 
 *            no.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Branch", menuName = "Cutscene/Data management/Set Flag", order = 1)]
public class BranchScene : CutsceneEvent
{
    #region VARIABLES

    // Choice text
    [SerializeField] Dialogue _choiceDialogue;

    // Branch Cutscenes
    [SerializeField] Cutscene _branchA;
    [SerializeField] Cutscene _branchB;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets up dialogue display and prepares player to make choice.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets up UI window
        DialogueManager.dialogueManager.dialogueOutline.SetActive(true);
        DialogueManager.dialogueManager.choiceMenu.SetActive(true);
        DialogueManager.dialogueManager.DisplayDialogue(_choiceDialogue);
        DialogueManager.dialogueManager.choiceDefaultSelection.Select();

    }

    #endregion
}
