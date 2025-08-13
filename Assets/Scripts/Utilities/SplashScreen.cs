/*=================================================================================================
 * FILE     : SplashScreen.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/13/24
 * UPDATED  : 8/13/25
 * 
 * DESC     : Fades in and out a splash screen.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] Image _splash;
    Color _fadeColor = new Color(255, 255, 255, 0);

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        StartCoroutine(FadeSplashIn());
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
}
