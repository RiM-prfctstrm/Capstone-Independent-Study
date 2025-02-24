/*=================================================================================================
 * FILE     : Door.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 2/23/25
 * 
 * DESC     : Basically a fancier version of SceneTrigger that activates when the player interacts
 *            with it, plays different animations and sounds, and can be locked until the player
 *            meets a certain condition.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    #region VARIABLES

    // Scene information
    [SerializeField] string _targetScene;
    [SerializeField] bool _leadsIndoors = true;
    [SerializeField] Vector3 _position;
    [SerializeField] int _direction = 3;

    // Locking info
    public bool isLocked = false;
    [SerializeField] DialogueEvent _lockedMessage;

    #endregion

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Determines whether player is close enough for dialogue
        if (Vector2.Distance(transform.position, 
            PlayerController.playerController.transform.position) > 5
            && PlayerController.playerController.lastTarget == gameObject
            && DialogueManager.dialogueInProgress)
        {
            DialogueManager.dialogueManager.CancelDialogue();
        }
    }

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Either loads a new scene or tells the player the door is locked
    /// </summary>
    public override void OnInteractedWith()
    {
        if (!isLocked)
        {
            // Loads new scene and positions player in it
            StartCoroutine(SceneTransition.TransitionScene(
                _targetScene, _leadsIndoors, _position, _direction));
        }
        else
        {
            // Infroms player the door is locked
            DialogueManager.dialogueManager.StartDialogue(_lockedMessage);
        }
    }

    #endregion
}
