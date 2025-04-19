/*=================================================================================================
 * FILE     : EssentialPreserver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 4/19/25
 * 
 * DESC     : Used to keep scene essentials when a new scene is loaded.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialPreserver : MonoBehaviour
{
    #region VARIABLES

    // Singleton used to create scene essentials
    public static EssentialPreserver instance;

    // List of one-off cutscenes, should only be edited in Start Up Controller Prefab
    [SerializeField] Cutscene[] _oneOffCutscenes;

    // Failsafe bools to prevent repeat firing of methods
    bool _cutscenesInitialized = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Sets all commonly used singletons in this and in children. It's probably more efficient
        // to set children in their respective classes, but since this only happens once it's not
        // as big a deal and helps keep the execution order clean.
        instance = this;
        PlayerController.playerController = GetComponentInChildren<PlayerController>();
        DialogueManager.dialogueManager = GetComponentInChildren<DialogueManager>();
        CutsceneManager.cutsceneManager = GetComponentInChildren<CutsceneManager>();
        MusicManager.musicManager = GetComponentInChildren<MusicManager>();
        CollectibleManager.collectibleManager = GetComponentInChildren<CollectibleManager>();
        TimerController.timerController = GetComponentInChildren<TimerController>();

        // Performs bespoke initializations for other kinds of objects
        InitializeCutscenes();
        DialogueManager.dialogueManager.SetMenuEffectsVolume();

        // Keeps object and children around when new scenes are loaded
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region BESPOKE INITIALIZATIONS
    
    /// <summary>
    /// Sets all cutscenes contained specified in the array _oneOffCutscenes to be read as unplayed
    /// when first booting the game.
    /// </summary>
    void InitializeCutscenes()
    {
        // Failsafe to prevent reinitialization mid-game
        if (_cutscenesInitialized)
        {
            return;
        }

        // Initializes cutscenes to unplayed state
        foreach(Cutscene i in _oneOffCutscenes)
        {
            i.hasPlayed = false;
        }

        // Tells game that cutscenes are initialized
        _cutscenesInitialized = true;
    }

    #endregion
}
