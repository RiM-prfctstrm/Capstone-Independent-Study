/*=================================================================================================
 * FILE     : OptionsMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/16/25
 * UPDATED  : 2/16/25
 * 
 * DESC     : Adjusts variables that affect the game's presentation.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    // UI Objects, ordered by vertical appearance in menu
    Button _defaultScaleButton;
    [SerializeField] Button[] _scaleArray = new Button[4];
    [SerializeField] Slider _musicVolume;
    [SerializeField] Slider _soundVolume;
    // External UI Objects
    [SerializeField] Button _returnSelection;

    // Value initializers for when objects are set by menu
    static float _saveMV = .8f;
    static float _saveSV = .25f;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Initializes volume vars
    /// </summary>
    private void Awake()
    {
        _musicVolume.value = _saveMV;
        _soundVolume.value = _saveSV;
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
    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Sets the volume of music
    /// </summary>
    public void SetMusicVolume()
    {
        // Sets volume variabes
        GlobalVariableTracker.musicVolume = _musicVolume.value;
        _saveMV = _musicVolume.value;

        // Automatically changes volume
        if (MusicManager.musicManager != null)
        {
            MusicManager.musicManager.SetVolume();
        }
    }

    /// <summary>
    /// Sets the volume of in-game sound effects, including menu sounds
    /// </summary>
    public void SetSFXVolume()
    {
        // Sets volume variables
        GlobalVariableTracker.sfxVolume = _soundVolume.value;
        _saveSV = _soundVolume.value;

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
        _returnSelection.Select();
        gameObject.SetActive(false);
    }

    #endregion
}
