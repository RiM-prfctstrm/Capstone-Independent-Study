/*=================================================================================================
 * FILE     : Collectible.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 9/16/25
 * 
 * DESC     : Behaviour for items that can be picked up off the ground.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    #region VARIABLES

    // State trackers
    public int collectibleID = -1;
    public CollectibleStateTracker localTracker;

    // Audio
    [SerializeField] AudioClip _pickUpSound;

    #endregion

    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collisions with player
        if (collision.gameObject == PlayerController.playerController.gameObject)
        {
            // Picks up collectible
            OnPickUp();
        }
    }

    #endregion


    #region BASE COLLECTIBLE FUNCTIONALITY

    /// <summary>
    /// Functionality that occurs when the collectible is picked up. Base code plays feedback
    /// effects. Functionality of specific types of collectibles is delegated to child classes.
    /// </summary>
    protected virtual void OnPickUp()
    {
        // Reducs pickup lag
        gameObject.isStatic = false;

        // Picks up collectible
        //GetComponent<Animator>().Play("Collect");
        GetComponent<Collider2D>().enabled = false;
        PlayerController.playerController.playerAudioSource.PlayOneShot(_pickUpSound);
    }

    #endregion

    #region MISC FUNCTIONS

    /// <summary>
    /// Removes the collectible from the game space
    /// </summary>
    [SerializeField] void RemoveCollectible()
    {
        Destroy(gameObject);
    }

    #endregion
}
