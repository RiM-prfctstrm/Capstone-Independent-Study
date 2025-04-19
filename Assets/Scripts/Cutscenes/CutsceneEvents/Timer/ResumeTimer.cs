/*=================================================================================================
 * FILE     : ResumeTimer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 4/19/25
 * 
 * DESC     : Resumes in-game timer at the end of an event
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Timer Resume", menuName = "Cutscene/Timer/Resume", order = 1)]
public class ResumeTimer : CutsceneEvent
{
    // Resumes the Timer
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        TimerController.timerController.ResumeTimer();
        eventComplete = true;
    }
}
