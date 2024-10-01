/*=================================================================================================
 * FILE     : Object.cs
 * AUTHOR   : prfctstrm479
 * CREATION : 6/6/23
 * UPDATED  : 6/6/23
 * 
 * DESC     : Base class for different types of interactible objects
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    /// <summary>
    /// Shell function for behavior when the player interacts
    /// </summary>
    public virtual void OnInteractedWith()
    {

    }
}
