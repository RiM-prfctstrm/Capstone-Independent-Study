/*=================================================================================================
 * FILE     : SaveEvent.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/3/25
 * UPDATED  : 12/3/25
 * 
 * DESC     : Writes a save for an event
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Cutscene/Save", order = 8)]
public class SaveEvent : CutsceneEvent
{
    /// <summary>
    /// Writes a save
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Performs Save
        SaveLoadFunctions.SaveFile(1);

        // Signals event completion
        eventComplete = true;
    }
}
