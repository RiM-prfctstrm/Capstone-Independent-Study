/*=================================================================================================
 * FILE     : DialogueEvent.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 2/8/25
 * 
 * DESC     : Used to collect and organize Dialogue objects in easily editable formats
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEvent
{
    #region VARIABLES

    // Dialogues
    public List<Dialogue> dialogueBoxes = new List<Dialogue>();

    #endregion

    #region CONSTRUCTORS

    /// <summary>
    /// Constructor for an event with a single line of dialogue
    /// </summary>
    /// <param name="dialogue">dialogue to play</param>
    public DialogueEvent(Dialogue dialogue)
    {
        dialogueBoxes.Add(dialogue);
    }

    #endregion
}
