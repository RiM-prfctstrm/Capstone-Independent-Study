/*=================================================================================================
 * FILE     : InteractableObject.cs (Originally Object.cs)
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/6/23
 * UPDATED  : 10/11/24
 * 
 * DESC     : Base class for different types of interactible objects
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    /// <summary>
    /// Shell function for behavior when the player interacts
    /// </summary>
    public virtual void OnInteractedWith()
    {

    }
}
