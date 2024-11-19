/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 11/19/24
 * 
 * DESC     : Controls the player character's movement and world interactions.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    // Player Singleton
    public static PlayerController playerController;

    // Components
    PlayerAnimator _playerAnimator;
    SpriteRenderer _playerRenderer;
    Rigidbody2D _rb2d;

    // External Components
    [SerializeField] DetectObjects _detector;
    DialogueManager _dialogueManager;

    // Parameters
    [SerializeField] float _accelTime;
    [SerializeField] float _brakeTime;
    [SerializeField] float _decelTime;
    [SerializeField] float _decelBuffer;
    [SerializeField] float _maxBikeSpeed;
    [SerializeField] float _walkSpeed;
    [SerializeField] int _steeringAngleThreshold;
    [SerializeField] float _steeringVelThreshold;

    // Movement Vars
    float _accelRate;
    float _brakeRate;
    float _decelRate;
    float _decelClamp;
    float _buffer;
    float _bufferRate;
    bool _isBraking;
    public bool isWalking;
    float _velocityX;
    float _velocityY;
    Vector2 _newVel;

    // Inputs
    [SerializeField] InputActionAsset _playerInputs;
    InputAction _brake;
    InputAction _debugSwitch;
    InputAction _openMenu;
    InputAction _interact;
    InputAction _xInput;
    InputAction _yInput;
    int _moveX;
    int _moveY;

    // External reference
    [SerializeField] GameObject _menu;
    GameObject _lastTarget;
    public GameObject lastTarget => _lastTarget;

    // Debug
    //bool _maxed = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets components
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _rb2d = GetComponent<Rigidbody2D>();

        // Sets external objects
        _dialogueManager = DialogueManager.dialogueManager;

        // Sets inputs
        _brake = _playerInputs.FindAction("Brake");
        _debugSwitch = _playerInputs.FindAction("DebugSwitchState");
        _openMenu = _playerInputs.FindAction("OpenMenu");
        _interact = _playerInputs.FindAction("Interact");
        _xInput = _playerInputs.FindAction("MoveX");
        _yInput = _playerInputs.FindAction("MoveY");

        // Assigns actions to inputs
        _brake.performed += ctx => _isBraking = true;
        _brake.canceled += ctx => _isBraking = false;
        _debugSwitch.performed += ctx => isWalking = !isWalking;
        _openMenu.performed += ctx => OpenMenu();
        _interact.performed += ctx => PerformInteraction();

        // Initializes Accel/Decel Rates
        SetAccelDecel();

        // DEBUG
        //StartCoroutine(LabTurn2Part());
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Debug commands
        // Exit Game
        if (Input.GetKey(KeyCode.Escape)) 
        { 
            Application.Quit(); 
        }
        // Restart
        if (Input.GetKey(KeyCode.R)) 
        { 
            SceneManager.LoadScene(0);
            Destroy(EssentialPreserver.instance.gameObject);
            EssentialPreserver.instance = null;
            Time.timeScale = 1; 
        }

        // Gets inputs for frame
        ValidateInputs();


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
            // Changes direction where the player can interact
            _detector.SetInteractionDirection(_playerAnimator.facingDirection);
        }
    }

    #endregion

    #region COLLISION CONTROLS

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided");

        // Reset velocity modifiers to compensate for collision DEBUG FIX
        if (!isWalking)
        {
            _velocityX = _rb2d.velocity.x;
            _velocityY = _rb2d.velocity.y;
        }
    }

    #endregion

    #region INPUT MANAGEMENT

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
        if (!isWalking && _rb2d.velocity.magnitude > _steeringVelThreshold)
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
    /// Deactivates input recognition, mostly used so the game can take over the player object
    /// during cutscenes.
    /// </summary>
    public void TogglePlayerInput()
    {
        if (_xInput.enabled)
        {
            _playerInputs.Disable();
            _interact.Enable();
        }
        else
        {
            _playerInputs.Enable();
        }
    }

    #endregion

    #region INTERACTIONS

    /// <summary>
    /// Controls what the interact button does
    /// </summary>
    void PerformInteraction()
    {
        if (!DialogueManager.dialogueInProgress)
        {
            // Initiates interactions with objects in game world
            if (_detector.target != null)
            {
                _detector.target.OnInteractedWith();
                _lastTarget = _detector.target.gameObject;
            }
            else
            {
                Debug.Log("No interaction");
            }
        }
        else
        {
            _dialogueManager.advancing = true;
        }
    }

    #endregion

    #region MOVEMENT FUNCTIONS

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
        // Computes velocity
        if (!_isBraking)
        {
            AccelerateX();
            AccelerateY();
        }
        
        // Steers bike
        if (((_moveX != 0 && _moveY == 0) || (_moveY != 0 && _moveX == 0)) && (
            _rb2d.velocity.x != 0 && _rb2d.velocity.y != 0))
        {
            BikeSteering();
            AccelerateX();
            AccelerateY();
            BikeSteering();
        }
        else if (UtilityFormulas.FindHypotenuse(_velocityX, _velocityY) > _maxBikeSpeed)
        {
            BikeSteering();
        }

        // Decelerates the bike
        if (_newVel.magnitude >= 0)
        {
            // Removes buffer while breaking
            if (_isBraking)
            {
                DecelerateBike(_brakeRate);
            }
            else if (_moveX == 0 && _moveY == 0)
            {
                if (_buffer > 0)
                {
                    _buffer -= _bufferRate;
                }
                else
                {
                    _decelClamp = _rb2d.velocity.magnitude;
                    DecelerateBike(_decelRate);
                }
            }
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

            // Debug feedback
            if (Mathf.Abs(_velocityX) >= _maxBikeSpeed)
            {
                Debug.Log("Maxed X!");
            }

            // Resets deceleration buffer
            _decelClamp = _decelTime; 
            _buffer = _decelBuffer * (_rb2d.velocity.magnitude / _maxBikeSpeed);
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

            // Debug feedback
            if (Mathf.Abs(_velocityY) >= _maxBikeSpeed)
            {
                Debug.Log("Maxed Y!");
            }

            // Resets deceleration buffer
            _decelClamp = _decelTime;
            _buffer = _decelBuffer * (_rb2d.velocity.magnitude / _maxBikeSpeed);
        }
    }

    /// <summary>
    /// Controls turning with the bike.
    /// </summary>
    void BikeSteering()
    {
        // Since diagonals are covered by the clamp later, this logic only controls when one button
        // is pressed.
        if (_moveX == 0 || _moveY == 0)
        {
            // Decreases Y velocity if X is increasing
            if (_moveX != 0)
            {
                // Subtracts to keep X
                _velocityY -= _accelRate * Mathf.Sign(_rb2d.velocity.y) * Time.fixedDeltaTime;

                // Stabilizes at 0
                if (_velocityY >= -.2f && _velocityY <= .2f)
                {
                    _velocityY = 0;
                }
            }
            
            // Decreases X velocity if Y is increasing
            if (_moveY != 0)
            {
                // Subtracts to keep Y
                _velocityX -= _accelRate * Mathf.Sign(_rb2d.velocity.x) * Time.fixedDeltaTime;

                // Stabilizes at 0
                if (_velocityX >= -.2f && _velocityX <= .2f)
                {
                    _velocityX = 0;
                }
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

    #endregion

    #region MENU CONTROLS

    /// <summary>
    /// Opens the menu
    /// </summary>
    void OpenMenu()
    {
        _menu.SetActive(true);
        _menu.GetComponent<InGameMainMenu>().defaultSelection.Select();
        TogglePlayerInput();
    }

    #endregion

    #region VAR MODIFIERS

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
            if (_rb2d.velocity.x >= 0) { return 2; }
            else { return 1; }
        }
        else
        {
            if (_rb2d.velocity.y >= 0) { return 3; }
            else { return 0; }
        }
    }

    /// <summary>
    /// Sets the rates at which the bike accelerates and decelerates
    /// </summary>
    void SetAccelDecel()
    {
        _accelRate = _maxBikeSpeed / _accelTime;
        _brakeRate = _maxBikeSpeed / _brakeTime;
        _decelRate = _maxBikeSpeed / _decelTime;
        _decelClamp = _decelTime;
        _buffer = _decelBuffer;
        _bufferRate = _decelBuffer * Time.fixedDeltaTime;
    }

    #endregion

    #region DEBUG

    /// <summary>
    /// Used to determine the space needed to turn 90 degrees, with the turn performed in 45deg
    /// intervals
    /// </summary>
    IEnumerator LabTurn2Part()
    {
        // Sets initial movement
        _moveX = 0;
        _moveY = 1;
        _rb2d.velocity = new Vector2(_maxBikeSpeed, 0);
        _velocityX = _maxBikeSpeed;

        // Gets location at first part of the turn once complete
        /*yield return new WaitUntil(() => _velocityY == _maxBikeSpeed);
        Debug.Log("Midpoint X: " + transform.position.x);
        Debug.Log("Midpoint Y: " + transform.position.y);
        _moveX = 0;*/

        // Finishes turn and gets location
        yield return new WaitUntil(() => _velocityX == 0);
        Debug.Log("Endpoint X: " + transform.position.x);
        Debug.Log("Endpoint Y: " + transform.position.y);
    }

    #endregion
}
