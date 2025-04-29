/*=================================================================================================
 * FILE     : CollectibleGate.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/29/25
 * UPDATED  : 4/29/25
 * 
 * DESC     : A special trigger that plays different events depending on whether the number of
 *            collectibles the player holds passes a certain threshold. It has commands for the
 *            first time the player arrives, subsequent visits without enough collectibles, and
 *            a script to pass and delete the trigger once the player arrives with the threshold.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleGate : MonoBehaviour
{
    #region VARIABLES

    // Collectible threshold
    [SerializeField] int _threshold;

    // Events activated by trigger
    [SerializeField] Cutscene _blockEvent;
    [SerializeField] Cutscene _firstTriggerEvent; // Make sure to write dialogue that can work
    // either if it ends with this or immediately goes to _passEvent;
    [SerializeField] Cutscene _passEvent;

    // Control bools
    bool _hasBeenTriggered;
    [SerializeField] string _initialMarkerVar;

    #endregion

    #region COLLISION CONTROLS

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Makes sure variables are properly set
        _hasBeenTriggered = GlobalVariableTracker.progressionFlags[_initialMarkerVar];

        // Scene control logic
        if (!_hasBeenTriggered)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_firstTriggerEvent);
        }
        else if (GlobalVariableTracker.collectiblesInPocket > _threshold)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_passEvent);
        }
        else
        {
            CutsceneManager.cutsceneManager.StartCutscene(_blockEvent);
        }
    }

    #endregion
}
