/*=================================================================================================
 * FILE     : FadeForCutscene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/15/25
 * UPDATED  : 2/15/25
 * 
 * DESC     : Commands a screen fade as part of a cutscene
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fade", menuName = "Cutscene/Fade", order = 6)]
public class FadeForCutscene : CutsceneEvent
{
    #region VARIABLES

    [SerializeField] bool _fadingOut = true;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Signals to ScreenEffects.cs which direction to fade in
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Starts fade
        if (_fadingOut)
        {
            ScreenEffects.fadingOut = true;
        }
        else
        {
            ScreenEffects.fadingIn = true;
        }

        // Starts wait
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Waits until fade is complete
    /// </summary>
    /// <returns>Delay for fade to complete</returns>
    protected override IEnumerator WaitForEventEnd()
    {
        // Delay, based on fade direction
        if (_fadingOut)
        {
            yield return new WaitUntil(() => ScreenEffects.fadingOut == false);
        }
        else
        {
            yield return new WaitUntil(() => ScreenEffects.fadingIn == false);
        }

        // Signals event completion
        eventComplete = true;
    }

    #endregion
}
