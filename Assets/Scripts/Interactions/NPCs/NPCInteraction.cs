/*=================================================================================================
 * FILE     : NPCInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 2/3/25
 * 
 * DESC     : Controls how NPCs behave when the player interacts with them.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : InteractableObject
{
    #region VARIABLES

    // Components
    NPCAnimator _animator;

    // Controls
    //[SerializeField] bool _hasDailyUpdates = false;
    //[SerializeField] bool _isMobile = false;
    [SerializeField] float _dialogueRange = 5;
    [SerializeField] bool _isEventTrigger = false;
    [SerializeField] bool _staticImage = false;

    // Interaction results
    [SerializeField] protected List<DialogueEvent> _NPCLines = new List<DialogueEvent>();
    [SerializeField] protected Cutscene _NPCCutscene;

    // External components
    GameObject _player;
    DialogueManager _dialogueManager;

    // Misc
    int _dialogueCycle = 0;
    public static bool inNPCInteraction = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initialize Vars
        _dialogueManager = DialogueManager.dialogueManager;
        _player = PlayerController.playerController.gameObject;
        if (!_staticImage)
        {
            _animator = GetComponent<NPCAnimator>();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Determines whether player is close enough for dialogue
        if (Vector2.Distance(transform.position, _player.transform.position) > _dialogueRange
            && PlayerController.playerController.lastTarget == gameObject
            && DialogueManager.dialogueInProgress)
        {
            _dialogueManager.CancelDialogue();
            _dialogueCycle--;

            // Stops Negative Overflow
            if (_dialogueCycle < 0)
            {
                _dialogueCycle = 0;
            }
        }
    }

    #endregion

    #region INTERACTION FUNCIONALITY

    /// <summary>
    /// Determines what to do when the player interacts with the NPC
    /// </summary>
    public override void OnInteractedWith()
    {
        // Tells game an interaction is happening
        inNPCInteraction = true;

        // Faces Player
        if (!_staticImage)
        {
            _animator.FacePlayer();
        }

        // Interaction type logic
        if (!_isEventTrigger)
        {
             NPCDialogue();
        }
        else
        {
            NPCTriggeredEvent();
        }
    }

    /// <summary>
    /// Controls which Dialogue Event to play
    /// </summary>
    void NPCDialogue()
    {
        // Last line of defence to stop regular dialogue from playing in cutscenes. Ideally, this
        // will never be true
        if (CutsceneManager.inCutscene)
        {
            return;
        }

        // Stops Positive Overflow
        if (_dialogueCycle >= _NPCLines.Count)
        {
            _dialogueCycle = 0;
        }

        // Plays Dialogue
        _dialogueManager.StartDialogue(_NPCLines[_dialogueCycle]);

        // Updates currently playing event
        _dialogueCycle++;
    }

    /// <summary>
    /// Controls cutscenes triggered by talking to the NPC
    /// </summary>
    void NPCTriggeredEvent()
    {
        if (!_NPCCutscene.hasPlayed)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_NPCCutscene);
            // Temporary trigger to set to standard diaogue. Will be replaced when a system for
            // saving and loading which cutscenes have been played has been built.
            _isEventTrigger = false;
        }
        else
        {
            NPCDialogue();
        }
    }

    #endregion
}
