/*=================================================================================================
 * FILE     : MapSetup.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/25/25
 * UPDATED  : 2/25/25
 * 
 * DESC     : Places and sizes map icons when map is first unloade
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSetup : MonoBehaviour
{
    #region VARIABLES

    // Object references
    // Visible UI
    [SerializeField] Image _playerMarker;
    [SerializeField] Image _targetMarker;
    // Backend information
    BootManager _currentSceneRef;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        // Sets vars
        _currentSceneRef = FindObjectOfType<BootManager>();

        // Sets map display
        SetPlayerMarkerPosition();
    }

    #endregion

    #region SETUP FUNCTIONS

    /// <summary>
    /// Sets the player map marker position based on the player's position and a scene-specific
    /// offset
    /// </summary>
    void SetPlayerMarkerPosition()
    {
        _playerMarker.rectTransform.localPosition =
            PlayerController.playerController.transform.position;
    }

    #endregion
}
