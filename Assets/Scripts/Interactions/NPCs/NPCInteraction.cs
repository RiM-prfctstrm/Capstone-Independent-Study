/*=================================================================================================
 * FILE     : NPCInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 10/12/24
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
    [SerializeField] bool _hasDailyUpdates = false;
    [SerializeField] bool _isMobile = false;
    [SerializeField] float _dialogueRange = 5;

    // Dialogue
    [SerializeField] List<DialogueEvent> _NPCLines = new List<DialogueEvent>();

    // External components
    [SerializeField] GameObject _player;
    DialogueManager _dialogueManager;

    // Misc
    int _dialogueCycle = 0;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initialize Vars
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _player = FindObjectOfType<PlayerController>().gameObject;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Determines whether player is close enough for dialogue
        if (Vector2.Distance(transform.position, _player.transform.position) > _dialogueRange
            && _player.GetComponent<PlayerController>().inDialogue)
        {
            _dialogueManager.CancelDialogue();
        }
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
                NPCDialogue();
                break;

            case 1:
                break;
        }
    }

    /// <summary>
    /// Controls which Dialogue Event to play
    /// </summary>
    void NPCDialogue()
    {
        // Plays Dialogue
        _dialogueManager.PlayDialogue(_NPCLines[_dialogueCycle].dialogueBoxes);

        // Updates currently playing event
        _dialogueCycle++;
        if (_dialogueCycle >= _NPCLines.Count)
        {
            _dialogueCycle = 0;
        }
    }

    #endregion
}
