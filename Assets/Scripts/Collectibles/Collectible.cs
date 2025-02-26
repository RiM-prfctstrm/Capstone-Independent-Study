/*=================================================================================================
 * FILE     : Collectible.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/22/25
 * UPDATED  : 2/26/25
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
        if (collision.gameObject == PlayerController.playerController.gameObject)
        {
            GlobalVariableTracker.collectiblesInPocket++;
        }
    }

    #endregion
}
