/*=================================================================================================
 * FILE     : TickTokenGlare.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/19/25
 * UPDATED  : 3/25/26
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
    BootManager _currentSceneRef;

    // Colors
    Color _fullGlare = new Color(250, 249, 255, .9f);
    Color _partialGlare = new Color(250, 249, 255, .75f);

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
        // Inits Vars
        _currentSceneRef = FindObjectOfType<BootManager>();

        // Ensures image is set
        if (_glareImage == null)
        {
            _glareImage = GetComponent<Image>();
        }

        // Checks game state and adjusts potency of glare effect
        if (_currentSceneRef.indoorScene)
        {
            _glareImage.color = Color.clear;
        }
        else if (GlobalVariableTracker.progressionFlags["inDelivery"])
        {
            _glareImage.color = _fullGlare;
        }
        else
        {
            _glareImage.color = _partialGlare;
        }
    }

    #endregion
}
