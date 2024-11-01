/*=================================================================================================
 * FILE     : CutsceneManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 10/11/24
 * 
 * DESC     : Controls the progression of scripted events.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    #region VARIABLES

    // Manager Singleton
    public static CutsceneManager cutsceneManager;

    // Objects
    DialogueManager dialogueManager = DialogueManager.dialogueManager;
    PlayerController _player = PlayerController.playerController;

    // Signals
    static bool _inCutscene = false;
    public static bool inCutscene => _inCutscene;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    #region CUTSCENE PROGRESS

    /// <summary>
    /// Puts the game in a cutscene state, disabling player input and setting up for cutscene
    /// functions
    /// </summary>
    public void StartCutscene(Cutscene cutscene)
    {
        // Sets up cutscene mode
        _inCutscene = true;
        _player.TogglePlayerInput();

        // Plays cutscene
        StartCoroutine(PlayCutscene(cutscene));
    }

    /// <summary>
    /// Progresses a cutscene along a linear series of events with varying functionality, including
    /// but not limited to, dialogue events, animating and moving characters, and image
    /// manipulation
    /// </summary>
    /// <returns>Delay Between</returns>
    IEnumerator PlayCutscene(Cutscene cutscene)
    {
        yield return null;
    }

    /// <summary>
    /// Returns Game to normal gameplay State
    /// </summary>
    void EndCutscene()
    {
        // Deactivates cutscene mode
        _inCutscene = false;
        _player.TogglePlayerInput();
    }

    #endregion
}
