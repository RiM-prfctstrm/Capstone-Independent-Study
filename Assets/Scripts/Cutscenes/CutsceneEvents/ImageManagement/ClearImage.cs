/*=================================================================================================
 * FILE     : ClearImage.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/6/24
 * UPDATED  : 5/21/26
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
    int _imageOrder;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Disables the designated image slot
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Finds imagelocation
        if (ScreenEffects.isScreenBlack)
        {
            _imageOrder = 2;
        }
        else
        {
            _imageOrder = 1;
        }

        // Clears image
        CutsceneManager.cutsceneManager.UISpace.transform.GetChild(_imageOrder).GetChild(_clearSlot).
            gameObject.SetActive(false);
        eventComplete = true;
    }

    #endregion
}
