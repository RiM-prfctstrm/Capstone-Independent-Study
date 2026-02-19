/*=================================================================================================
 * FILE     : BottleMailMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/16/25
 * UPDATED  : 2/19/25
 * 
 * DESC     : Controls BottleMail menu behavior to emulate an email program.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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
        // Scales menu properly
        GetComponent<Canvas>().worldCamera =
            PlayerController.playerController.GetComponentInChildren<Camera>();

        // Sets Player input capabilitys
        PlayerController.playerController.TogglePlayerInput();
        PlayerController.playerController.openMenu.Disable();
        PlayerController.playerController.cancel.Enable();
        PlayerController.playerController.cancel.performed += CloseMenu;

        // Sets default menu values
        _defaultSelection.Select();

        // Sets Menu Volune
        GetComponent<AudioSource>().volume = GlobalVariableTracker.menuVolume;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Ensures cancelling is enabled
        {
            if (!PlayerController.playerController.cancel.enabled)
            {
                PlayerController.playerController.cancel.Enable();
            }
        }
    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Sets which button the scrollbar navigates left to
    /// </summary>
    public void SetReturnButton(Button currentMsgBtn)
    {
        // Vaars
        Navigation scrollNav = _scrollBar.navigation;

        // Setter
        scrollNav.selectOnLeft = currentMsgBtn;
        _scrollBar.navigation = scrollNav;
    }

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

        // Sets Scrollbar size and return button
        _scrollBar.size = _messageText.GetComponent<RectTransform>().rect.height / 330;
        if (_scrollBar.size > 1)
        {
            _scrollBar.size = 1;
        }

        // Auto selects scrollbar
        _scrollBar.interactable = true;
        _scrollBar.Select();

        // Sets cancel action to return to message select
        PlayerController.playerController.cancel.performed -= CloseMenu;
        PlayerController.playerController.cancel.performed += ReturnToMsgSelect;
    }

    /// <summary>
    /// Exits the BottleMail Menu and return's to Pepper's room
    /// </summary>
    public void CloseMenu()
    {
        // Prevents closing while loading is not complete
        if (ScreenEffects.fadingIn)
        {
            return;
        }

        // Reenables player input
        PlayerController.playerController.TogglePlayerInput();
        PlayerController.playerController.cancel.performed -= CloseMenu;

        // Sends player back to room
        StartCoroutine(SceneTransition.TransitionScene(
            "ShakerHouse", true, new Vector3(5.5f, -37.25f, 0), 3));
    }
    public void CloseMenu(InputAction.CallbackContext ctx)
    {
        // Prevents closing while loading is not complete
        if (ScreenEffects.fadingIn)
        {
            return;
        }

        // Reenables player input
        PlayerController.playerController.TogglePlayerInput();
        PlayerController.playerController.cancel.performed -= CloseMenu;

        // Sends player back to room
        StartCoroutine(SceneTransition.TransitionScene(
            "ShakerHouse", true, new Vector3(5.5f, -37.25f, 0), 3));
    }

    #endregion

    #region CANCEL FUNCTIONS

    /// <summary>
    /// Returns to selected message in the message menu
    /// </summary>
    public void ReturnToMsgSelect()
    {
        // Resets cancel function
        PlayerController.playerController.cancel.performed -= ReturnToMsgSelect;
        PlayerController.playerController.cancel.performed += CloseMenu;
    }
    public void ReturnToMsgSelect(InputAction.CallbackContext ctx)
    {
        // Sets button
        _scrollBar.navigation.selectOnLeft.Select();

        // Resets cancel function
        PlayerController.playerController.cancel.performed -= ReturnToMsgSelect;
        PlayerController.playerController.cancel.performed += CloseMenu;
    }

    #endregion
}
