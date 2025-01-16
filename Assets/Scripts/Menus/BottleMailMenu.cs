/*=================================================================================================
 * FILE     : BottleMailMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/16/25
 * UPDATED  : 1/16/25
 * 
 * DESC     : Controls BottleMail menu behavior to emulate an email program.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleMailMenu : MonoBehaviour
{
    #region VARIABLES

    // Objects
    [SerializeField] Button _defaultSelection;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Disables regular player input
        PlayerController.playerController.TogglePlayerInput();

        // Ensures button is selected
        _defaultSelection.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
