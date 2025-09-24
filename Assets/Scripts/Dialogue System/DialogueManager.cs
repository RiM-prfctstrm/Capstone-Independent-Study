/*=================================================================================================
 * FILE     : DialogueManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/12/24
 * UPDATED  : 9/24/25
 * 
 * DESC     : Controls which dialogue is currently displayed.
=================================================================================================*/
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region VARIABLES

    // Manager Singleton
    public static DialogueManager dialogueManager;

    // UI Elements
    // Visible elements
    [SerializeField] GameObject _choiceMenu;
    [SerializeField] GameObject _choiceNo;
    [SerializeField] GameObject _choiceYes;
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] GameObject _dialogueOutline;
    [SerializeField] TextMeshProUGUI _nametagText;
    [SerializeField] GameObject _nametagOutline;
    [SerializeField] Image _portraitImage;
    [SerializeField] GameObject _portraitOutline;

    // Other Objects
    [SerializeField] AudioMixer _mixer;
    [SerializeField] AudioSource _systemSounds;
    public Button previouslySelected = null;

    // Getters
    public GameObject choiceMenu => _choiceMenu;
    public GameObject choiceNo => _choiceNo;
    public GameObject choiceYes => _choiceYes;
    public GameObject dialogueOutline => _dialogueOutline;

    // Progress signals
    static bool _dialogueInProgress = false;
    public static bool dialogueInProgress => _dialogueInProgress;
    public bool advancing = false;
    bool _inScroll = false;
    public IEnumerator dialogRoutine;
    IEnumerator _scrollRoutine;

    // SFX
    [SerializeField] AudioClip _advanceSound;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initializes volume
        SetMenuEffectsVolume();
    }

    #endregion

    #region DIALOGUE FUNCTIONS

    /// <summary>
    /// Sets up dialogue mode
    /// </summary>
    /// <param name="dialogue">The dialogue that will play</param>
    public void StartDialogue(DialogueEvent dialogue)
    {
        // Sets up dialogue mode
        advancing = false;
        _dialogueInProgress = true;
        _dialogueOutline.SetActive(true);

        // Enables cancelling outside cutscenes
        if (!CutsceneManager.inCutscene)
        {
            PlayerController.playerController.cancel.Enable();
            //PlayerController.playerController.cancel.performed += CancelDialogue;
        }

        // Clears existing dialogue to help skip unwanted first lines
        _dialogueText.text = "";

        // Activates Dialogue loop
        dialogRoutine = PlayDialogue(dialogue);
        StartCoroutine(dialogRoutine);
    }

    /// <summary>
    /// Controls the playback of dialogue
    /// </summary>
    /// <param name="sequence">Sequence of dialogues to play</param>
    public IEnumerator PlayDialogue(DialogueEvent sequence)
    {
        // Plays each line of dialogue at correct time
        for (int i = 0; i < sequence.dialogueBoxes.Count;)
        {
            // Vars
            Dialogue line = sequence.dialogueBoxes[i];

            // Plays selection sound
            _systemSounds.PlayOneShot(_advanceSound);

            // Displays new dialogue
            if (!_inScroll)
            {
                // Displays the line
                DisplayDialogue(line);
                i++;
            }
            // Finishes text scroll
            else
            {
                _dialogueText.maxVisibleCharacters = _dialogueText.text.Length;
                StopCoroutine(_scrollRoutine);
                _inScroll = false;

                // Keeps final line from infinitely looping
                if (i == sequence.dialogueBoxes.Count - 1)
                {
                    i++;
                }
            }

            // Skips a wait if there is an unintended line by default
            if (_dialogueText.text == "")
            {
                continue;
            }

            // Waits for input to continue the loop
            yield return new WaitUntil(() => advancing == true);
            advancing = false;

            // Makes sure skipping final scroll doesnt skip final text
            if (i == sequence.dialogueBoxes.Count && _inScroll)
            {
                i--;
            }
        }

        // Ends Dialogue
        CancelDialogue();
    }

    /// <summary>
    /// Determines which parts of dialogue UI to display and displays them on screen
    /// </summary>
    /// <param name="dialogue">The dialogue to display</param>
    public void DisplayDialogue(Dialogue dialogue)
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
        _dialogueText.maxVisibleCharacters = 0;
        _dialogueText.text = dialogue.dialogueText;
        _scrollRoutine = ScrollHorizontal();
        StartCoroutine(_scrollRoutine);
    }

    /// <summary>
    /// Makes text appear one character at a time
    /// </summary>
    /// <returns>Delay between character appearance</returns>
    IEnumerator ScrollHorizontal()
    {
        _inScroll = true;

        while(_dialogueText.maxVisibleCharacters < _dialogueText.text.Length)
        {
            _dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(.025f);
        }

        _inScroll = false;
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
        StopCoroutine(dialogRoutine);

        // Prevents auto-advancing through next text
        advancing = false;

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

        // Disables cancelling functionality
        if (!CutsceneManager.inCutscene)
        {
            PlayerController.playerController.cancel.Disable();
            //PlayerController.playerController.cancel.performed -= CancelDialogue;
            PlayerController.playerController.TogglePlayerInput();
        }

        // Deselects any targeted gameobjects
        PlayerController.playerController.lastTarget = null;
    }
    public void CancelDialogue(InputAction.CallbackContext ctx)
    {
        // Lets other scripts know player is out of dialogue
        _dialogueInProgress = false;
        NPCInteraction.inNPCInteraction = false;

        // Ends Active Dialogue sequence
        StopCoroutine(dialogRoutine);

        // Prevents auto-advancing through next text
        advancing = false;

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

        // Disables cancelling functionality
        if (!CutsceneManager.inCutscene)
        {
            //PlayerController.playerController.cancel.Disable();
            //PlayerController.playerController.cancel.performed -= CancelDialogue;
        }

        // Deselects any targeted gameobjects
        PlayerController.playerController.lastTarget = null;
    }

    #endregion

    #region MISC

    /// <summary>
    /// Changes volume of menu sound effects. I will probably have to rework this once I add other
    /// sounds.
    /// </summary>
    public void SetMenuEffectsVolume()
    {
        _mixer.SetFloat("menVol", Mathf.Log10(GlobalVariableTracker.menuVolume) * 20);
    }

    #endregion

}
