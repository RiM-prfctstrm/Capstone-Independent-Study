/*=================================================================================================
 * FILE     : ExitGameOver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/20/25
 * UPDATED  : 8/25/25
 * 
 * DESC     : Controls entire game over screen
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitGameOver : MonoBehaviour
{
    #region VARS

    // Object Refs
    [SerializeField] Button _dummyButton;
    [SerializeField] GameObject _evSysObj;
    [SerializeField] ScreenEffects _effects;
    [SerializeField] Button _firstOption;
    CanvasGroup _menuBack;
    [SerializeField] Graphic _movingText;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits Vars
        _menuBack = GetComponent<CanvasGroup>();

        // Starts effects
        
        StartCoroutine(EntryEffectsRunner());
    }

    #endregion

    #region VFX SCRIPTS

    /// <summary>
    /// Coroutine that choreographs the audio and visual effects when the scene is booted
    /// </summary>
    /// <returns>length of tick update</returns>
    IEnumerator EntryEffectsRunner()
    {
        // Vars
        // Counts ticks since function starts to coordinate all effects in a single coroutine
        int tick = 0;

        // Effect Params
        Color textColor = new Color(255, 0, 0, 0);

        // Effects that start instantly
        ScreenEffects.fadingIn = true;

        // Update loop
        while (true)
        {
            // Fades and positions main text
            if (tick >= 180 && tick < 380)
            {
                // Updates params
                textColor.a += .02f;

                // Updates object
                _movingText.rectTransform.localPosition += Vector3.down * .25f;
                if (_movingText.GetComponent<TextMeshProUGUI>().color.a < 1)
                {
                    _movingText.GetComponent<TextMeshProUGUI>().color = textColor;
                }
            }

            // Fades in menu
            if (tick > 440)
            {
                _menuBack.alpha += 1f * Time.deltaTime;
                
                // Exits sequence once menu finishes fading
                if (_menuBack.alpha == 1)
                {
                    // Selects menu button
                    _evSysObj.SetActive(true);
                    _firstOption.Select();

                    // Sets speed to fade out faster
                    _effects.fadeSpeed = 2;

                    break;
                }
            }

            // Delays loop execution and updates tick
            yield return new WaitForEndOfFrame();
            tick++;
        }
    }

    /// <summary>
    /// Choreographs effects when closing the menu before changing scene
    /// </summary>
    /// <returns>Delay before scene ends</returns>
    IEnumerator ExitEffectRunner(bool retry)
    {
        // Deselects button
        _dummyButton.Select();

        // Delays
        yield return new WaitUntil(() => !ScreenEffects.fadingOut);

        // Sends to next scene
        if (retry)
        {
            SceneManager.LoadScene("GameStore");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    #endregion

    #region MENU BUTTONS

    /// <summary>
    /// Returns player to DDD at the start of the current delivery and reenables the delivery clock
    /// </summary>
    public void RetryDelivery()
    {
        ScreenEffects.fadingOut = true;
        StartCoroutine(ExitEffectRunner(true));
    }

    /// <summary>
    /// Returns to the title
    /// </summary>
    public void QuitToTitle()
    {
        // Resets data
        DebugProgressInjector resetter = gameObject.AddComponent<DebugProgressInjector>();
        resetter.InjectGlobalData();

        // Loads menu
        ScreenEffects.fadingOut = true;
        StartCoroutine(ExitEffectRunner(false));
    }

    #endregion

}
