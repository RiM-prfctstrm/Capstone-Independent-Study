/*=================================================================================================
 * FILE     : TickTokenButtonAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/21/25
 * UPDATED  : 5/26/25
 * 
 * DESC     : Makes analog buttons on Tick Token respond to player input
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TickTokenButtonAnimator : MonoBehaviour
{
    #region VARIABLES

    // Input controls
    [SerializeField] InputActionAsset _inputs;
    InputAction _cancel;
    InputAction _confirm;
    InputAction _negX;
    InputAction _negY;
    InputAction _posX;
    InputAction _posY;

    // UI Elements
    [SerializeField] Image _backButton;
    [SerializeField] Image _dPad;
    [SerializeField] GameObject[] _dPadPressButtons;
    [SerializeField] Image _powerButton;

    // Sprites
    [SerializeField] Sprite[] _buttonSprites;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Inits inputs
        _cancel = _inputs.FindAction("Cancel");
        _confirm = _inputs.FindAction("Submit");
        _negX = _inputs.FindAction("NegX");
        _negY = _inputs.FindAction("NegY");
        _posX = _inputs.FindAction("PosX");
        _posY = _inputs.FindAction("PosY");

        // Adds function to unpressing buttons
        _cancel.canceled += ReleaseCancel;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Checks which inputs have been pressed this frame
        CheckForNewInputs();
        CheckForInputEnds();
    }

    #endregion

    #region SPRITE CONTROLS

    /// <summary>
    /// Checks when a new input is pressed
    /// </summary>
    void CheckForNewInputs()
    {
        // Dpad navigation
        if (_negX.triggered)
        {
            _dPadPressButtons[1].SetActive(true);
        }
        if (_posX.triggered)
        {
            _dPadPressButtons[2].SetActive(true);
        }
        if (_negY.triggered)
        {
            _dPadPressButtons[0].SetActive(true);
        }
        if (_posY.triggered)
        {
            _dPadPressButtons[3].SetActive(true);
        }
        if (_confirm.triggered)
        {
            _dPadPressButtons[4].SetActive(true);
        }

        // Cancel Button
        if (_cancel.triggered)
        {
            _backButton.sprite = _buttonSprites[1];
        }
    }

    /// <summary>
    /// Checks when an input ends.
    /// </summary>
    void CheckForInputEnds()
    {
        // Dpad navigation
        if (_negX.WasReleasedThisFrame())
        {
            _dPadPressButtons[1].SetActive(false);
        }
        if (_posX.WasReleasedThisFrame())
        {
            _dPadPressButtons[2].SetActive(false);
        }
        if (_negY.WasReleasedThisFrame())
        {
            _dPadPressButtons[0].SetActive(false);
        }
        if (_posY.WasReleasedThisFrame())
        {
            _dPadPressButtons[3].SetActive(false);
        }
        if (_confirm.WasReleasedThisFrame())
        {
            _dPadPressButtons[4].SetActive(false);
        }
    }

    /// <summary>
    /// Sets the cancel button to its unpressed sprite
    /// </summary>
    void ReleaseCancel(InputAction.CallbackContext ctx)
    {
        _backButton.sprite = _buttonSprites[0];
    }

    #endregion
}
