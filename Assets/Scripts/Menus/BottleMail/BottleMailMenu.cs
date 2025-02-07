/*=================================================================================================
 * FILE     : BottleMailMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/16/25
 * UPDATED  : 2/7/25
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
    [SerializeField] Scrollbar _scrollBar;
    // Message info container, ordered by vertical placement on screen
    [SerializeField] TextMeshProUGUI _subject;
    [SerializeField] TextMeshProUGUI _sender;
    [SerializeField] TextMeshProUGUI _receiver;
    [SerializeField] GameObject _messageText;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Disables regular player input
        PlayerController.playerController.TogglePlayerInput();

        // Sets default menu values
        _defaultSelection.Select();
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
        _messageText.GetComponent<TextMeshProUGUI>().text = msg.msgContents;

        // Sets Scrollbar to proper size
        _scrollBar.size = _messageText.GetComponent<RectTransform>().rect.height / 330;
        if (_scrollBar.size > 1)
        {
            _scrollBar.size = 1;
        }

        // Auto selects scrollbar
        _scrollBar.Select();
    }

    /// <summary>
    /// Exits the BottleMail Menu and return's to Pepper's room
    /// </summary>
    public void CloseMenu()
    {
        // Reenables player input
        PlayerController.playerController.TogglePlayerInput();

        // Sends player back to room
        StartCoroutine(SceneTransition.TransitionScene(
            "ShakerHouse", true, new Vector3(5.5f, -37.25f, 0), 3));
    }

    #endregion
}
