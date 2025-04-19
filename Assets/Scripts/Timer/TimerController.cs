/*=================================================================================================
 * FILE     : TimerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 4/19/25
 * 
 * DESC     : Controls countdown timer that ends the game when it reaches 0.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    #region VARIABLES

    // Class singleton
    public static TimerController timerController;

    // Countdown function
    IEnumerator _countdownRoutine;

    // Countdown holders
    int _pausedTime;
    int _timeRemaining;

    // UI Objects
    [SerializeField] TextMeshProUGUI _countdownClock;
    [SerializeField] GameObject _timerBG;

    // Signals
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

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    #region TIMER FUNCTIONS

    /// <summary>
    /// Prepares timer's initial state and begins countdown
    /// </summary>
    /// <param name="startSeconds">Total time when the timer is first started, in seconds</param>
    public void BeginTimer(int startSeconds)
    {
        // Initializes countdown and UI
        _timeRemaining = startSeconds;
        ShowClock();
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
        }
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

    #endregion

    #region VISUAL INDICATORS

    /// <summary>
    /// Activates clock display
    /// </summary>
    void ShowClock()
    {

    }

    /// <summary>
    /// Sets the clock display to show current time remaining
    /// </summary>
    void UpdateHUDClock()
    {
        Debug.Log(_timeRemaining);
    }

    /// <summary>
    /// Deactivates Clock Display
    /// </summary>
    void HideClock()
    {

    }

    #endregion
}
