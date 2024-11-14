/*=================================================================================================
 * FILE     : InGameMainMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/14/24
 * UPDATED  : 11/14/24
 * 
 * DESC     : Performs functions of the main in-game menu
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMainMenu : MonoBehaviour
{
    #region VARIABLES

    // Objects
    [SerializeField] public Button defaultSelection;

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Returns the game to the title screen and deletes the scene essentials, which are not meant
    /// to exist there
    /// </summary>
    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);
        Destroy(EssentialPreserver.instance.gameObject);
        EssentialPreserver.instance = null;
    }

    /// <summary>
    /// Exits the menu and returns to gameplay
    /// </summary>
    public void ExitMenu()
    {
        PlayerController.playerController.TogglePlayerInput();
        gameObject.SetActive(false);
    }

    #endregion
}
