/*=================================================================================================
 * FILE     : DisplayImage.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/6/24
 * UPDATED  : 11/6/24
 * 
 * DESC     : Displays an image at a specified point on the screen. Used in cutscenes.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Display Image", menuName = "Cutscene/Image Management/Display Image",
                 order = 1)]
public class DisplayImage : CutsceneEvent
{
    #region VARIABLES

    // Inputs
    [SerializeField] Sprite _image;
    [SerializeField] Vector2 _coords; // The position of the bottom left corner of the image

    // Objects used to render image in scene
    GameObject _ImageSpace;
    GameObject _renderObject;
    RectTransform _imageTransform;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Finds an available image slot and displays the input specified image in it.
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Searches for available slot
        _ImageSpace = CutsceneManager.cutsceneManager.UISpace.transform.GetChild(0).gameObject;
        for (int i = 0; i <= _ImageSpace.transform.childCount; i++)
        {
            // Skips slots currently in use
            if (_ImageSpace.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                continue;
            }

            // Sets which object the image displays in
            _renderObject = _ImageSpace.transform.GetChild(i).gameObject;
            _imageTransform = _renderObject.GetComponent < RectTransform>();
            break;
        }

        // Sets image, position, and size
        _renderObject.GetComponent<Image>().sprite = _image;
        _imageTransform.anchoredPosition = _coords;
        _imageTransform.sizeDelta = new Vector2(_image.rect.width, _image.rect.height);

        // Activates image and finishes event
        _renderObject.SetActive(true);
        _eventComplete = true;
    }

    #endregion
}
