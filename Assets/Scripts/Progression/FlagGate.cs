/*=================================================================================================
 * FILE     : FlagGate.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/4/25
 * UPDATED  : 2/4/25
 * 
 * DESC     : Objects with this script attached will only be active if the specified flag is in
 *            the specified state.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagGate : MonoBehaviour
{
    #region VARIABLES

    // Activation specifications
    [SerializeField] string _keyflag;
    [SerializeField] bool _activeState;

    #endregion

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Disables object if flag does not match active state
        if (GlobalVariableTracker.progressionFlags[_keyflag] != _activeState)
        {
            gameObject.SetActive(false);
        }
    }
}
