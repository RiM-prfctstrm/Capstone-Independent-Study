/*=================================================================================================
 * FILE     : Door.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 11/5/24
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

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

    }


    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Either loads a new scene or tells the player the door is locked
    /// </summary>
    public override void OnInteractedWith()
    {
        if (!isLocked)
        {
            // Loads new scene and positions player in it
            SceneTransition.ChangeScene(_targetScene, _leadsIndoors, _position, _direction);
        }
        else
        {
            // Infroms player the door is locked
            DialogueManager.dialogueManager.StartDialogue(_lockedMessage);
        }
    }

    #endregion
}
