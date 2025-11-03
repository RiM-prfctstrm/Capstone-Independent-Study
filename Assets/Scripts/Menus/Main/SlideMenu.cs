/*=================================================================================================
 * FILE     : SlideMenu.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/3/25
 * UPDATED  : 11/3/25
 * 
 * DESC     : Slides menu canvas to a specified point.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideMenu : MonoBehaviour
{
    #region VARIABLES

    // Position Vars
    [SerializeField] int _heldcoord;
    Vector2 _start;
    Vector2 _target;

    // Speed Vars
    bool _inMove = false;
    float _duration = .1f;
    float _elapsed;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Slides object position
        if (_inMove)
        {
            GetComponent<RectTransform>().anchoredPosition =
                Vector2.Lerp(_start, _target, _elapsed / _duration);
            _elapsed += Time.deltaTime;
        }

        // Ends movement
        if (_elapsed > _duration)
        {
            _inMove = false;
        }
    }

    #endregion

    #region BUTTON ACTIONS

    /// <summary>
    /// Sets point for the menu to slide to
    /// </summary>
    /// <param name="pos">The position to slide to</param>
    public void SlideToPos(int pos)
    {
        // Vars
        RectTransform rt = GetComponent<RectTransform>();

        // Sets parameters
        _start = rt.anchoredPosition;
        _target = new Vector2(pos, _heldcoord);
        _elapsed = 0;
        _inMove = true;
    }

    #endregion
}
