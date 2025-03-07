/*=================================================================================================
 * FILE     : CollectibleManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 3/7/25
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

    // Timer var
    [SerializeField] float _hideDelay;
    float _timeToHide;

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
        // Performs adjustment
        GlobalVariableTracker.collectiblesInPocket += amount;
        if (GlobalVariableTracker.collectiblesInPocket < 0)
        {
            GlobalVariableTracker.collectiblesInPocket = 0;
        }

        // Shows player current total
        _HUDObject.SetActive(true);
        _HUDCounter.text = GlobalVariableTracker.collectiblesInPocket.ToString();
        _timeToHide = _hideDelay;

        // Changes display on map counter
        _mapCounter.text = GlobalVariableTracker.collectiblesInPocket.ToString();
    }

    #endregion
}
