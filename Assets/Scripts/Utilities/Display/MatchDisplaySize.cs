/*=================================================================================================
 * FILE     : MatchDisplaySize.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/28/25
 * UPDATED  : 10/28/25
 * 
 * DESC     : Matches rect transform size to that of display.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchDisplaySize : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta =
            new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);
    }
}
