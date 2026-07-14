/*=================================================================================================
 * FILE     : NightFilterVisibility.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 7/14/26
 * UPDATED  : 7/14/26
 * 
 * DESC     : Checks whether the night filter is active when a scene loads, and if so, shows image.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NightFilterVisibility : MonoBehaviour
{
    #region VARIABLES

    // Image component
    [SerializeField] Image _filterImg;
    Color _imgColor;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        _imgColor = _filterImg.color;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion

    #region SCENE EVENTS

    /// <summary>
    /// Determines whether to enable the night filter
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GlobalVariableTracker.progressionFlags["nightFilter"])
        {
            _imgColor.a = .25f;
            _filterImg.color = _imgColor;
        }
        else
        {
            _imgColor.a = 0f;
            _filterImg.color = _imgColor;
        }
    }

    #endregion
}
