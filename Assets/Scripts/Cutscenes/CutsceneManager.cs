/*=================================================================================================
 * FILE     : CutsceneManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1X/X/24
 * UPDATED  : 4/19/25
 * 
 * DESC     : Controls the progression of scripted events.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    #region VARIABLES

    // Manager Singleton
    public static CutsceneManager cutsceneManager;

    // Object References, typically to allow event SOs to refer to objects in the scene
    [SerializeField] GameObject _UISpace;
    public GameObject UISpace => _UISpace;
    [SerializeField] Slider _skipGauge;
    [SerializeField] GameObject _skipHUD;

    // A list of characters that can be used in cutscenes. This keeps the cutscene functions
    // uncluttered by NPCs that aren't used by them, making the object references easier to keep
    // track of.
    public List<GameObject> cutsceneObjects = new List<GameObject>();

    // Signals
    static bool _inCutscene = false;
    public static bool inCutscene => _inCutscene;
    static bool _skippingCutscene = false;
    public static bool skippingCutscene => _skippingCutscene;

    // Skip Position Determinants
    Cutscene _skipEvent;
    int _skipPos;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called every fixed framerate frame.
    /// </summary>
    void FixedUpdate()
    {
        ControlSkipUI();
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
        PlayerController.playerController.cancel.Enable();
        _inCutscene = true;
        _skipEvent = cutscene;
        _skipPos = 0;

        // Plays cutscene
        cutscene.hasPlayed = true;
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
            i.eventComplete = false;
            i.PlayEventFunction();
            yield return new WaitUntil(() => i.eventComplete == true);
            _skipPos++;
        }

        // Ends cutscene
        EndCutscene();
    }

    /// <summary>
    /// Returns Game to normal gameplay State
    /// </summary>
    public void EndCutscene()
    {
        // Deactivates cutscene mode
        _inCutscene = false;
        PlayerController.playerController.cancel.Disable();
        PlayerController.playerController.TogglePlayerInput();

        // Reeneables cancelling if going into menu
        if (InGameMainMenu.inMainMenu)
        {
            PlayerController.playerController.cancel.Enable();
        }
    }

    #endregion

    #region CUTSCENE SKIPPING

    #region SKIPPING FUNCTIONS

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

        // Signals skipping
        _skippingCutscene = true;

        // Fades out for skip
        ScreenEffects.fadingOut = true;

        // Loops through cutscene events
        for (int j = _skipPos - 1; j < _skipEvent.cutsceneScript.Count; j++)
        {
            if (j < 0)
            {
                j = 0;
            }

            // Set event
            i = _skipEvent.cutsceneScript[j];
            i.eventComplete = false;

            // Skips events that do not alter game state beyond cosmetics within cutscene
            if (i.GetType() == typeof(CutsceneDialogue) || i.GetType() == typeof(ScriptedWait) ||
                i.GetType() == typeof(FadeForCutscene) || i.GetType() == typeof(PlaySound) ||
                i.GetType() == typeof(DisplayImage))
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
                     i.GetType() == typeof(ChangeActiveState) ||
                     i.GetType() == typeof(ClearImage) ||
                     i.GetType() == typeof(BranchScene) || i.GetType() == typeof(QuitToTitle) ||
                     i.GetType() == typeof(ReturnToMenu) || i.GetType() == typeof(ResetProgress) ||
                     i.GetType() == typeof(PauseTimer) || i.GetType() == typeof(ResumeTimer))
            {
                i.PlayEventFunction();

                // Fades in for choice
                if (i.GetType() == typeof(BranchScene))
                {
                    ScreenEffects.fadingIn = true;

                    // Stops skipping coroutine
                    _skippingCutscene = false;
                    yield break;
                }
            }
            // Instantaneously performs character movement
            else if (i.GetType() == typeof(MoveByVectors) || i.GetType() == typeof(MoveToPoint))
            {
                k = (MoveByVectors)i;
                k.MoveInstantly();
            }

            // Delays until the next event is ready, primarily used to keep flow during scene
            // changes. Ironic that this exists
            yield return new WaitUntil(() => i.eventComplete == true &&
                !ScreenEffects.fadingOut);
        }

        // Fades screen back in
        ScreenEffects.fadingIn = true;

        // Exits Cutscene State
        _skippingCutscene = false;
        EndCutscene();
    }

    #endregion

    #region SKIPPING UI

    /// <summary>
    /// Shows, hides, and progresses the gauge to skip cutscenes, as well as displaying
    /// notifications if the player can't skip at the moment.
    /// </summary>
    void ControlSkipUI()
    {

        // Fills gauge to skip event
        if (PlayerController.playerController.cancel.IsPressed() && !_skippingCutscene &&
            (_inCutscene || NPCInteraction.inNPCInteraction))
        {
            // Fills Gauge
            if (!DialogueManager.dialogueManager.choiceMenu.activeInHierarchy)
            {
                CollectibleManager.collectibleManager.GetComponent<AudioSource>().mute = true;
                _skipGauge.value += Time.fixedDeltaTime;
            }

            // Shows background
            if (!_skipHUD.activeInHierarchy)
            {
                _skipHUD.SetActive(true);
            }
        }
        // Resets gauge when button removed
        else if (_skipGauge.value != 0)
        {
            CollectibleManager.collectibleManager.GetComponent<AudioSource>().mute = false;
            PlayerController.playerController.PlayCancelSound();
            _skipGauge.value = 0;
            _skipHUD.SetActive(false);
        }

        // Skips cutscene or dialogue (all done here for simplicity's sake, and because I plan to
        // merge these systems after capstone)
        if (_skipGauge.value == 1)
        {
            // Resets UI Vars
            _skipGauge.value = 0;
            _skipHUD.SetActive(false);

            // Plays skipping soundeffect
            CollectibleManager.collectibleManager.GetComponent<AudioSource>().mute = false;
            PlayerController.playerController.PlayCancelSound();

            // Performs skip
            if (_inCutscene)
            {
                SkipCutscene();
            }
            else if (DialogueManager.dialogueInProgress)
            {
                DialogueManager.dialogueManager.CancelDialogue();
            }
        }

        // Ensures skip HUD is hidden when taking finger off button
        if (!PlayerController.playerController.cancel.IsPressed() &&
            _skipHUD.activeInHierarchy)
        {
            CollectibleManager.collectibleManager.GetComponent<AudioSource>().mute = false;
            PlayerController.playerController.PlayCancelSound();
            _skipGauge.value = 0;
            _skipHUD.SetActive(false);
        }
    }

    #endregion

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
