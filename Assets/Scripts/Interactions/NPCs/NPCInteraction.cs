/*=================================================================================================
 * FILE     : NPCInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 10/11/24
 * 
 * DESC     : Performs unique events depending
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    #region VARIABLES

    // Controls
    [SerializeField] int _type; // 0=dialogue, 1=event trigger

    // Dialogue
    [SerializeField] [TextArea] string[] _dialogue;
    public string[] dialogue => _dialogue;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
     void Update()
    {

    }

    #endregion

    #region INTERACTION FUNCIONALITY

    /// <summary>
    /// Determines what to do when the player interacts with the NPC
    /// </summary>
    public override void OnInteractedWith()
    {
        switch (_type)
        {
            case 0:
                break;

            case 1:
                break;
        }
    }

    /// <summary>
    /// Display's the NPC's dialogue
    /// </summary>
    void NPCDialogue()
    {

    }

    #endregion
}
