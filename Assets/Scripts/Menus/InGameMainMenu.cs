/*=================================================================================================
 * FILE     : InGameMainMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/14/24
 * UPDATED  : 11/18/24
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
    [SerializeField] Button _manualButton;
    [SerializeField] Button _optionsButton;
    // Submenu Buttons
    [SerializeField] Button _map;
    [SerializeField] Button _manual;
    // Other UI
    [SerializeField] TextMeshProUGUI _mapText;

    // Data input
    [TextArea] [SerializeField] string[] _deliveryInfo;

    // Debug
    [SerializeField] DialogueEvent _unimplementedNotif;

    #endregion

    #region BUTTON ACTIONS

    #region MAIN MENU BUTTONS

    /// <summary>
    /// Opens the world map and sets text explaining the destination
    /// </summary>
    public void OpenMap()
    {
        _map.gameObject.SetActive(true);
        _map.Select();
        _mapText.text = _deliveryInfo[GlobalVariableTracker.currentMission];
    }

    /// <summary>
    /// Opens the Bike Manual menu
    /// </summary>
    public void OpenManual()
    {
        _manual.gameObject.SetActive(true);
        _manual.Select();
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

    /// <summary>
    /// Returns from the map to the main menu
    /// </summary>
    public void CloseMap()
    {
        _mapButton.Select();
        _map.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns from manual to the main menu
    /// </summary>
    public void CloseManual()
    {
        _manualButton.Select();
        _manual.gameObject.SetActive(false);
    }

    #endregion

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
            returnButton.Select();
            DialogueManager.dialogueManager.StartDialogue(_unimplementedNotif);
        }
    }

    #endregion
}
