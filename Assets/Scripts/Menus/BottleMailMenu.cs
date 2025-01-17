/*=================================================================================================
 * FILE     : BottleMailMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/16/25
 * UPDATED  : 1/16/25
 * 
 * DESC     : Controls BottleMail menu behavior to emulate an email program.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottleMailMenu : MonoBehaviour
{
    #region VARIABLES

    // Objects
    [SerializeField] Button _defaultSelection;
    // Message info container, ordered by vertical placement on screen
    [SerializeField] TextMeshProUGUI _subject;
    [SerializeField] TextMeshProUGUI _sender;
    [SerializeField] TextMeshProUGUI _receiver;
    [SerializeField] TextMeshProUGUI _message;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Disables regular player input
        PlayerController.playerController.TogglePlayerInput();

        // Ensures button is selected
        _defaultSelection.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Opens a BottleMail message and enables scrolling
    /// </summary>
    /// <param name="msg">Container for data that makes up desired message</param>
    public void OpenMessage(BottleMailMessage msg)
    {
        // Sets Message texts
        _subject.text = msg.msgSubject;
        _sender.text = msg.msgSender;
        _receiver.text = msg.msgReceivers;
        _message.text = msg.msgContents;
    }

    #endregion
}
