/*=================================================================================================
 * FILE     : MapMovement.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/1/25
 * UPDATED  : 12/4/25
 * 
 * DESC     : Allows the player to move the map within specified constraints
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MapMovement : MonoBehaviour
{

    #region VARIABLES

    // Components
    RectTransform _rectTransform;

    // Parameters
    [SerializeField] int _moveSpeed;

    // Containers
    Vector3 _translationVector = new Vector3();
    // Constraints
    int _maxX;
    int _maxY;

    // Inputs
    [SerializeField] InputActionAsset _moveInputs;
    InputAction _xInput;
    InputAction _yInput;

    // External Reference
    [SerializeField] Image[] _arrowArray = new Image[4]; // 0=D, 1=L, 2=R, 3=U

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits Vars
        _rectTransform = GetComponent<RectTransform>();
        // Sets Inputs
        _xInput = _moveInputs.FindAction("MoveX");
        _yInput = _moveInputs.FindAction("MoveY");
        // Sets maximum constraints for movements
        _maxX = -(int)(_rectTransform.rect.width - 384);
        _maxY = 32 + (int)(_rectTransform.rect.height - 216);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Computes translation vector
        _translationVector.x = _moveSpeed * -_xInput.ReadValue<float>() * Time.deltaTime;
        _translationVector.y = _moveSpeed * -_yInput.ReadValue<float>() * Time.deltaTime;

        // Moves map
        _rectTransform.Translate(_translationVector);

        // Constrains map position
        _rectTransform.anchoredPosition = new Vector2
            (
            Mathf.Clamp(_rectTransform.anchoredPosition.x, _maxX, 0),
            Mathf.Clamp(_rectTransform.anchoredPosition.y, 0, _maxY));

        // Determines which arrows should be visible
        // Down
        if (_rectTransform.anchoredPosition.y == _maxY)
        {
            if (_arrowArray[0].color.a != 0)
            {
                _arrowArray[0].CrossFadeAlpha(0, 0, true);
            }
        }
        else if (_arrowArray[0].canvasRenderer.GetAlpha() != 1)
        {
            _arrowArray[0].CrossFadeAlpha(1, 0, true);
        }
        // Left
        if (_rectTransform.anchoredPosition.x == 0)
        {
            if (_arrowArray[1].color.a != 0)
            {
                _arrowArray[1].CrossFadeAlpha(0, 0, true);
            }
        }
        else if (_arrowArray[1].canvasRenderer.GetAlpha() != 1)
        {
            _arrowArray[1].CrossFadeAlpha(1, 0, true);
        }
        // Right
        if (_rectTransform.anchoredPosition.x == _maxX)
        {
            if (_arrowArray[2].color.a != 0)
            {
                _arrowArray[2].CrossFadeAlpha(0, 0, true);
            }
        }
        else if (_arrowArray[2].canvasRenderer.GetAlpha() != 1)
        {
            _arrowArray[2].CrossFadeAlpha(1, 0, true);
        }
        // Up
        if (_rectTransform.anchoredPosition.y == 0)
        {
            if (_arrowArray[3].color.a != 0)
            {
                _arrowArray[3].CrossFadeAlpha(0, 0, true);
            }
        }
        else if (_arrowArray[3].canvasRenderer.GetAlpha() != 1)
        {
            _arrowArray[3].CrossFadeAlpha(1, 0, true);
        }
    }

    #endregion
}
