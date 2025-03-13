/*=================================================================================================
 * FILE     : QuitToTitleu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/11/25
 * UPDATED  : 3/13/25
 * 
 * DESC     : Returns to title screen scene and deletes all player objects. Originally part of
 *            InGameMainMenu.cs, moved to cutscene event for easier integration with choice menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Quit Game", menuName = "Cutscene/Menu Functions/Quit", order = 0)]
public class QuitToTitle : CutsceneEvent
{
    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Returns to title screen and deletes player objects
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Resets global variables
        CollectibleManager.collectibleManager.ResetCount();

        // Returns the game to the title screen and deletes the scene essentials, which are not
        // meant to exist there
        SceneManager.LoadScene(0);
        Destroy(EssentialPreserver.instance.gameObject);
        EssentialPreserver.instance = null;

        // Marks completion
        eventComplete = true;
    }

    #endregion
}
