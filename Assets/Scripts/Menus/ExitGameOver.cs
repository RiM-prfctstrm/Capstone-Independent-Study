/*=================================================================================================
 * FILE     : ExitGameOver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/20/25
 * UPDATED  : 8/15/25
 * 
 * DESC     : Lets the player choose whether to return to the title screen or retry the last
 *            delivery after they fail it.
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

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

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
            if (tick >= 90 && tick < 190)
            {
                // Updates params
                textColor.a += .02f;

                // Updates object
                _movingText.rectTransform.localPosition += Vector3.down * .5f;
                if (_movingText.GetComponent<TextMeshProUGUI>().color.a < 1)
                {
                    _movingText.GetComponent<TextMeshProUGUI>().color = textColor;
                }
            }

            // Fades in menu
            if (tick > 250)
            {
                _menuBack.alpha += 1f * Time.deltaTime;
                
                // Exits sequence once menu finishes fading
                if (_menuBack.alpha == 1)
                {
                    // Selects menu button
                    _firstOption.Select();

                    break;
                }
            }

            // Delays loop execution and updates tick
            yield return new WaitForEndOfFrame();
            tick++;
        }
    }

    #endregion

    #region MENU BUTTONS

    /// <summary>
    /// Returns player to DDD at the start of the current delivery and reenables the delivery clock
    /// </summary>
    public void RetryDelivery()
    {
        SceneManager.LoadScene("GameStore");
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
        SceneManager.LoadScene(0);
    }

    #endregion

}
