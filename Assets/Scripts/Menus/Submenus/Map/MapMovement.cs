/*=================================================================================================
 * FILE     : MapMovement.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/1/25
 * UPDATED  : 12/1/25
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
        _maxX = -(166 + (int)(_rectTransform.rect.width - 384));
        _maxY = 114 + (int)(_rectTransform.rect.height - 216);
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
        _rectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(_rectTransform.anchoredPosition.x, _maxX, -166),
            Mathf.Clamp(_rectTransform.anchoredPosition.y, 82, _maxY));
    }

    #endregion
}
