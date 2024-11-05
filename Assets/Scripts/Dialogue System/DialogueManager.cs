/*=================================================================================================
 * FILE     : DialogueManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 11/5/24
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
    // Invisible Button to advance dialogue
    [SerializeField] Button _advanceButton;

    // Other Objects
    public Button previouslySelected = null;

    // Progress signals
    public static bool dialogueInProgress = false;
    bool _advancing = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _advanceButton.onClick.AddListener(() => _advancing = true);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    #endregion

    #region DIALOGUE FUNCTIONS

    /// <summary>
    /// Sets up dialogue mode
    /// </summary>
    /// <param name="dialogue">The dialogue that will play</param>
    public void StartDialogue(DialogueEvent dialogue)
    {
        StartCoroutine(PlayDialogue(dialogue));
    }

    /// <summary>
    /// Controls the playback of dialogue
    /// </summary>
    /// <param name="sequence">Sequence of dialogues to play</param>
    public IEnumerator PlayDialogue(DialogueEvent sequence)
    {
        Debug.Log("sus");
        // Sets up dialogue mode
        dialogueInProgress = true;
        _dialogueOutline.SetActive(true);

        // Sets up buttons for dialogue
        _advanceButton.Select();
        _advanceButton.interactable = true;

        // Delays loop so that first line isn't cut off by activating input
        if (previouslySelected == null)
        {
            yield return new WaitUntil(() => _advancing == true);
            _advancing = false;
        }

        // Plays each line of dialogue at correct time
        foreach (Dialogue line in sequence.dialogueBoxes)
        {
            DisplayDialogue(line);
            yield return new WaitUntil(() => _advancing == true);
            _advancing = false;
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
        dialogueInProgress = false;
        _advanceButton.interactable = false;
        //_player.ToggleDialogueInputs();

        // Ends Active Dialogue sequence
        StopAllCoroutines();

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
