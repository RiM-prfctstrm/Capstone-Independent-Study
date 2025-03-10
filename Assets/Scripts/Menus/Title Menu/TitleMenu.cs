/*=================================================================================================
 * FILE     : TitleMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/31/24
 * UPDATED  : 3/10/25
 * 
 * DESC     : Performs functions of the title screen menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _optionsBG;
    [SerializeField] DialogueManager _dm;

    // Input Controls
    [SerializeField] InputActionAsset _menuInputs;
    public InputAction cancel;

    // Debug
    [SerializeField] DialogueManager _menuDM;
    [SerializeField] DialogueEvent _UnimplementedNotif;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Inits Vars
        DialogueManager.dialogueManager = _dm;

        // Sets inputs
        cancel = _menuInputs.FindAction("Cancel");
    }

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
    /// Activates the credits and sets the sequence
    /// </summary>
    public void PlayCredits()
    {
        // Sets up credits
        _credits.SetActive(true);
        _credits.GetComponent<Button>().Select();

        // Sets up cancel function
        cancel.performed += _credits.GetComponentInChildren<TitleMenuCredits>().StopCredits;
        cancel.Enable();
    }

    /// <summary>
    /// Opens the options menu
    /// </summary>
    public void OpenOptions()
    {
        // Sets up menu
        _optionsBG.SetActive(true);

        // Sets up cancel function
        cancel.performed += _optionsBG.GetComponent<OptionsMenu>().ReturnToMenu;
        cancel.Enable();
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
