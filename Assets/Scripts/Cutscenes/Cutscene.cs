/*=================================================================================================
 * FILE     : Cutscene.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1X/X/24
 * UPDATED  : 11/5/24
 * 
 * DESC     : A list of events that occur in a cutscene, with some functionality to direct those
 *            events if they are meant to have more complex behaviour than going one after the
 *            other.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Script", menuName = "Cutscene/Cutscene Script", order = 1)]
public class Cutscene : ScriptableObject
{
    #region VARIABLES

    // Cutscene Events
    public List<CutsceneEvent> cutsceneScript = new List<CutsceneEvent>();

    // Control flags

    #endregion
}
