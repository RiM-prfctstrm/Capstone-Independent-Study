/*=================================================================================================
 * FILE     : ScreenEffects
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/8/25
 * UPDATED  : 1/8/25
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

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Controls fading effects
        if (fadingOut)
        {
            FadeToBlack();
        }
    }

    #endregion

    #region FADES

    /// <summary>
    /// Fades the screen to black. In the future may be modified to enable multiple colors
    /// </summary>
    public void FadeToBlack()
    {
        if (_fadeAlpha < 1)
        {
            _fadeAlpha += _fadeSpeed * Time.deltaTime;
            //_blackFader.color = 
        }
        // Ends fade
        else
        {
            fadingOut = false;
        }
    }

    #endregion
}
