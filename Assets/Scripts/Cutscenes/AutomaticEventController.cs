/*=================================================================================================
 * FILE     : AutomaticEventController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/4/24
 * UPDATED  : 11/4/24
 * 
 * DESC     : Decides whether cutscenes that start automatically when a scene is loaded should
 *            play, and if multiple such scenes exist, which one to activate.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticEventController : MonoBehaviour
{
    #region VARIABLES

    // Possible Cutscenes
    [SerializeField] List<Cutscene> _autoCutscenes = new List<Cutscene>();

    // Selection controls
    [SerializeField] bool _cyclesPerMission = true;
    public int readyCutscene;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // For mission based selection, chooses which cutscene to try
        if (_cyclesPerMission)
        {
            readyCutscene = GlobalVariableTracker.currentMission;
        }

        // Plays a cutscene if it hasn't already played
        if (!_autoCutscenes[readyCutscene].hasPlayed)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_autoCutscenes[readyCutscene]);
        }
    }

    #endregion
}
