/*=================================================================================================
 * FILE     : ClearImage.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/6/24
 * UPDATED  : 11/6/24
 * 
 * DESC     : Removes an image from the screen.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clear Image", menuName = "Cutscene/Image Management/Clear Image",
                 order = 2)]
public class ClearImage : CutsceneEvent
{
    #region VARIABLES

    // The image slot to disable
    [SerializeField] int _clearSlot;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Disables the designated image slot
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        CutsceneManager.cutsceneManager.UISpace.transform.GetChild(0).GetChild(_clearSlot).
            gameObject.SetActive(false);
        _eventComplete = true;
    }

    #endregion
}
