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
    PlayerAnimator _playerAnimator;
    Rigidbody2D _rb2d;

    //Parameters
    [SerializeField] float _walkSpeed;
    [SerializeField] float _maxBikeSpeed;
    [SerializeField] float _accelTime;
    [SerializeField] float _decelTime;    
    public bool _isWalking;

    //Movement Vars
    float _velocityX;
    float _velocityY;
    float _accelRate;
    float _decelRate;

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
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();

        // Sets inputs
        _moveX = _playerInputs.FindAction("MoveX");
        _moveY = _playerInputs.FindAction("MoveY");

        // Initializes Accel/Decel Rates
        SetAccelDecel();
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

        // Updates animations
        if (_rb2d.velocity != Vector2.zero)
        {
            _playerAnimator.facingDirection = GetDirection();
        }
    }

    /// <summary>
    /// Controls the player's movement while walking
    /// </summary>
    void WalkMovement()
    {
        // Sets velocity components
        _velocityX = _walkSpeed * (int)_moveX.ReadValue<float>() * Time.fixedDeltaTime;
        _velocityY = _walkSpeed * (int)_moveY.ReadValue<float>() * Time.fixedDeltaTime;

        // Sets Player's velocity
        _rb2d.velocity = new Vector2(_velocityX, _velocityY);
    }

    /// <summary>
    /// Controls the player's movement while biking
    /// </summary>
    void BikeMovement()
    {
        /* DETERMINES INITIAL VELOCITY COMPONENTS */
        // X component
        if ((int)_moveX.ReadValue<float>() != 0)
        {
            _velocityX += _accelRate * (int)_moveX.ReadValue<float>() * Time.fixedDeltaTime;
            Mathf.Clamp(_velocityX, 0, _maxBikeSpeed);
        }
        /*else if (_rb2d.velocity.x != 0)
        {
            // Braking logic might go here
            _velocityX -= _decelRate * Time.deltaTime;
        }*/

        // Y component
        if ((int)_moveY.ReadValue<float>() != 0)
        {
            _velocityY += _accelRate * (int)_moveY.ReadValue<float>() * Time.fixedDeltaTime;
            Mathf.Clamp(_velocityY, 0, _maxBikeSpeed);
        }
        /*else if (_rb2d.velocity.y != 0)
        {
            // Braking logic might go here
            _velocityY -= _decelRate * Time.deltaTime;
        } */

        // Multidirectional logic


        /*// Determines velocity sign
        if ((int)_moveX.ReadValue<float>() == -1 || _rb2d.velocity.x < 0)
            _velocityX *= -1;
        if ((int)_moveY.ReadValue<float>() == -1 || _rb2d.velocity.y < 0)
            _velocityY *= -1;*/

        // Sets Player's velocity
        _rb2d.velocity = new Vector2(_velocityX, _velocityY);
    }

    /// <summary>
    /// Tells which direction the character is facing
    /// </summary>
    /// <returns>Integer shorthand for direction
    /// 0=D, 1=L, 2=R, 3=U</returns>
    int GetDirection()
    {
        // Bases direction on velocity, prioritizing X axis and positive values
        if (Mathf.Abs(_rb2d.velocity.x) >= Mathf.Abs(_rb2d.velocity.y))
        {
            if (_rb2d.velocity.x >= 0)
                return 2;
            else
                return 1;
        }
        else
        {
            if (_rb2d.velocity.y >= 0)
                return 3;
            else
                return 0;
        }
    }

    /// <summary>
    /// Sets the rates at which the bike accelerates and decelerates
    /// </summary>
    void SetAccelDecel()
    {
        _accelRate = (_maxBikeSpeed / _accelTime) * Time.fixedDeltaTime;
        _decelRate = (_maxBikeSpeed / _decelTime) * Time.fixedDeltaTime;
        Debug.Log(_accelRate + ", " + _decelRate);
    }
}
