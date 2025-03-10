/*=================================================================================================
 * FILE     : OptionsMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/16/25
 * UPDATED  : 3/10/25
 * 
 * DESC     : Adjusts variables that affect the game's presentation.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OptionsMenu : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    // UI Objects, ordered by vertical appearance in menu
    Button _defaultScaleButton;
    [SerializeField] Button[] _scaleArray = new Button[4];
    [SerializeField] Slider _musicVolume;
    [SerializeField] Slider _soundVolume;
    [SerializeField] Slider _menuVolume;
    // External UI Objects
    [SerializeField] Button _returnSelection;
    [SerializeField] TitleMenu _titleMenu;

    // Value initializers for when objects are set by menu, ordered by vertical appearance in menu
    static float _saveMuV = .8f;
    static float _saveSV = 1f;
    static float _saveMeV = .25f;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        // Initializes volume vars
        _musicVolume.value = _saveMuV;
        _soundVolume.value = _saveSV;
        _menuVolume.value = _saveMeV;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        // Initializes UI Positions
        _defaultScaleButton = _scaleArray[GlobalVariableTracker.windowScale - 1];
        _defaultScaleButton.Select();
        _musicVolume.value = GlobalVariableTracker.musicVolume;
        _soundVolume.value = GlobalVariableTracker.sfxVolume;
        _menuVolume.value = GlobalVariableTracker.menuVolume;

        // Sets cancel function
        if (PlayerController.playerController != null)
        {
            PlayerController.playerController.cancel.performed += ReturnToMenu;
        }
    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Resizes the game screen and rescales UI Elements
    /// </summary>
    /// <param name="scaleFactor">The scale by which to change the screen</param>
    public void ResizeScreen(int scaleFactor)
    {
        // Sets screen dimension variable
        int screenSide = 464 * scaleFactor;

        // Sets global scale var
        GlobalVariableTracker.windowScale = scaleFactor;

        // Resizes Screen
        Screen.SetResolution(screenSide, screenSide, false);
    }

    /// <summary>
    /// Sets the volume of music
    /// </summary>
    public void SetMusicVolume()
    {
        // Sets volume variabes
        GlobalVariableTracker.musicVolume = _musicVolume.value;
        _saveMuV = _musicVolume.value;

        // Automatically changes volume
        if (MusicManager.musicManager != null)
        {
            MusicManager.musicManager.SetVolume();
        }
    }

    /// <summary>
    /// Sets the volume of in-game sound effects
    /// </summary>
    public void SetSFXVolume()
    {
        // Sets volume variables
        GlobalVariableTracker.sfxVolume = _soundVolume.value;
        _saveSV = _soundVolume.value;

        // Automatically changes volume
        if (PlayerController.playerController != null)
        {
            PlayerController.playerController.SetSoundEffectsVolume();
        }
    }


    /// <summary>
    /// Sets the volume of menu sounds
    /// </summary>
    public void SetMenuVolume()
    {
        // Sets volume variables
        GlobalVariableTracker.menuVolume = _menuVolume.value;
        _saveMeV = _menuVolume.value;

        // Automatically changes volume
        if (DialogueManager.dialogueManager != null)
        {
            DialogueManager.dialogueManager.SetMenuEffectsVolume();
        }
    }

    /// <summary>
    /// Returns to whichever menu Options was opened from, and closes options menu
    /// </summary>
    public void ReturnToMenu()
    {
        // Returns to menu
        _returnSelection.Select();
        gameObject.SetActive(false);

        // Resets cancel function
        if (PlayerController.playerController != null)
        {
            PlayerController.playerController.cancel.performed -= ReturnToMenu;
            if (_returnSelection.GetComponentInParent<InGameMainMenu>() != null)
            {
                PlayerController.playerController.cancel.performed +=
                    _returnSelection.GetComponentInParent<InGameMainMenu>().ExitMenu;
            }
        }
        else if (_titleMenu != null)
        {
            _titleMenu.cancel.performed -= ReturnToMenu;
            _titleMenu.cancel.Disable();
        }
    }
    public void ReturnToMenu(InputAction.CallbackContext ctx)
    {
        // Returns to menu
        _returnSelection.Select();
        gameObject.SetActive(false);

        // Resets cancel function
        if (PlayerController.playerController != null)
        {
            PlayerController.playerController.cancel.performed -= ReturnToMenu;
            if (_returnSelection.GetComponentInParent<InGameMainMenu>() != null)
            {
                PlayerController.playerController.cancel.performed +=
                    _returnSelection.GetComponentInParent<InGameMainMenu>().ExitMenu;
            }    
        }
        else if (_titleMenu != null)
        {
            _titleMenu.cancel.performed -= ReturnToMenu;
            _titleMenu.cancel.Disable();
        }
    }

    #endregion
}
