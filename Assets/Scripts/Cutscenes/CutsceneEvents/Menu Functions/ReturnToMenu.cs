/*=================================================================================================
 * FILE     : ReturnToMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/11/25
 * UPDATED  : 4/4/25
 * 
 * DESC     : Exits from a cutscene event into the main in-game menu's default selection.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Return to Menu", menuName = "Cutscene/Menu Functions/Return", 
    order = 1)]
public class ReturnToMenu : CutsceneEvent
{
    /* #region VARIABLES

    // Object references
    InGameMainMenu _menu;

    #endregion */

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Selects default button in main menu.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets Vars
        /*_menu = DialogueManager.dialogueManager.gameObject.
            GetComponentInChildren<InGameMainMenu>();*/

        // Performs an extra movement toggle to ensure movement doesn't reactivate in confirmation
        PlayerController.playerController.TogglePlayerInput();
        PlayerController.playerController.cancel.performed
            += InGameMainMenu.inGameMainMenu.ExitMenu;

        // Performs selection
        InGameMainMenu.inGameMainMenu.gameObject.SetActive(true);
        InGameMainMenu.inGameMainMenu.defaultSelection.Select();

        // Notifies completion
        eventComplete = true;
    }

    #endregion
}
