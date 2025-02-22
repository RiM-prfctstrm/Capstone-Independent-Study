/*=================================================================================================
 * FILE     : CollectibleManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 2/22/25
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
    [SerializeField] TextMeshProUGUI _counter;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

        // Changes display on counter
        _counter.text = GlobalVariableTracker.collectiblesInPocket.ToString();
    }

    #endregion
}
