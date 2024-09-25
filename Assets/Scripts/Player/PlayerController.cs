/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 9/25/24
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
    [SerializeField] float _brakeTime;
    [SerializeField] float _decelBuffer;
    [SerializeField] int _steeringAngleThreshold;
    [SerializeField] float _steeringVelThreshold;
    public bool isWalking;

    //Movement Vars
    float _velocityX;
    float _velocityY;
    float _accelRate;
    float _decelRate;
    float _brakeRate;
    float _decelComponent;
    float _decelClamp;
    float _buffer;
    Vector2 _newVel;
    Vector2 _steeringVector;
    bool _isBraking;

    //Inputs
    [SerializeField] InputActionAsset _playerInputs;
    InputAction _xInput;
    InputAction _yInput;
    InputAction _brake;
    int _moveX;
    int _moveY;

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
        _xInput = _playerInputs.FindAction("MoveX");
        _yInput = _playerInputs.FindAction("MoveY");
        _brake = _playerInputs.FindAction("Brake");

        _brake.performed += ctx => _isBraking = true;
        _brake.canceled += ctx => _isBraking = false;

        // Initializes Accel/Decel Rates
        SetAccelDecel();
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Gets inputs for frame
        ValidateInputs();

        // Controls movement states
        // Braking state
        /*if (_brake.IsPressed())
            _isBraking = true;
        else
            _isBraking = false;*/

        // Controls which kind of movement to perform.
        if (isWalking)
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
    /// Disables certain inputs while biking based on the rigidbody's velocity angle to prevent the
    /// player from directly reverse.
    /// </summary>
    void ValidateInputs()
    {
        // Converts input into variables used for calculating movement
        _moveX = (int)_xInput.ReadValue<float>();
        _moveY = (int)_yInput.ReadValue<float>();

        // Alters movement vars if they would lead the player in a direction that is invalid for
        // their current direction. Inputs are valid when the player's direction is <= 135 degrees
        // from the input direction.
        if(!isWalking && _rb2d.velocity.magnitude > _steeringVelThreshold)
        {
            // Prevents leftward movement while moving right
            if (_moveX == -1 && Vector2.Angle(Vector2.left, _rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveX = 0;
            }

            // Prevents rightward moving while moving left
            if (_moveX == 1 && Vector2.Angle(Vector2.right, _rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveX = 0;
            }

            // Prevents downward movement while moving up
            if (_moveY == -1 && Vector2.Angle(Vector2.down, _rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveY = 0;
            }

            // Prevents upward moving while moving down
            if (_moveY == 1 && Vector2.Angle(Vector2.up, _rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveY = 0;
            }
        }
    }

    /// <summary>
    /// Controls the player's movement while walking
    /// </summary>
    void WalkMovement()
    {
        // Sets velocity components
        _velocityX = _walkSpeed * _moveX * Time.fixedDeltaTime;
        _velocityY = _walkSpeed * _moveY * Time.fixedDeltaTime;

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
        _decelComponent = _decelRate * Time.fixedDeltaTime;

        // Compute velocity
        //BikeAcceleration();
        if (!_isBraking)
        {
            AccelerateX();
            AccelerateY();
        }
        if (UtilityFormulas.FindHypotenuse(_velocityX, _velocityY) > _maxBikeSpeed)
        {
            BikeSteering();
        }

        /*// Determines velocity sign
        if ((int)_moveX.ReadValue<float>() == -1 || _rb2d.velocity.x < 0)
            _velocityX *= -1;
        if ((int)_moveY.ReadValue<float>() == -1 || _rb2d.velocity.y < 0)
            _velocityY *= -1;*/    

        // Decelerates the bike
        if (_newVel.magnitude >= 0)
        {
            // Removes buffer while breaking
            if (_isBraking)
            {
                /*if (_decelClamp > _maxBikeSpeed)
                {
                    _decelClamp = _maxBikeSpeed;
                }*/

                DecelerateBike(_brakeRate);
            }
            /*else if (_moveX == 0 || _moveY == 0)
            {
                DecelerateBike(_decelRate);
            }*/
        }

        // Ensures velocity zeroes out
        if (Mathf.Abs(_velocityX) <= .015f)
            _velocityX = 0;
        if (Mathf.Abs(_velocityY) <= .015f)
            _velocityY = 0;

        // Sets player's velocity
        _newVel.x = _velocityX;
        _newVel.y = _velocityY;
        _newVel = Vector2.ClampMagnitude(_newVel, _maxBikeSpeed);
        _rb2d.velocity = _newVel;
        
    }

    /*/// <summary>
    /// Performs initial acceleration of X and Y velocity components.
    /// MOVE DECELERATION AFTER CLAMPING FOR SMOOTHER STOPPING
    /// </summary>
    void BikeAcceleration()
    {
        /* DETERMINES INITIAL VELOCITY COMPONENTS
        // X component
        // Acceleration from player input
        

        // Y component
        
    }*/

    /// <summary>
    /// Accelerates X component of player velocity
    /// </summary>
    void AccelerateX()
    {
        // Acceleration from player input
        if (_moveX != 0)
        {
            // Accelerates on axis
            _velocityX += _accelRate * _moveX * Time.fixedDeltaTime;
            _velocityX = Mathf.Clamp(_velocityX, -_maxBikeSpeed, _maxBikeSpeed);
            if (Mathf.Abs(_velocityX) >= _maxBikeSpeed)
                Debug.Log("Maxed X!");

            // Resets deceleration buffer
            _decelClamp = _decelTime; //+ _decelBuffer;
            _buffer = _decelBuffer;
        }
        // Deceleration
        else if (_rb2d.velocity.x != 0)
        {
            // Subtracts from buffer before decelerating
            if (_buffer > 0)
            {
                _buffer -= _decelComponent;
            }
            else
            {
                //Natural Deceleration
                if (_velocityX > 0)
                    _velocityX -= _decelComponent;
                else
                    _velocityX += _decelComponent;
            }
        }
    }

    /// <summary>
    /// Accelerates Y component of player velocity
    /// </summary>
    void AccelerateY()
    {
        // Acceleration from player input
        if (_moveY != 0)
        {
            // Accelerates on axis
            _velocityY += _accelRate * _moveY * Time.fixedDeltaTime;
            _velocityY = Mathf.Clamp(_velocityY, -_maxBikeSpeed, _maxBikeSpeed);
            if (Mathf.Abs(_velocityY) >= _maxBikeSpeed)
                Debug.Log("Maxed Y!");

            // Resets deceleration buffer
            _decelClamp = _decelTime; //+ _decelBuffer;
            _buffer = _decelBuffer;
        }
        // Deceleration
        else if (_rb2d.velocity.y != 0)
        {
            // Subtracts from buffer before decelerating
            if (_buffer > 0)
            {
                _buffer -= _decelComponent;
            }
            else
            {
                //Natural Deceleration
                if (_velocityY > 0)
                    _velocityY -= _decelComponent;
                else
                    _velocityY += _decelComponent;
            }
        }
    }

    /// <summary>
    /// Controls turning with the bike.
    /// </summary>
    void BikeSteering()
    {
        // Sets vector used to aid in steering calculation
        _steeringVector.x = _velocityX;
        _steeringVector.y = _velocityY;
        _steeringVector = Vector2.ClampMagnitude(_steeringVector, _maxBikeSpeed);

        // Since diagonals are covered by the clamp later, this logic only controls when one button
        // is pressed.
        if (_moveX == 0 || _moveY == 0)
        {
            // Decreases Y velocity if X is increasing
            if (_moveX != 0)
            {
                // Brings X down to perform calculation accurately calculate needed Y velocity
                _velocityX = _steeringVector.x;
                //AccelerateX();

                // Calculates Y value needed to maintain max speed
                _velocityY = UtilityFormulas.FindTriangleLeg(_maxBikeSpeed, _velocityX);
                if (_rb2d.velocity.y < 0)
                    _velocityY *= -1;
            }
            
            // Decreases X velocity if Y is increasing
            if (_moveY != 0)
            {
                // Brings Y down to perform calculation accurately calculate needed X velocity
                _velocityY = _steeringVector.y;
                //AccelerateY();

                // Calculates Y value needed to maintain max speed
                _velocityX = UtilityFormulas.FindTriangleLeg(_maxBikeSpeed, _velocityY);
                if (_rb2d.velocity.x < 0)
                    _velocityX *= -1;
            }
        }
    }

    /// <summary>
    /// Decelerates the bike
    /// </summary>
    /// <param name="rate">Rate at which bike decelerates</param>
    void DecelerateBike(float rate)
    {
        // Performs decelration
        _decelClamp -= rate * Time.fixedDeltaTime;
        if (_decelClamp < 0)
        {
            _decelClamp = 0;
        }
        _newVel = Vector2.ClampMagnitude(_newVel, _decelClamp);

        // Adjusts velocity vars
        _velocityX = _newVel.x;
        _velocityY = _newVel.y;
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
        _brakeRate = _maxBikeSpeed / _brakeTime;
        _decelClamp = _decelTime; //+ _decelBuffer;
        _buffer = _decelBuffer;
        //Debug.Log(_accelRate + ", " + _decelRate);
    }
}
