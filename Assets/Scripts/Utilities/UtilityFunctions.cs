/*=================================================================================================
 * FILE     : UtilityFunctions.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/9/24
 * UPDATED  : 11/9/24
 * 
 * DESC     : Miscellaneous functions that are used in multiple circumstances and don't fit evenly
 *            in any other class.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions : MonoBehaviour
{
    #region MOVEMENT FUNCTIONS

    /// <summary>
    /// Used to warp the player to a point in the same scene. Presently called from
    /// SceneTransition, may rewrite so that it works the other way around.
    /// </summary>
    /// <param name="point">Position to warp the player to</param>
    /// <param name="direction">Direction the player faces</param>
    public static void WarpToPoint(Vector3 point, int direction)
    {
        // Teleports Player
        Debug.Log("teleportation yeah!");
        PlayerController.playerController.transform.position = point;
        PlayerController.playerController.GetComponent<PlayerAnimator>().facingDirection
            = direction;
    }

    #endregion
}
