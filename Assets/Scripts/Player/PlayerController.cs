/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 4/29/25
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
    AudioSource _playerAudioSource;
    public AudioSource playerAudioSource => _playerAudioSource;
    PlayerAnimator _playerAnimator;
    SpriteRenderer _playerRenderer;
    public Rigidbody2D rb2d;
    // External Components
    [SerializeField] DetectObjects _detector;
    [SerializeField] ParticleSystem _lossParticles;
    public static GameObject playerCamera;
    // Parameters
    [SerializeField] float _accelTime;
    [SerializeField] float _brakeTime;
    [SerializeField] float _decelTime;
    [SerializeField] float _decelBuffer;
    [SerializeField] float _lossBuffer;
    [SerializeField] float _maxBikeSpeed;
    [SerializeField] float _walkSpeed;
    [SerializeField] int _steeringAngleThreshold;
    [SerializeField] float _steeringVelThreshold;
    [SerializeField] PhysicsMaterial2D _bikeMaterial;

    // Movement Vars
    float _accelRate;
    float _brakeRate;
    float _decelRate;
    float _decelClamp;
    float _buffer;
    float _bufferRate;
    float _moveX;
    float _moveY;
    float _velocityX;
    float _velocityY;
    Vector2 _newVel;

    // States
    bool _autoBraking;
    bool _isBraking;
    public bool isWalking;
    public bool inBikeableArea;
    bool _movementDisabled = false;
    public bool movementDisabled => _movementDisabled;

    // Inputs
    [SerializeField] InputActionAsset _playerInputs;
    InputAction _brake;
    public InputAction cancel;
    InputAction _debugSwitch;
    public InputAction openMenu;
    InputAction _interact;
    InputAction _xInput;
    InputAction _yInput;

    // External reference
    DialogueManager _dialogueManager;
    public GameObject lastTarget;
    [SerializeField] GameObject _menu;
    Button _mainMenuDefault;

    // Sound effects
    [SerializeField] AudioClip _bikeBell;
    [SerializeField] AudioClip _bumpSound;
    [SerializeField] AudioClip _cancelSound;
    public AudioClip sceneShift;

    // Miscellaneous
    [SerializeField] Cutscene _cantRideMessage;

    // Debug
    //bool _maxed = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Sets components
        _playerAudioSource = GetComponent<AudioSource>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        playerCamera = GetComponentInChildren<Camera>().gameObject;
        rb2d = GetComponent<Rigidbody2D>();

        // Sets external objects
        _dialogueManager = DialogueManager.dialogueManager;
        _mainMenuDefault = _menu.GetComponent<InGameMainMenu>().defaultSelection;

        // Sets inputs
        _brake = _playerInputs.FindAction("Brake");
        cancel = _playerInputs.FindAction("Cancel");
        _debugSwitch = _playerInputs.FindAction("DebugSwitchState");
        openMenu = _playerInputs.FindAction("OpenMenu");
        _interact = _playerInputs.FindAction("Interact");
        _xInput = _playerInputs.FindAction("MoveX");
        _yInput = _playerInputs.FindAction("MoveY");

        // Assigns actions to inputs
        _brake.performed += StartBraking;
        _brake.canceled += StopBraking;
        cancel.performed += PlayCancelSound;
        cancel.Disable();
        _debugSwitch.performed += ToggleBike;
        openMenu.performed += OpenMenu;
        _interact.performed += PerformInteraction;

        // Initializes Accel/Decel Rates
        SetAccelDecel();

        // Initializes volume
        SetSoundEffectsVolume();

        // DEBUG
        //StartCoroutine(LabTurn2Part());

        // Initializes main menu singelton. Done here to prevent Nullreferences
        InGameMainMenu.inGameMainMenu = _menu.GetComponent<InGameMainMenu>();
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Gets inputs for frame
        if (!_movementDisabled)
        {
            ValidateInputs();
        }
        else
        {
            _moveX = 0;
            _moveY = 0;
        }

        // Controls which kind of movement to perform.
        if (isWalking)
        {
            WalkMovement();
        }
        else
        {
            BikeMovement();
        }

        // Automatically brakes
        if (_autoBraking)
        {
            _isBraking = true;
            
            // Ends Autobrake
            if (rb2d.velocity == Vector2.zero)
            {
                _isBraking = false;
                _autoBraking = false;
            }
        }

        // Updates animations
        if (rb2d.velocity != Vector2.zero)
        {
            _playerAnimator.facingDirection = GetDirection();
            // Changes direction where the player can interact
            UpdateDetection();
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
        // Collision vars
        float impactForce = new Vector2(_velocityX, _velocityY).magnitude;

        // Bike specific collision logic
        if (!isWalking)
        {
            // Loses Collectibles
            if (impactForce > _lossBuffer)
            {
                // Performs loss
                LoseCollectibles(impactForce,
                    90 - Vector2.Angle(rb2d.velocity, collision.GetContact(0).normal));

                // Performs visible collision effects
                StartCoroutine(_playerAnimator.SpinOut(impactForce / _maxBikeSpeed));
            }

            // Reset velocity modifiers to compensate for collision DEBUG FIX
            _velocityX = rb2d.velocity.x;
            _velocityY = rb2d.velocity.y;   
        }
    }

    #endregion

    #region INPUT MANAGEMENT

    #region INPUT MODE CONTROLS

    /// <summary>
    /// Disables certain inputs while biking based on the rigidbody's velocity angle to prevent the
    /// player from directly reverse.
    /// </summary>
    void ValidateInputs()
    {
        // Converts input into variables used for calculating movement
        _moveX = _xInput.ReadValue<float>();
        _moveY = _yInput.ReadValue<float>();

        // Alters movement vars if they would lead the player in a direction that is invalid for
        // their current direction. Inputs are valid when the player's direction is <= 135 degrees
        // from the input direction.
        if (!isWalking && rb2d.velocity.magnitude > _steeringVelThreshold)
        {
            // Prevents leftward movement while moving right
            if (_moveX < 0 && Vector2.Angle(Vector2.left, rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveX = 0;
            }

            // Prevents rightward moving while moving left
            if (_moveX > 0 && Vector2.Angle(Vector2.right, rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveX = 0;
            }

            // Prevents downward movement while moving up
            if (_moveY < 0 && Vector2.Angle(Vector2.down, rb2d.velocity)
                > _steeringAngleThreshold)
            {
                _moveY = 0;
            }

            // Prevents upward moving while moving down
            if (_moveY > 0 && Vector2.Angle(Vector2.up, rb2d.velocity)
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
        // Disables Input
        if (_brake.enabled)
        {
            // Disables input functions
            _brake.Disable();
            _debugSwitch.Disable();
            openMenu.Disable();
            _movementDisabled = true;

            // Keeps interaction enabled. Don't know why I have to explicitly spell this out, but
            // it doesn't work if I don't.
            _interact.Enable();

            // Resets animation
            _playerAnimator.PlayScriptedAnimation(_playerAnimator.SetAnimState());
        }
        // Enables Input
        else
        {
            // Enables input functions
            _brake.Enable();
            _debugSwitch.Enable();
            openMenu.Enable();
            _movementDisabled = false;

            // Makes sure break is off when leaving menus
            _isBraking = false;

            // Disables menu cancel
            cancel.Disable();
            cancel.performed -= _menu.GetComponent<InGameMainMenu>().ExitMenu;

            // Keeps interaction enabled. Don't know why I have to explicitly spell this out, but
            // it doesn't work if I don't.
            _interact.Enable();
        }
    }

    /// <summary>
    /// Disables function of all inputs in the game
    /// </summary>
    public void ClearAllInputFunctions()
    {
        _brake.performed -= StartBraking;
        _brake.canceled -= StopBraking;
        cancel.performed -= PlayCancelSound;
        _debugSwitch.performed -= ToggleBike;
        openMenu.performed -= OpenMenu;
        _interact.performed -= PerformInteraction;
    }

    #endregion

    #region INPUT FUNCTIONS
    // Functions used by the input system so they can be removed when quitting the game. This
    // section only includes functions for controls that didn't already play a function

    /// <summary>
    /// Enters braking state
    /// </summary>
    void StartBraking(InputAction.CallbackContext ctx)
    {
        _isBraking = true;
    }

    /// <summary>
    /// Exits braking state
    /// </summary>
    void StopBraking(InputAction.CallbackContext ctx)
    {
        _isBraking = false;
    }

    /// <summary>
    /// Plays the sound for picking up a collectible
    /// </summary>
    public void PlayCancelSound(InputAction.CallbackContext ctx)
    {
        CollectibleManager.collectibleManager.GetComponent<AudioSource>()
            .PlayOneShot(_cancelSound);
    }

    #endregion'

    #endregion

    #region INTERACTIONS

    /// <summary>
    /// Controls what the interact button does
    /// </summary>
    void PerformInteraction(InputAction.CallbackContext ctx)
    {
        if (!CutsceneManager.inCutscene && !DialogueManager.dialogueInProgress)
        {
            // Initiates interactions with objects in game world
            if (_detector.target != null)
            {
                _detector.target.OnInteractedWith();
                lastTarget = _detector.target.gameObject;

                // Brakes for interaction
                _autoBraking = true;
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
        rb2d.velocity = _newVel;
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

            // Steers bike
            if (((_moveX != 0 && _moveY == 0) || (_moveY != 0 && _moveX == 0)) && (
                rb2d.velocity.x != 0 && rb2d.velocity.y != 0))
            {
                BikeSteering();
                AccelerateX();
                AccelerateY();
                BikeSteering();
            }
            else if (UtilityFormulas.FindHypotenuse(_velocityX, _velocityY) > _maxBikeSpeed)
            {
                BikeSteering();
                AccelerateX();
                AccelerateY();
                BikeSteering();
            }
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
                    _decelClamp = rb2d.velocity.magnitude;
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
        rb2d.velocity = _newVel;
        
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

            // Resets deceleration buffer
            _decelClamp = _decelTime; 
            _buffer = _decelBuffer * (rb2d.velocity.magnitude / _maxBikeSpeed);
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

            // Resets deceleration buffer
            _decelClamp = _decelTime;
            _buffer = _decelBuffer * (rb2d.velocity.magnitude / _maxBikeSpeed);
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
                _velocityY -= _accelRate * Mathf.Sign(rb2d.velocity.y) * Time.fixedDeltaTime;

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
                _velocityX -= _accelRate * Mathf.Sign(rb2d.velocity.x) * Time.fixedDeltaTime;

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

    /// <summary>
    /// Switches from walking to biking, and changes physics materials
    /// </summary>
    public void ToggleBike()
    {
        // Displays message telling player they can't ride in this area
        if (!inBikeableArea)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_cantRideMessage);
        }
        // Prevents player from deactivating bike while moving, as a balance measure
        else if (isWalking || rb2d.velocity == Vector2.zero)
        {
            // Switches mode
            isWalking = !isWalking;

            // Changes material
            if (!isWalking)
            {
                _playerAudioSource.PlayOneShot(_bikeBell);
                rb2d.sharedMaterial = _bikeMaterial;
            }
            else
            {
                rb2d.sharedMaterial = null;
            }
        }
    }
    public void ToggleBike(InputAction.CallbackContext ctx)
    {
        // Displays message telling player they can't ride in this area
        if (!inBikeableArea)
        {
            CutsceneManager.cutsceneManager.StartCutscene(_cantRideMessage);
        }
        // Prevents player from deactivating bike while moving, as a balance measure
        else if (isWalking || rb2d.velocity == Vector2.zero)
        {
            // Switches mode
            isWalking = !isWalking;

            // Changes material
            if (!isWalking)
            {
                _playerAudioSource.PlayOneShot(_bikeBell);
                rb2d.sharedMaterial = _bikeMaterial;
            }
            else
            {
                rb2d.sharedMaterial = null;
            }
        }
    }

    /// <summary>
    /// Stops player in their tracks and zeros movement vars to stay there
    /// </summary>
    public void CancelMomentum()
    {
        // Zeroes movement vars
        _moveX = 0;
        _moveY = 0;
        _velocityX = 0;
        _velocityY = 0;

        // Stops rigidbody
        rb2d.velocity = Vector2.zero;
    }

    #endregion

    #region MENU CONTROLS

    /// <summary>
    /// Opens the menu
    /// </summary>
    public void OpenMenu(GameObject menu, Button selection)
    {
        // Prevents menu from opening in dialogue
        if (DialogueManager.dialogueInProgress)
        {
            return;
        }

        // Opens menu
        menu.SetActive(true);
        selection.Select();

        // Allows cancelling from menu
        cancel.Enable();
        if (menu.GetComponent<InGameMainMenu>() != null)
        {
            cancel.performed += menu.GetComponent<InGameMainMenu>().ExitMenu;
        }

        // Disables non-menu inputs
        if (!CutsceneManager.inCutscene)
        {
            TogglePlayerInput();
            _isBraking = true;
        }
        _interact.Disable();
    }
    public void OpenMenu(InputAction.CallbackContext ctx)
    {
        // Prevents menu from opening in dialogue
        if (DialogueManager.dialogueInProgress)
        {
            return;
        }

        // Opens menu
        _menu.SetActive(true);
        _mainMenuDefault.Select();

        // Allows cancelling from menu
        cancel.Enable();
        if (_menu.GetComponent<InGameMainMenu>() != null)
        {
            cancel.performed += _menu.GetComponent<InGameMainMenu>().ExitMenu;
        }

        // Disables non-menu inputs
        if (!CutsceneManager.inCutscene)
        {
            TogglePlayerInput();
            _isBraking = true;
        }
        _interact.Disable();
    }

    /// <summary>
    /// Plays the sound for picking up a collectible
    /// </summary>
    public void PlayCancelSound()
    {
        CollectibleManager.collectibleManager.GetComponent<AudioSource>()
            .PlayOneShot(_cancelSound);
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
        if (Mathf.Abs(rb2d.velocity.x) >= Mathf.Abs(rb2d.velocity.y))
        {
            if (rb2d.velocity.x >= 0) { return 2; }
            else { return 1; }
        }
        else
        {
            if (rb2d.velocity.y >= 0) { return 3; }
            else { return 0; }
        }
    }


    /// <summary>
    /// Calculates the number of collectibles to be lost when colliding with an obstacle
    /// </summary>
    /// <param name="collisionSpeed">Player's speed before collision</param>
    /// <param name="collisionAngle">Angle between velocity and collider</param>
    void LoseCollectibles(float collisionSpeed, float collisionAngle)
    {
        // Play Collision Sound Effect
        _playerAudioSource.PlayOneShot(_bumpSound);

        // Calculates component variables
        float collisionForce = Mathf.Pow(collisionSpeed / _maxBikeSpeed, 2);
        float collisionDirectness = (collisionAngle + 10) / 100;
        int subtractionTotal = -(int)
            (GlobalVariableTracker.collectiblesInPocket * (collisionDirectness * collisionForce));

        // Play Collision Particle Effect
        _lossParticles.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, -
            Mathf.Clamp(subtractionTotal, -10, 0)));
        _lossParticles.Play();

        // Subtracts from total
        CollectibleManager.collectibleManager.AdjustCount(subtractionTotal);
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

    /// <summary>
    /// Sets the volume of in-game sound effects
    /// </summary>
    public void SetSoundEffectsVolume()
    {
        _playerAudioSource.volume = GlobalVariableTracker.sfxVolume;
    }

    /// <summary>
    /// Updates which direction the player's interaction space is facing.
    /// </summary>
    public void UpdateDetection()
    {
        _detector.SetInteractionDirection(_playerAnimator.facingDirection);
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
        rb2d.velocity = new Vector2(_maxBikeSpeed, 0);
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
