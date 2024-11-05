/*=================================================================================================
 * FILE     : CutsceneManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1X/X/24
 * UPDATED  : 11/5/24
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
        PlayerController.playerController.TogglePlayerInput();

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
        // Plays each event in sequence
        foreach (CutsceneEvent i in cutscene.cutsceneScript)
        {
            i.PlayEventFunction();
            yield return new WaitUntil(() => i.eventComplete == true);
        }

        // Ends cutscene
        EndCutscene();
    }

    /// <summary>
    /// Returns Game to normal gameplay State
    /// </summary>
    void EndCutscene()
    {
        Debug.Log("the cutscene is over.");
        // Deactivates cutscene mode
        _inCutscene = false;
        PlayerController.playerController.TogglePlayerInput();
    }

    #endregion
}
