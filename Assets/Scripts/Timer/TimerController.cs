/*=================================================================================================
 * FILE     : TimerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 5/3/25
 * 
 * DESC     : Controls countdown timer that ends the game when it reaches 0.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerController : MonoBehaviour
{
    #region VARIABLES

    // Class singleton
    public static TimerController timerController;

    // Countdown function
    IEnumerator _countdownRoutine;

    // Countdown vars
    int _minuteCounter = 0;
    int _pausedTime;
    int _startingSeconds;
    int _timeRemaining;

    // UI Objects
    [SerializeField] TextMeshProUGUI _countdownClock;
    [SerializeField] GameObject _timerBG;

    // Display numbers
    int _displayMinutes;
    int _displaySeconds;
    string _displayText;

    // Signals
    [SerializeField] AudioClip _stopwatch;
    public static bool timerInProgress = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits vars
        _countdownRoutine = Countdown();
    }  

    #endregion

    #region TIMER FUNCTIONS

    /// <summary>
    /// Prepares timer's initial state and begins countdown
    /// </summary>
    /// <param name="startSeconds">Total time when the timer is first started, in seconds</param>
    /// <param name="startMinutes">Total time when the timer is first started, in seconds</param>
    public void BeginTimer(int startSeconds, int startMinutes)
    {
        // Initializes countdown and UI
        _startingSeconds = startSeconds;
        _timeRemaining = startSeconds;
        _displayMinutes = startMinutes;
        _minuteCounter = 58;
        _timerBG.SetActive(true);
        UpdateHUDClock();

        // Starts countdown
        timerInProgress = true;
        StartCoroutine(_countdownRoutine);
    }

    /// <summary>
    /// Counts down time the player has left
    /// </summary>
    /// <returns>1 Second intervals between timer updates</returns>
    IEnumerator Countdown()
    {
        // Counts down clock
        while (_timeRemaining > 0)
        {
            // Clock interval
            yield return new WaitForSecondsRealtime(1);

            // Clock update
            _timeRemaining--;
            UpdateHUDClock();

            // Plays a warning sound at specific intervals
            if (_timeRemaining == _startingSeconds / 2 || _timeRemaining == 60)
            {
                DialogueManager.dialogueManager.systemSounds.PlayOneShot(_stopwatch);
            }
        }

        // Performs functionality on ending the clock
        OnTimerReachZero();
    }

    /// <summary>
    /// Stops the timer and retains the current amount of time remaining
    /// </summary>
    public void PauseTimer()
    {
        StopCoroutine(_countdownRoutine);
        _pausedTime = _timeRemaining;
    }

    /// <summary>
    /// Contines timer, starting at _paused time
    /// </summary>
    public void ResumeTimer()
    {
        _timeRemaining = _pausedTime;
        StartCoroutine(_countdownRoutine);
    }

    /// <summary>
    /// Brings the timer to a stop and hides display
    /// </summary>
    public void StopTimer()
    {
        StopCoroutine(_countdownRoutine);
        timerInProgress = false;
        HideClock();
    }

    /// <summary>
    /// Triggers the game's end when the countdown concludes
    /// </summary>
    void OnTimerReachZero()
    {
        // Stops all current events
        StopTimer();
        StopAllCoroutines();

        // Stops game actions
        // Removes player inputs
        PlayerController.playerController.ClearAllInputFunctions();
        // Completes Event
        if (CutsceneManager.inCutscene)
        {
            CutsceneManager.cutsceneManager.EndCutscene();
        }
        // Fade effect
        ScreenEffects.fadingOut = true;

        // Exits game
        StartCoroutine(GameOver());
    }

    #endregion

    #region VISUAL INDICATORS

    /// <summary>
    /// Sets the clock display to show current time remaining
    /// </summary>
    void UpdateHUDClock()
    {
        // Reverts displat text
        _displayText = "";

        // Updates minute display
        _minuteCounter++;
        if (_minuteCounter == 60)
        {
            _displayMinutes--;
            _minuteCounter = 0;
        }
        if (_displayMinutes < 10)
        {
            _displayText += 0;
        }
        _displayText += _displayMinutes + ":";

        // Updates seconds display
        _displaySeconds = _timeRemaining % 60;
        if(_displaySeconds < 10)
        {
            _displayText += 0;
        }
        _displayText += _displaySeconds;

        // Displays time remaining
        _countdownClock.text = _displayText;
        //Debug.Log(_timeRemaining);
    }

    /// <summary>
    /// Deactivates Clock Display
    /// </summary>
    void HideClock()
    {
        _timerBG.SetActive(false);
    }

    #endregion


    #region GAME OVER
    
    /// <summary>
    /// Runs failure state. Basically copied from QuitToTitle.cs with one var changed
    /// </summary>
    IEnumerator GameOver()
    {
        // Delay for fade
        yield return new WaitUntil(() => ScreenEffects.fadingOut == false);

        // Resets global variables
        CollectibleManager.collectibleManager.ResetCount();
        InGameMainMenu.inMainMenu = false;
        InGameMainMenu.inGameMainMenu.ExitMenu();

        // Resets progress
        DebugProgressInjector resetter = gameObject.AddComponent<DebugProgressInjector>();
        resetter.InjectGlobalData();

        // Returns the game to the title screen and deletes the scene essentials, which are not
        // meant to exist there
        SceneManager.LoadScene("GameOver");
        Destroy(EssentialPreserver.instance.gameObject);
        EssentialPreserver.instance = null;
    }

    #endregion
}
