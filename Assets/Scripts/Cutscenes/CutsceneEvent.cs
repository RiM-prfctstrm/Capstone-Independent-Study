/*=================================================================================================
 * FILE     : CutsceneEvent.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/5/24
 * UPDATED  : 11/7/24
 * 
 * DESC     : Shell class for different kinds of events that can be performed in cutscenes.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutsceneEvent : ScriptableObject
{
    #region VARIABLES

    // Flag to tell the cutscene manager that an event is complete so it can proceed to the next
    // one. Stays false until the event says not to.
    [SerializeField] protected bool _eventComplete = false;
    public bool eventComplete => _eventComplete;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// The functionality of an individual event. Left empty here.
    /// </summary>
    public virtual void PlayEventFunction()
    {
        // Resets completion signal because apparently it's shared across instances?
        _eventComplete = false;
    }
    
    /// <summary>
    /// Used to delay the completion signal until the event is over when there is no convenient way
    /// to do that in its regular functionality.
    /// </summary>
    protected virtual IEnumerator WaitForEventEnd()
    {
        yield return null;
    }

    #endregion
}
