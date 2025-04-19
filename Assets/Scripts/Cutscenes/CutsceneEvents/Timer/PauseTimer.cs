/*=================================================================================================
 * FILE     : PauseTimer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 4/19/25
 * UPDATED  : 4/19/25
 * 
 * DESC     : Pauses in-game timer so player doesn't have to worry about it during events
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Timer Pause", menuName = "Cutscene/Timer/Pause", order = 0)]
public class PauseTimer : CutsceneEvent
{
    // Pauses the Timer
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        TimerController.timerController.PauseTimer();
        eventComplete = true;
    }
}
