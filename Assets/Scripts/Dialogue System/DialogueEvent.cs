/*=================================================================================================
 * FILE     : DialogueEvent.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 10/12/24
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
}
