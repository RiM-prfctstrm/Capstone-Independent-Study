/*=================================================================================================
 * FILE     : NPCInteraction.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/11/24
 * UPDATED  : 10/31/24
 * 
 * DESC     : Performs unique events depending
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
    [SerializeField] int _type; // 0=dialogue, 1=event trigger
    //[SerializeField] bool _hasDailyUpdates = false;
    //[SerializeField] bool _isMobile = false;
    [SerializeField] float _dialogueRange = 5;
    [SerializeField] bool _staticImage = false;

    // Dialogue
    [SerializeField] List<DialogueEvent> _NPCLines = new List<DialogueEvent>();

    // External components
    GameObject _player;
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
            && _player.GetComponent<PlayerController>().lastTarget == gameObject
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
        // Faces Player
        if (!_staticImage)
        {
            _animator.FacePlayer();
        }

        // Plays Dialogue
        StartCoroutine(_dialogueManager.PlayDialogue(_NPCLines[_dialogueCycle]));

        // Updates currently playing event
        _dialogueCycle++;
        if (_dialogueCycle >= _NPCLines.Count)
        {
            _dialogueCycle = 0;
        }
    }

    #endregion
}
