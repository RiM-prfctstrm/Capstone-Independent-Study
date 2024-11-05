/*=================================================================================================
 * FILE     : TitleMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/31/24
 * UPDATED  : 11/5/24
 * 
 * DESC     : Performs functions of the title screen menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    #region VARIABLES

    // Debug
    [SerializeField] DialogueManager _menuDM;
    [SerializeField] DialogueEvent _UnimplementedNotif;

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Begins a new game on a blank save t save
    /// </summary>
    public void StartNewGame()
    {
        SceneManager.LoadScene("Newscast");
    }

    /// <summary>
    /// Quits the Game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region DEBUG

    /// <summary>
    /// Placeholder functionality for when a button's feature is not yet implemented.
    /// </summary>
    public void UnimplementedMessage(Button returnButton)
    {
        if (!DialogueManager.dialogueInProgress)
        {
            // Sends a message telling the player nothing happens yet
            _menuDM.previouslySelected = returnButton;
            _menuDM.StartDialogue(_UnimplementedNotif);
        }
    }

    #endregion
}
