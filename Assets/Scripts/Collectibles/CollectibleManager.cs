/*=================================================================================================
 * FILE     : CollectibleManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 3/11/25
 * 
 * DESC     : While the actual collectible count is stored in GlobalVariableTracker, this script
 *            modifies that count and controls displays
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    #region VARIABLES

    // Class Singleton
    public static CollectibleManager collectibleManager;

    // UI Object reference
    [SerializeField] TextMeshProUGUI _HUDCounter;
    [SerializeField] GameObject _HUDObject;
    [SerializeField] TextMeshProUGUI _mapCounter;

    // Timer vars
    [SerializeField] float _hideDelay;
    float _timeToHide;

    // Sound Effects
    [SerializeField] AudioClip _countdownSound;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Hides pop-up display after a while
        if (_timeToHide > 0)
        {
            _timeToHide -= Time.fixedDeltaTime;
        }
        else if (_HUDObject.activeInHierarchy)
        {
            _HUDObject.SetActive(false);
        }
    }

    #endregion

    #region VARIABLE MANAGEMENT

    /// <summary>
    /// Adjusts how many collectibles the player carries on hand
    /// </summary>
    /// <param name="amount">The amount to adjust by</param>
    public void AdjustCount(int amount)
    {
        // Tracks previous number
        int previousNumber = (int)GlobalVariableTracker.collectiblesInPocket;

        // Performs adjustment
        GlobalVariableTracker.collectiblesInPocket += amount;
        if (GlobalVariableTracker.collectiblesInPocket < 0)
        {
            GlobalVariableTracker.collectiblesInPocket = 0;
        }

        // Controls whether to instantly display variables or animate their decline
        if (amount > 0)
        {
            SetCountDisplay(GlobalVariableTracker.collectiblesInPocket.ToString());
        }
        else
        {
            StartCoroutine(
                CountDown(previousNumber, (int)GlobalVariableTracker.collectiblesInPocket));
        }
    }

    /// <summary>
    /// Resets the number of collectibles the player has
    /// </summary>
    public void ResetCount()
    {
        // Zeroes collectible count
        GlobalVariableTracker.collectiblesInPocket = 0;

        // Changes display on map counter
        _mapCounter.text = GlobalVariableTracker.collectiblesInPocket.ToString();
    }

    #endregion

    #region DISPLAY CONTROLS

    /// <summary>
    /// Displays current collectible
    /// </summary>
    /// <param name="displayNo">Number of collectibles, converted to a string</param>
    void SetCountDisplay(string displayNo)
    {
        // Shows player current total
        _HUDObject.SetActive(true);
        _HUDCounter.text = displayNo;
        _timeToHide = _hideDelay;

        // Changes display on map counter
        _mapCounter.text = displayNo;
    }

    /// <summary>
    /// Rapidly counts down collectible total in display
    /// </summary>
    /// <param name="startNum">Total before loss</param>
    /// <param name="endNum">Total after loss</param>
    /// <returns></returns>
    IEnumerator CountDown(int startNum, int endNum)
    {
        // Initailizes display
        SetCountDisplay(startNum.ToString());

        // Loops through subtraction
        while (startNum > endNum)
        {
            // Delays for effect visibility
            yield return new WaitForFixedUpdate();

            // Subtracts from count
            startNum--;
            _HUDCounter.text = startNum.ToString();

            // Keeps display active
            _timeToHide += Time.fixedDeltaTime;

            // Audio Effect
            PlayerController.playerController.playerAudioSource.PlayOneShot(_countdownSound);
        }
    }

    #endregion
}
