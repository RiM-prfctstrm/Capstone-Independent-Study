/*=================================================================================================
 * FILE     : TickTokenButtonAnimator.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/21/25
 * UPDATED  : 11/21/25
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
    InputAction _selectX;
    InputAction _selectY;

    // UI Elements
    [SerializeField] Image _backButton;
    [SerializeField] Image _dPad;
    [SerializeField] Image _powerButton;

    // Sprites
    [SerializeField] Sprite[] _buttonSprites;
    [SerializeField] Sprite[] _dPadSprites;

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
        _selectX = _inputs.FindAction("MoveX");
        _selectY = _inputs.FindAction("MoveY");

        // Adds function to unpressing buttons
        _cancel.canceled += ReleaseCancel;
        _confirm.canceled += ReleaseDPad;
        _selectX.canceled += ReleaseDPad;
        _selectY.canceled += ReleaseDPad;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Checks which inputs have been pressed this frame
        CheckForNewInputs();
    }

    #endregion

    #region SPRITE CONTROLS

    /// <summary>
    /// Checks when a new input is pressed
    /// </summary>
    void CheckForNewInputs()
    {
        // Dpad navigation
        if (_selectX.triggered)
        {
            if (_selectX.ReadValue<float>() > 0)
            {
                _dPad.sprite = _dPadSprites[2];
            }
            else if (_selectX.ReadValue<float>() < 0)
            {
                _dPad.sprite = _dPadSprites[1];
            }
        }
        if (_selectY.triggered)
        {
            if (_selectY.ReadValue<float>() > 0)
            {
                _dPad.sprite = _dPadSprites[3];
            }
            else if (_selectY.ReadValue<float>() < 0)
            {
                _dPad.sprite = _dPadSprites[0];
            }
        }
        if (_confirm.triggered)
        {
            _dPad.sprite = _dPadSprites[5];
        }

        // Cancel Button
        if (_cancel.triggered)
        {
            _backButton.sprite = _buttonSprites[1];
        }
    }


    /// <summary>
    /// Sets the dpad to its unpressed image
    /// </summary>
    void ReleaseDPad(InputAction.CallbackContext ctx)
    {
        _dPad.sprite = _dPadSprites[4];
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
