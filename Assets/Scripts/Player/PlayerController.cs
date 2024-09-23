/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 9/21/24
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
    float _decelComponent;
    Vector2 _newVel;

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
        _newVel.x =+ _velocityX;
        _newVel.y = _velocityY;
        _newVel = Vector2.ClampMagnitude(_newVel,
            UtilityFormulas.FindHypotenuse(_velocityX, _velocityY));
        _rb2d.velocity = _newVel;
    }

    /// <summary>
    /// Controls the player's movement while biking
    /// </summary>
    void BikeMovement()
    {
        // Set Vars
        _decelComponent = _decelRate * Time.deltaTime;

        // Compute velocity
        BikeAcceleration();
        if (UtilityFormulas.FindHypotenuse(_velocityX, _velocityY) > _maxBikeSpeed)
        {
            //BikeSteering();
        }

        /*// Determines velocity sign
        if ((int)_moveX.ReadValue<float>() == -1 || _rb2d.velocity.x < 0)
            _velocityX *= -1;
        if ((int)_moveY.ReadValue<float>() == -1 || _rb2d.velocity.y < 0)
            _velocityY *= -1;*/

        // Ensures velocity zeroes out
        if (Mathf.Abs(_velocityX) <= .015f)
            _velocityX = 0;
        if (Mathf.Abs(_velocityY) <= .015f)
            _velocityY = 0;

        // Sets Player's velocity
        _newVel.x = _velocityX;
        _newVel.y = _velocityY;
        _newVel = Vector2.ClampMagnitude(_newVel, _maxBikeSpeed);
        _rb2d.velocity = _newVel;
        
    }

    /// <summary>
    /// Performs initial acceleration of X and Y velocity components.
    /// MOVE DECELERATION AFTER CLAMPING FOR SMOOTHER STOPPING
    /// </summary>
    void BikeAcceleration()
    {
        /* DETERMINES INITIAL VELOCITY COMPONENTS */
        // X component
        // Acceleration from player input
        if ((int)_moveX.ReadValue<float>() != 0)
        {
            _velocityX += _accelRate * (int)_moveX.ReadValue<float>() * Time.fixedDeltaTime;
            _velocityX = Mathf.Clamp(_velocityX, -_maxBikeSpeed, _maxBikeSpeed);
            if (Mathf.Abs(_velocityX) >= _maxBikeSpeed)
                Debug.Log("Maxed X!");
        }
        // Deceleration
        else if (_rb2d.velocity.x != 0)
        {
            // Braking logic might go here

            //Natural Deceleration
            if (_velocityX > 0)
                _velocityX -= _decelComponent;
            else
                _velocityX += _decelComponent;
        }

        // Y component
        // Acceleration from player input
        if ((int)_moveY.ReadValue<float>() != 0)
        {
            _velocityY += _accelRate * (int)_moveY.ReadValue<float>() * Time.fixedDeltaTime;
            _velocityY = Mathf.Clamp(_velocityY, -_maxBikeSpeed, _maxBikeSpeed);
            if (Mathf.Abs(_velocityY) >= _maxBikeSpeed)
                Debug.Log("Maxed Y!");
        }
        // Deceleration
        else if (_rb2d.velocity.y != 0)
        {
            // Braking logic might go here

            //Natural Deceleration
            if (_velocityY > 0)
                _velocityY -= _decelComponent;
            else
                _velocityY += _decelComponent;
        }
    }

    /// <summary>
    /// Controls turning with the bike.
    /// </summary>
    void BikeSteering()
    {
        // Since diagonals are covered by the clamp later, this logic only controls when one button
        // is pressed.
        // Lowers X magnitude as Y increases
        if ((int)_moveX.ReadValue<float>() == 0 /*|| (int)_moveY.ReadValue<float>() == 0*/)
        {
            _velocityX = UtilityFormulas.FindTriangleLeg(_maxBikeSpeed, _velocityY);

           /*// Determines which direction the player is trying to go and subtracts velocity from
            // the other axis
            if ((int)_moveX.ReadValue<float>() != 0)
            {
                _velocityY = UtilityFormulas.FindTriangleLeg(_maxBikeSpeed, _velocityX)
                    * Time.fixedDeltaTime;
                if (_rb2d.velocity.y < 0)
                    _velocityY *= -1;
            }
            else if ((int)_moveY.ReadValue<float>() != 0)
            {
                _velocityX = UtilityFormulas.FindTriangleLeg(_maxBikeSpeed, _velocityY)
                    * Time.fixedDeltaTime;
                if (_rb2d.velocity.x < 0)
                    _velocityX *= -1;
            }*/
        }
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
        _accelRate = _maxBikeSpeed / _accelTime;
        _decelRate = _maxBikeSpeed / _decelTime;
        //Debug.Log(_accelRate + ", " + _decelRate);
    }
}
