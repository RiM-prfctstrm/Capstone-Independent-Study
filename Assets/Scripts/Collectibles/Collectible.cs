/*=================================================================================================
 * FILE     : Collectible.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 3/5/25
 * 
 * DESC     : Behaviour for items that can be picked up off the ground.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    #region VARIABLES

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            // Reducs pickup lag
            gameObject.isStatic = false;

            // Picks up collectible
            if (GlobalVariableTracker.collectiblesInPocket < 200)
            {
                GlobalVariableTracker.collectiblesInPocket++;
                GetComponent<Animator>().Play("Collect");
                GetComponent<Collider2D>().enabled = false;
            }
            // If any more collectibles would do nothing, plays an effect to show that the item
            // can't be picked up
            else
            {
                GetComponent<Animator>().Play("Full");
            }
        }
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
