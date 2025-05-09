/*=================================================================================================
 * FILE     : CollectibleGate.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/29/25
 * UPDATED  : 4/30/25
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
    [SerializeField] Cutscene _moveBackEvent;
    [SerializeField] Cutscene _firstTriggerEvent; // Make sure to write dialogue that can work
    // either if it ends with this or immediately goes to _passEvent;
    [SerializeField] Cutscene _passEvent;

    // Control bools
    bool _hasBeenTriggered;
    [SerializeField] string _initialMarkerVar;

    #endregion

    #region COLLISION lOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.playerController.gameObject)
        {
            // Makes sure variables are properly set
            _hasBeenTriggered = GlobalVariableTracker.progressionFlags[_initialMarkerVar];

            // Pauses timer
            TimerController.timerController.PauseTimer();

            // Gets player in cutscene state (copied from CutsceneTrigger.cs cause I'm too tired to
            // be elegant.
            // Stops movement and exits any menus
            PlayerController.playerController.CancelMomentum();
            if (InGameMainMenu.inGameMainMenu.isActiveAndEnabled)
            {
                InGameMainMenu.inGameMainMenu.CloseSubmenus();
                InGameMainMenu.inGameMainMenu.ExitMenu();
                PlayerController.playerController.TogglePlayerInput();
            }

            // Emergency input toggle
            if (PlayerController.playerController.movementDisabled)
            {
                PlayerController.playerController.TogglePlayerInput();
            }

            // Scene control logic
            if (!_hasBeenTriggered)
            {
                CutsceneManager.cutsceneManager.StartCutscene(_firstTriggerEvent);
                StartCoroutine(FirstTriggerEnding());
            }
            else if (GlobalVariableTracker.collectiblesInPocket >= _threshold)
            {
                CutsceneManager.cutsceneManager.StartCutscene(_passEvent);
            }
            else
            {
                CutsceneManager.cutsceneManager.StartCutscene(_blockEvent);
                StartCoroutine(FirstTriggerEnding());
            }
        }
            
    }

    #endregion

    #region POST-EVENT FUNCTIONS

    /// <summary>
    /// Controls whether to play the pass event or push player back after the first trigger
    /// </summary>
    IEnumerator FirstTriggerEnding()
    {
        // Delays until initial event is over
        yield return new WaitUntil(() => !CutsceneManager.inCutscene);

        // Allows player through
        if (GlobalVariableTracker.collectiblesInPocket >= _threshold)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_passEvent);
        }
        else
        {
            CutsceneManager.cutsceneManager.StartCutscene(_moveBackEvent);
        }
    }

    #endregion
}
