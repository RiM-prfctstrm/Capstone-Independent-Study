/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 9/10/24
 * 
 * DESC     : Controls the player character's movement and world interactions.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /* VARS */

    //Components
    SpriteRenderer _playerRenderer;
    Animator _playerAnimator;
    Rigidbody2D _rb2D;

    //Parameters
    [SerializeField] float _walkSpeed;
    [SerializeField] float _maxBikeSpeed;
    [SerializeField] float _accelRate;
    [SerializeField] float _decelRate;
    [SerializeField] bool _isWalking;

    //Movement Vars
    float _velocityX;
    float _velocityY;

    //Inputs
    [SerializeField] InputActionAsset _playerInputs;
    InputAction _moveX;
    InputAction _moveY;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets components
        _playerAnimator = GetComponent<Animator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();

        // Sets inputs
        _moveX = _playerInputs.FindAction("MoveX");
        _moveY = _playerInputs.FindAction("MoveY");
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Controls which kind of movement to perform.
        if (_isWalking)
        {
            WalkMovement();
        }
        else
        {
            BikeMovement();
        }
    }

    /// <summary>
    /// Controls the player's movement while walking
    /// </summary>
    void WalkMovement()
    {
        // Sets velocity components
        _velocityX = _walkSpeed * (int)_moveX.ReadValue<float>() * Time.deltaTime;
        _velocityY = _walkSpeed * (int)_moveY.ReadValue<float>() * Time.deltaTime;

        // Sets Player's velocity
        _rb2D.velocity = new Vector2(_velocityX, _velocityY);
    }

    /// <summary>
    /// Controls the player's movement while biking
    /// </summary>
    void BikeMovement()
    {

    }
}
