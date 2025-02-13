/*=================================================================================================
 * FILE     : CutsceneTrigger.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/11/24 (Veteran's Day [USA] / Singles' Day [China])
 * UPDATED  : 2/13/25
 * 
 * DESC     : Triggers a cutscene when the player enters. Presently this is sometimes used to
 *            continue a cutscene that has already begun due to a bug where dialogue gerts skipped
 *            after the player gets warped to a point in the same map.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    #region VARIABLES

    // Cutscene object
    [SerializeField] Cutscene _cutscene;

    // Control logic
    [SerializeField] bool _oneTimeTrigger = false;
    bool _hasPlayed = false;

    #endregion

    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.playerController.gameObject)
        {
            // Triggers a cutscene under correct conditions
            if (!_hasPlayed || !_oneTimeTrigger)
            {
                // Stops movement
                collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                // Plays Cutscene
                CutsceneManager.cutsceneManager.StartCutscene(_cutscene);

                // Checks that cutscene has played
                _hasPlayed = true;
            }
        }
    }

    #endregion
}
