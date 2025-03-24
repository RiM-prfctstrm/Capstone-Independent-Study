/*=================================================================================================
 * FILE     : TitleMenuMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/15/24
 * UPDATED  : 3/24/25
 * 
 * DESC     : Rolls the credits and contains a function to cancel them on the title menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TitleMenuCredits : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    [SerializeField] Button _titleMenuButton;
    [SerializeField] TitleMenu _titleMenu;
    [SerializeField] RectTransform _rectTransform;

    // Parameters
    [SerializeField] Vector2 _resetPos;
    [SerializeField] int _speed;
    int _trueSpeed;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// This function is called when the object becomes enabled and active
    /// </summary>
    void OnEnable()
    {
        _trueSpeed = _speed * GlobalVariableTracker.windowScale;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Raises the credits text
        if (_rectTransform.position.y < 0)
        {
            _rectTransform.Translate(Vector2.up * _trueSpeed * Time.fixedDeltaTime *
                3);
        }
    }

    #endregion

    #region DEACTIVATION BUTTON

    /// <summary>
    /// Cancels the credits and resets position
    /// </summary>
    public void StopCredits()
    {
        // Returns to menu
        transform.position = _resetPos;
        _titleMenuButton.Select();
        transform.parent.gameObject.SetActive(false);

        // Disables cancel function
        _titleMenu.cancel.performed -= StopCredits;
        _titleMenu.cancel.Disable();
    }
    public void StopCredits(InputAction.CallbackContext ctx)
    {
        // Returns to menu
        transform.position = _resetPos;
        _titleMenuButton.Select();
        transform.parent.gameObject.SetActive(false);

        // Disables cancel function
        _titleMenu.cancel.performed -= StopCredits;
        _titleMenu.cancel.Disable();
    }

    #endregion
}
