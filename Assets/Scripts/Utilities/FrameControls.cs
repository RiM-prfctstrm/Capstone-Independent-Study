/*=================================================================================================
 * FILE     : FrameControls.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/23/25
 * UPDATED  : 10/24/25
 * 
 * DESC     : Changes the game window from just showing the game to surrounding it with a
              fullscreen border, and vice versa.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FrameControls : MonoBehaviour
{
    #region VARIABLES

    // Cameras
    [SerializeField] Camera _frameCamera;
    [SerializeField] Camera _gameCamera;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    void Awake()
    {
        EnableFullscreen();
    }

    #endregion

    #region SCREEN MODIFICATION

    /// <summary>
    /// Sets the application to fullscreen and ensures the regular camera is functioning properly
    /// </summary>
    void EnableFullscreen()
    {
        // Full screens game
        Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, true);

        // Aligns and centers gamewindow
        _gameCamera.ViewportToScreenPoint(new Vector2(.5f, .5f));
    }

    #endregion
}
