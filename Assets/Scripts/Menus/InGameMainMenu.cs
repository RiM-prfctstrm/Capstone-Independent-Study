/*=================================================================================================
 * FILE     : InGameMainMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/14/24
 * UPDATED  : 5/3/25
 * 
 * DESC     : Performs functions of the main in-game menu
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InGameMainMenu : MonoBehaviour
{
    #region VARIABLES
    
    // Class singleton
    public static InGameMainMenu inGameMainMenu;

    // Object Refs
    // Buttons, ordered by vertical appearance in menu
    public Button defaultSelection;
    [SerializeField] Button _mapButton;
    [SerializeField] Button _manualButton;
    [SerializeField] Button _optionsButton;
    [SerializeField] Button _quitButton;
    // Submenu Buttons, ordered by vertical appearance in menu
    [SerializeField] Button _map;
    [SerializeField] Button _manual;
    [SerializeField] GameObject _optionsBG;
    // Other UI
    [SerializeField] TextMeshProUGUI _mapText;
    [SerializeField] TextMeshProUGUI _quickCounter;
    // External options
    [SerializeField] Cutscene _quitEvent;

    // Data input
    [TextArea] [SerializeField] string[] _deliveryInfo;

    // Underutilized State Controls
    public static bool inMainMenu;

    // Debug
    [SerializeField] DialogueEvent _unimplementedNotif;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        // Prevents opening in cutscenes
        if ((CutsceneManager.inCutscene || DialogueManager.dialogueInProgress) && !inMainMenu)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            // Tells game the main menu is actibe
            inMainMenu = true;
        }

        // Pauses Timer
        if (TimerController.timerInProgress)
        {
            TimerController.timerController.PauseTimer();
        }

        // Plays menu opening sound
        GetComponent<AudioSource>().volume = GlobalVariableTracker.menuVolume;

        // Sets counter for collectibles
        _quickCounter.text = GlobalVariableTracker.collectiblesInPocket.ToString();
    }

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

        // Sets cancel button to leave map instead
        PlayerController.playerController.cancel.performed -= ExitMenu;
        PlayerController.playerController.cancel.performed += CloseMap;
    }

    /// <summary>
    /// Opens the Bike Manual menu
    /// </summary>
    public void OpenManual()
    {
        _manual.gameObject.SetActive(true);
        _manual.Select();

        // Sets cancel button to leave manual instead
        PlayerController.playerController.cancel.performed -= ExitMenu;
        PlayerController.playerController.cancel.performed += CloseManual;
    }
    
    /// <summary>
    /// Opens the options menu
    /// </summary>
    public void OpenOptions()
    {
        _optionsBG.SetActive(true);

        // Sets cancel button to leave options instead
        PlayerController.playerController.cancel.performed -= ExitMenu;
    }

    /// <summary>
    /// Exits the menu and returns to gameplay
    /// </summary>
    public void ExitMenu()
    {
        // Sets normal gameplay state
        PlayerController.playerController.TogglePlayerInput();
        gameObject.SetActive(false);
        inMainMenu = false;

        // Removes cancel function
        PlayerController.playerController.cancel.performed -= ExitMenu;

        // Resumes Timer
        if (TimerController.timerInProgress)
        {
            TimerController.timerController.ResumeTimer();
        }
    }
    public void ExitMenu(InputAction.CallbackContext ctx)
    {
        // Sets normal gameplay state
        PlayerController.playerController.TogglePlayerInput();
        gameObject.SetActive(false);
        inMainMenu = false;

        // Removes cancel function
        PlayerController.playerController.cancel.performed -= ExitMenu;

        // Resumes Timer
        if (TimerController.timerInProgress)
        {
            TimerController.timerController.ResumeTimer();
        }
    }

    /// <summary>
    /// Starts event used to confirm player's decision to quit
    /// </summary>
    public void QuitToTitle()
    {
        // Performs an extra movement toggle to ensure movement doesn't reactivate in confirmation
        PlayerController.playerController.TogglePlayerInput();

        // Removes cancel function
        PlayerController.playerController.cancel.performed -= ExitMenu;

        // Tells game that it is out of main menu
        inMainMenu = false;
        gameObject.SetActive(false);

        // Sets class singleton to ensure return event knows where to go. Probably a cleaner way of
        // doing this.
        inGameMainMenu = this;

        // Prepares event
        defaultSelection = _quitButton;
        CutsceneManager.cutsceneManager.StartCutscene(_quitEvent);
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

        // Sets cancel button to leave menu instead
        PlayerController.playerController.cancel.performed -= CloseMap;
        PlayerController.playerController.cancel.performed += ExitMenu;
    }
    public void CloseMap(InputAction.CallbackContext ctx)
    {
        _mapButton.Select();
        _map.gameObject.SetActive(false);

        // Sets cancel button to leave menu instead
        PlayerController.playerController.cancel.performed -= CloseMap;
        PlayerController.playerController.cancel.performed += ExitMenu;
    }

    /// <summary>
    /// Returns from manual to the main menu
    /// </summary>
    public void CloseManual()
    {
        _manualButton.Select();
        _manual.gameObject.SetActive(false);

        // Sets cancel button to leave menu instead
        PlayerController.playerController.cancel.performed -= CloseManual;
        PlayerController.playerController.cancel.performed += ExitMenu;
    }
    public void CloseManual(InputAction.CallbackContext ctx)
    {
        _manualButton.Select();
        _manual.gameObject.SetActive(false);

        // Sets cancel button to leave menu instead
        PlayerController.playerController.cancel.performed -= CloseManual;
        PlayerController.playerController.cancel.performed += ExitMenu;
    }

    #endregion

    #endregion

    #region NON-BUTTON FUNCTIONS

    /// <summary>
    /// Closes submenus
    /// </summary>
    public void CloseSubmenus()
    {
        CloseMap();
        CloseManual();
        _optionsBG.SetActive(false);
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
            returnButton.Select();
            DialogueManager.dialogueManager.StartDialogue(_unimplementedNotif);
        }
    }

    #endregion
}
