/*=================================================================================================
 * FILE     : TitleMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/31/24
 * UPDATED  : 7/30/26
 * 
 * DESC     : Performs functions of the title screen menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TitleMenu : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _optionsBG;
    [SerializeField] Button _dummyButton;
    [SerializeField] Button _returnButton;
    [SerializeField] DialogueManager _dm;
    [SerializeField] AudioSource _menuAudioSource;
    [SerializeField] DebugProgressInjector _loadInjector;

    // Input Controls
    [SerializeField] InputActionAsset _menuInputs;
    public InputAction advance;
    public InputAction cancel;
    InputAction _debugScreenCap;

    // Sound Effects
    [SerializeField] AudioClip _cancelSound;

    // Data checking
    StreamReader _reader;

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
        _reader = new StreamReader(Application.dataPath + "/SaveData/ProgressFiles/Slot" + 1 +
            "/Level.txt");

        // Sets inputs
        advance = _menuInputs.FindAction("Submit");
        cancel = _menuInputs.FindAction("Cancel");

        // Sets input functions
        advance.performed += StartAdvance;
        advance.canceled += EndAdvance;
        cancel.performed += PlayCancelSound;
            
        // Inits volume
        GetComponent<AudioSource>().volume = GlobalVariableTracker.musicVolume;

        // DEBUG Enables screenshots
        _debugScreenCap = _menuInputs.FindAction("DEBUGScreenCap");
        _debugScreenCap.performed += ScreenCap;

        // Fades in
        ScreenEffects.fadingIn = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Reverts button selection after message
        if (EventSystem.current.currentSelectedGameObject == _dummyButton.gameObject &&
            !DialogueManager.dialogueInProgress)
        {
            _returnButton.Select();
        }


    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Begins a new game on a blank save 
    /// </summary>
    public void StartNewGame()
    {
        InGameMainMenu.inMainMenu = false;
        advance.performed -= StartAdvance;
        advance.canceled -= EndAdvance;
        cancel.performed -= PlayCancelSound;
        SceneManager.LoadScene("MissionTransition");
    }


    /// <summary>
    /// Loads the last made save file
    /// </summary>
    public void ContinueGame()
    {
        // Checks if save data exits
        if (_reader.Peek() == -1)
        {
            // Notifies that there is no save
            _dummyButton.Select();
            _dm.StartDialogue(_UnimplementedNotif);
        }
        else
        {
            // Loads save
            InGameMainMenu.inMainMenu = false;
            advance.performed -= StartAdvance;
            advance.canceled -= EndAdvance;
            cancel.performed -= PlayCancelSound;
            SaveLoadFunctions.LoadFile(1, _loadInjector);
        }
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

    #region INPUT FUNCTIONS

    /// <summary>
    /// Plays cancel sound effect when hitting the cancel button.
    /// </summary>
    void PlayCancelSound(InputAction.CallbackContext ctx)
    {
        _menuAudioSource.PlayOneShot(_cancelSound);
    }

    /// <summary>
    /// Used to advance dialogue
    /// </summary>
    void StartAdvance(InputAction.CallbackContext ctx)
    {
        DialogueManager.dialogueManager.advancing = true;
    }

    /// <summary>
    /// Tells Dialogue Manager not advance if button is not held.
    /// </summary>
    void EndAdvance(InputAction.CallbackContext ctx)
    {
        DialogueManager.dialogueManager.advancing = false;
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
            returnButton.Select();
        }
    }

    /// <summary>
    /// Takes a screenshot
    /// </summary>
    public void ScreenCap()
    {
        ScreenCapture.CaptureScreenshot(
            "screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
        Debug.Log("A screenshot was taken!");
    }
    public void ScreenCap(InputAction.CallbackContext ctx)
    {
        ScreenCapture.CaptureScreenshot(
            "screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
        Debug.Log("A screenshot was taken!");
    }

    #endregion
}
