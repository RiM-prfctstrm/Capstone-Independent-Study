/*=================================================================================================
 * FILE     : AntiBikeZone.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/5/26
 * UPDATED  : 2/5/26
 * 
 * DESC     : Prevents the player from using their bike while inside the trigger
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBikeZone : MonoBehaviour
{
    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when Collider2D other enters the trigger.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Makes the game think it's indoors.
        if (collision.tag == "Player")
        {
            PlayerController.playerController.inBikeableArea = false;
        }
    }

    /// <summary>
    /// OnTriggerEnter2D is called when Collider2D other has stopped touching the trigger.
    /// </summary>
    void OnTriggerExit2D(Collider2D collision)
    {
        // Makes the game think it's indoors.
        if (collision.tag == "Player")
        {
            PlayerController.playerController.inBikeableArea = true;
        }
    }

    #endregion
}
