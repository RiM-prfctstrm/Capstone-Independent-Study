/*=================================================================================================
 * FILE     : ReturnToMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/11/25
 * UPDATED  : 5/3/25
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
    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Selects default button in main menu.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Resumes Timer
        if (TimerController.timerInProgress)
        {
            TimerController.timerController.ResumeTimer();
        }

        // Notifies completion
        eventComplete = true;
    }

    #endregion
}
