/*=================================================================================================
 * FILE     : CutsceneManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1X/X/24
 * UPDATED  : 2/4/25
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

    // Object References, typically to allow event SOs to refer to objects in the scene
    [SerializeField] GameObject _UISpace;
    public GameObject UISpace => _UISpace;
    // A list of characters that can be used in cutscenes. This keeps the cutscene functions
    // uncluttered by NPCs that aren't used by them, making the object references easier to keep
    // track of.
    public List<GameObject> cutsceneObjects = new List<GameObject>();

    // Signals
    static bool _inCutscene = false;
    public static bool inCutscene => _inCutscene;

    // Skip Position Determinants
    Cutscene _skipEvent;
    int _skipPos;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // DEBUG
        // Skips cutscenes. To be added to main functionality
        if (Input.GetKeyDown(KeyCode.Tab) && inCutscene)
        {
            SkipCutscene();
        }
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
        PlayerController.playerController.rb2d.velocity = Vector2.zero;
        PlayerController.playerController.TogglePlayerInput();
        _inCutscene = true;
        _skipEvent = cutscene;
        _skipPos = 0;

        // Plays cutscene
        StartCoroutine(PlayCutscene(cutscene));
    }

    /// <summary>
    /// Progresses a cutscene along a linear series of events with varying functionality, including
    /// but not limited to, dialogue events, animating and moving characters, and image
    /// manipulation
    /// </summary>
    /// <returns>Delay Between Events</returns>
    IEnumerator PlayCutscene(Cutscene cutscene)
    {   
        // Plays each event in sequence
        foreach (CutsceneEvent i in cutscene.cutsceneScript)
        {
            i.PlayEventFunction();
            yield return new WaitUntil(() => i.eventComplete == true);
            _skipPos++;
        }

        // Ends cutscene
        //cutscene.hasPlayed = true; temporarily commented until a system to save and adjust this
        // data is in place
        EndCutscene();
    }

    /// <summary>
    /// Returns Game to normal gameplay State
    /// </summary>
    void EndCutscene()
    {
        // Deactivates cutscene mode
        _inCutscene = false;
        PlayerController.playerController.TogglePlayerInput();
    }

    /// <summary>
    /// Triggers cutscene skipping
    /// </summary>
    public void SkipCutscene()
    {
        // Stop Ordinary Cutscene functionality
        DialogueManager.dialogueManager.CancelDialogue();
        StopAllCoroutines();

        // Initiate skip loop
        StartCoroutine(SkipLoop());
    }

    /// <summary>
    /// Instantaneously performs all game state changing cutscene functionality while skipping any
    /// process that takes time.
    /// </summary>
    IEnumerator SkipLoop()
    {
        // Vars
        CutsceneEvent i;
        MoveByVectors k;

        // Loops through cutscene events
        for (int j = _skipPos; j < _skipEvent.cutsceneScript.Count; j++)
        {
            // Set event
            i = _skipEvent.cutsceneScript[j];

            // Skips events that do not alter game state beyond cosmetics within cutscene
            if (i.GetType() == typeof(CutsceneDialogue) || i.GetType() == typeof(ScriptedWait))
            {
                i.eventComplete = true;
                continue;
            }
            // Performs state-changing events that happen instantaneously by default
            else if (i.GetType() == typeof(SetGlobalFlag) ||
                     i.GetType() == typeof(AnimateCharacter) ||
                     i.GetType() == typeof(CutsceneSceneTransition) ||
                     i.GetType() == typeof(SetEventMusic) || 
                     i.GetType() == typeof(ToggleVisibility) ||
                     i.GetType() == typeof(DisplayImage) || i.GetType() == typeof(ClearImage))
            {
                i.PlayEventFunction();
            }
            // Instantaneously performs character movement
            else if (i.GetType() == typeof(MoveByVectors))
            {
                k = (MoveByVectors)i;
                k.MoveInstantly();
            }

            // Delays until the next event is ready, primarily used to keep flow during scene
            // changes. Ironic that this exists
            yield return new WaitUntil(() => i.eventComplete == true);
        }

        //Exits Cutscene State
        EndCutscene();
    }

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Resets the list of NPCs used in cutscenes. Typically occurs during scene transitions.
    /// </summary>
    public void ResetCharacterList()
    {
        // Removes all objects from the list
        cutsceneObjects.Clear();
        cutsceneObjects.TrimExcess();

        // Re-adds the player;
        cutsceneObjects.Add(PlayerController.playerController.gameObject);
    }

    #endregion
}
