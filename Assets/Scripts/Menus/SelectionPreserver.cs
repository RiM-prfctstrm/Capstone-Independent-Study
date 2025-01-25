/*=================================================================================================
 * FILE     : SelectionPreserver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/25/25
 * UPDATED  : 1/25/25
 * 
 * DESC     : Makes sure a button is always selected if player clicks off menu. Adapted and partly
 *            copied from support answer on this thread:
 *            https://discussions.unity.com/t/possible-to-always-have-an-element-selected/718572/2
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionPreserver : MonoBehaviour
{
    #region VARIABLES

    // Event System
    EventSystem _eventSystem;

    // Selection
    GameObject _currentlySelected;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Init Vars
        _eventSystem = GetComponent<EventSystem>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Tells which variable was last selected
        if (_eventSystem.currentSelectedGameObject != null &&
            _currentlySelected != _eventSystem.currentSelectedGameObject)
        {
            _currentlySelected = _eventSystem.currentSelectedGameObject;
        }

        // The currentSelectedGameObject will be null when you click with your
        // anywhere on the screen on a non-Selectable GameObject.
        if (_eventSystem.currentSelectedGameObject == null)
        {
            // If this happens simply re-select the last known selected GameObject.
            if (_currentlySelected != null)
            {
                _currentlySelected.GetComponent<Selectable>().Select();
            }
            else
            {
                // If there is none, select the firstSelectedGameObject
                // (which can be setup inthe EventSystem component).
                _currentlySelected = _eventSystem.firstSelectedGameObject;
                _currentlySelected.GetComponent<Selectable>().Select();
            }
        }
    }

    #endregion
}
