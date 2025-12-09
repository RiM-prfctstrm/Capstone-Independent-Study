/*=================================================================================================
 * FILE     : SavePoint.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/9/25
 * UPDATED  : 12/9/25
 * 
 * DESC     : Writes a save when the player interacts.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : InteractableObject
{
    #region VARIABLES

    // Save Variables
    Vector3 _savePosition;

    // Messages
    [SerializeField] DialogueEvent _midDeliMsg;
    [SerializeField] Cutscene _saveConfirm;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets point to return player on load
        _savePosition = transform.position;
        _savePosition.y += -1;
    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Determines whether to save or notify player that that isn't currently possible.
    /// </summary>
    public override void OnInteractedWith()
    {
        // Determines whether to save or note that saving is not possible
        if (GlobalVariableTracker.progressionFlags["inDelivery"])
        {
            // Tells player they can't save mid delivery
            PlayerController.playerController.TogglePlayerInput();
            DialogueManager.dialogueManager.StartDialogue(_midDeliMsg);
        }
        // Saves the game
        else
        {
            SaveLoadFunctions.SaveFile(1, _savePosition);
            CutsceneManager.cutsceneManager.StartCutscene(_saveConfirm);
        }
    }

    #endregion
}
