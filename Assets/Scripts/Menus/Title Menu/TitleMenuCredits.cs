/*=================================================================================================
 * FILE     : TitleMenuMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/15/24
 * UPDATED  : 2/17/25
 * 
 * DESC     : Rolls the credits and contains a function to cancel them on the title menu.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuCredits : MonoBehaviour
{
    #region VARIABLES

    // Object Refs
    [SerializeField] Button _titleMenuButton;
    RectTransform _rectTransform;

    // Parameters
    [SerializeField] int _speed;
    Vector2 _resetPos = new Vector2(0, -257);

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Raises the credits text
        if (_rectTransform.localPosition.y < 1770)
        {
            _rectTransform.Translate(Vector2.up * _speed * Time.fixedDeltaTime *
                GlobalVariableTracker.windowScale);
        }
    }

    #endregion

    #region DEACTIVATION BUTTON

    /// <summary>
    /// Cancels the credits and resets position
    /// </summary>
    public void StopCredits()
    {
        _rectTransform.localPosition = _resetPos;
        _titleMenuButton.Select();
        transform.parent.gameObject.SetActive(false);
    }

    #endregion
}
