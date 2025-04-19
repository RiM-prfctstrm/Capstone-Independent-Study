/*=================================================================================================
 * FILE     : StopTimer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 4/19/25
 * 
 * DESC     : Stops in-game timer completely. For use when ending a mission
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Timer Stop", menuName = "Cutscene/Timer/Stop", order = 2)]
public class StopTimer : CutsceneEvent
{
    // Resumes the Timer
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        TimerController.timerController.StopTimer();
        eventComplete = true;
    }
}
