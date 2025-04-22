/*=================================================================================================
 * FILE     : ExitGameOver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/20/25
 * UPDATED  : 4/22/25
 * 
 * DESC     : Returns to the title screen when any button is pressed.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitGameOver : MonoBehaviour
{

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Loads title screen on button press
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }

    #endregion

}
