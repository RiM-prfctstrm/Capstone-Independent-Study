/*=================================================================================================
 * FILE     : ScreenEffects
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/8/25
 * UPDATED  : 3/25/25
 * 
 * DESC     : Performs visual effects that take up the whole screen.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    #region VARIABLES

    // Class Singleton
    public static ScreenEffects screenEffects;

    // Fading Vars
    // Fading controls
    float _fadeAlpha = 0;
    [SerializeField] float _fadeSpeed = 1;
    public static bool fadingIn = false;
    public static bool fadingOut = false;
    // Fading Objects
    [SerializeField] Image _blackFader;
    public static Image blackFader;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initializes Vars
        blackFader = _blackFader;
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Controls fading effects
        if (fadingOut)
        {
            FadeToBlack();
        }
        if (fadingIn)
        {
            FadeIn();
        }
    }

    #endregion

    #region FADES

    /// <summary>
    /// Fades the screen to black. In the future may be modified to enable multiple colors
    /// </summary>
    void FadeToBlack()
    {
        // Performs fade by increasing object opacity
        if (_fadeAlpha < 1)
        {
            _fadeAlpha += _fadeSpeed * Time.fixedDeltaTime;
            _blackFader.color = new Color(0, 0, 0, _fadeAlpha);
        }
        // Ends fade
        else
        {
            _fadeAlpha = 1;
            fadingOut = false;
        }
    }

    /// <summary>
    /// Fades in from a color by lowering its alpha
    /// </summary>
    void FadeIn()
    {
        // Performs fade by decreasing object opacity
        if (_fadeAlpha > 0)
        {
            _fadeAlpha -= _fadeSpeed * Time.fixedDeltaTime;
            _blackFader.color = new Color(0, 0, 0, _fadeAlpha);
        }
        // Ends fade
        else
        {
            _fadeAlpha = 0;
            fadingIn = false;
        }
    }

    #endregion
}
