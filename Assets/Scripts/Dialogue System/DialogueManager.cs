/*=================================================================================================
 * FILE     : DialogueManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 12/8/24
 * 
 * DESC     : Controls which dialogue is currently displayed.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    #region VARIABLES

    // Manager Singleton
    public static DialogueManager dialogueManager;

    // UI Elements
    // Visible elements
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] GameObject _dialogueOutline;
    [SerializeField] TextMeshProUGUI _nametagText;
    [SerializeField] GameObject _nametagOutline;
    [SerializeField] Image _portraitImage;
    [SerializeField] GameObject _portraitOutline;

    // Other Objects
    [SerializeField] AudioSource _systemSounds;
    public Button previouslySelected = null;

    // Progress signals
    static bool _dialogueInProgress = false;
    public static bool dialogueInProgress => _dialogueInProgress;
    public bool advancing = false;
    IEnumerator _dialogRoutine;

    #endregion

    #region DIALOGUE FUNCTIONS

    /// <summary>
    /// Sets up dialogue mode
    /// </summary>
    /// <param name="dialogue">The dialogue that will play</param>
    public void StartDialogue(DialogueEvent dialogue)
    {
        // Sets up dialogue mode
        _dialogueInProgress = true;
        _dialogueOutline.SetActive(true);

        // Clears existing dialogue to help skip unwanted first lines
        _dialogueText.text = "";

        // Activates Dialogue loop
        _dialogRoutine = PlayDialogue(dialogue);
        StartCoroutine(_dialogRoutine);
    }

    /// <summary>
    /// Controls the playback of dialogue
    /// </summary>
    /// <param name="sequence">Sequence of dialogues to play</param>
    IEnumerator PlayDialogue(DialogueEvent sequence)
    {
        // Plays each line of dialogue at correct time
        foreach (Dialogue line in sequence.dialogueBoxes)
        {
            // Plays selection sound
            _systemSounds.Play();

            // Displays the line
            DisplayDialogue(line);

            // Skips a wait if there is an unintended line by default
            if (_dialogueText.text == "")
            {
                continue;
            }

            // Waits for input to continue the loop
            yield return new WaitUntil(() => advancing == true);
            advancing = false;
        }

        // Ends Dialogue
        CancelDialogue();
    }

    /// <summary>
    /// Determines which parts of dialogue UI to display and displays them on screen
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    void DisplayDialogue(Dialogue dialogue)
    {
        // Controls whether and what portrait displays
        if (dialogue.portrait != null)
        {
            _portraitOutline.SetActive(true);
            _portraitImage.sprite = dialogue.portrait;
        }
        else
        {
            _portraitOutline.SetActive(false);
        }

        // Controls whether and what name tag displays
        if (dialogue.nameTag != null && dialogue.nameTag != "")
        {
            _nametagOutline.SetActive(true);
            _nametagText.text = dialogue.nameTag;
        }
        else
        {
            _nametagOutline.SetActive(false);
        }

        // Displays Dialogue
        _dialogueText.text = dialogue.dialogueText;
    }

    /// <summary>
    /// Cancels currently playing dialogue
    /// </summary>
    public void CancelDialogue()
    {
        // Lets other scripts know player is out of dialogue
        _dialogueInProgress = false;
        NPCInteraction.inNPCInteraction = false;

        // Ends Active Dialogue sequence
        StopCoroutine(_dialogRoutine);

        // Deactivates UI
        _dialogueOutline.SetActive(false);
        _nametagOutline.SetActive(false);
        _portraitOutline.SetActive(false);

        // Reselects the previously selected button if applicable
        if (previouslySelected != null)
        {
            previouslySelected.Select();
            previouslySelected = null;
        }
    }

    #endregion
}
