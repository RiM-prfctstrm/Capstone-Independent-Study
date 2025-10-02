/*=================================================================================================
 * FILE     : SplashScreen.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/13/25
 * UPDATED  : 10/2/25
 * 
 * DESC     : Fades in and out a splash screen.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class SplashScreen : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] Image _splash;
    Color _fadeColor = new Color(255, 255, 255, 0);
    bool _hasSkipped = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        StartCoroutine(FadeSplashIn());
    }
    
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Skips splash Screen
        if (!_hasSkipped)
        {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => SkipSplash());
            _hasSkipped = true;
        }
    }

    #endregion

    #region AUTOMATION ROUTINES

    /// <summary>
    /// Fades in the screen
    /// </summary>
    /// <returns>Delay for frame updates</returns>
    IEnumerator FadeSplashIn()
    {
        // Fade loop
        while (_splash.color.a < 1)
        {
            _fadeColor.a += 1 * Time.deltaTime; ;
            _splash.color = _fadeColor;
            yield return new WaitForEndOfFrame();
        }
        
        // Starts next part of sequence
        StartCoroutine(ReadDelay());
    }

    /// <summary>
    /// Keeps screen static for splash to be read
    /// </summary>
    /// <returns>Time the screen stays static</returns>
    IEnumerator ReadDelay()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(FadeSplashOut());
    }

    /// <summary>
    /// Fades out the screen
    /// </summary>
    /// <returns>Delay for frame updates</returns>
    IEnumerator FadeSplashOut()
    {
        // Fade loop
        while (_splash.color.a > 0)
        {
            _fadeColor.a -= 1 * Time.deltaTime;
            _splash.color = _fadeColor;
            yield return new WaitForEndOfFrame();
        }

        // Loads main menu
        SceneManager.LoadScene(1);
    }

    #endregion

    #region SKIP FUNCTIONALITY

    /// <summary>
    /// Skips splash screen
    /// </summary>
    void SkipSplash()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(1);
    }

    #endregion
}
