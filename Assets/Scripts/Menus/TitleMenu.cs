/*=================================================================================================
 * FILE     : Title Menu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/31/24
 * UPDATED  : 10/31/24
 * 
 * DESC     : Performs functions of the title screen menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    #region VARIABLES

    // Debug
    [SerializeField] DialogueManager _menuDM;
    [SerializeField] DialogueEvent _UnimplementedNotif;

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Begins a new game with a default save
    /// </summary>
    public void StartNewGame()
    {

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
    public void UnimplementedMessage()
    {
        if (!DialogueManager.dialogueInProgress)
        {
            StartCoroutine(_menuDM.PlayDialogue(_UnimplementedNotif));
        }
    }

    #endregion
}
