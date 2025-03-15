/*=================================================================================================
 * FILE     : LinkActiveState.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/15/25
 * UPDATED  : 3/15/25
 * 
 * DESC     : Used to control the active state of a non-child gameobject by matching it to the 
 *            attached object's.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkActiveState : MonoBehaviour
{
    #region VARIABLES
    
    // GameObject to link
    [SerializeField] GameObject _psuedochild;

    // Bool to invert relationship
    [SerializeField] bool _invertState = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        if (!_invertState)
        {
            _psuedochild.SetActive(true);
        }
        else
        {
            _psuedochild.SetActive(false);
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        if (!_invertState)
        {
            _psuedochild.SetActive(false);
        }
        else
        {
            _psuedochild.SetActive(true);
        }
    }

    #endregion
}
