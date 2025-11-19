/*=================================================================================================
 * FILE     : TickTokenGlare.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/19/25
 * UPDATED  : 11/19/25
 * 
 * DESC     : Controls visibility of glare effect over Tick Token Screen
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TickTokenGlare : MonoBehaviour
{
    #region VARIABLES

    // Object refs
    Image _glareImage;

    // Colors
    Color _partialOpacity = new Color(255, 255, 255, .5f);

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _glareImage = GetComponent<Image>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        // Ensures image is set
        if (_glareImage == null)
        {
            _glareImage = GetComponent<Image>();
        }

        // Checks game state and adjusts potency of glare effect
        if (!PlayerController.playerController.inBikeableArea)
        {
            _glareImage.color = Color.clear;
        }
        else if (GlobalVariableTracker.progressionFlags["inDelivery"])
        {
            _glareImage.color = Color.white;
        }
        else
        {
            _glareImage.color = _partialOpacity;
        }
    }

    #endregion
}
