/*=================================================================================================
 * FILE     : Dialogue.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 2/8/24
 * 
 * DESC     : Container for data used for in-game dialogue;
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    #region VARIABLES

    // Dialogue Data
    [TextArea] public string dialogueText = null;
    public string nameTag = null;
    public Sprite portrait = null;

    #endregion

    #region CONSTRUCTORS

    /// <summary>
    /// Dialogue constructor that only gives text.
    /// </summary>
    /// <param name="text">Text to display</param>
    public Dialogue(string text)
    {
        dialogueText = text;
    }

    #endregion
}
