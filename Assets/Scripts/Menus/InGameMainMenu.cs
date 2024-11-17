/*=================================================================================================
 * FILE     : InGameMainMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/14/24
 * UPDATED  : 11/17/24
 * 
 * DESC     : Performs functions of the main in-game menu
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMainMenu : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    // Buttons
    public Button defaultSelection;
    [SerializeField] Button _mapButton;
    // Submenu Buttons
    [SerializeField] Button _map;
    // Other UI
    [SerializeField] TextMeshProUGUI _mapText;

    // Data input

    #endregion

    #region BUTTON ACTIONS

    #region MAIN MENU BUTTONS

    /// <summary>
    /// Opens the world map and sets text explaining the destination
    /// </summary>
    public void OpenMap()
    {

    }

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

    #region SUBMENU BUTTONS

    #endregion

    #endregion
}
