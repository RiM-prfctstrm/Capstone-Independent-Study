/*=================================================================================================
 * FILE     : MoveByVectors.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/8/24
 * UPDATED  : 12/2/24
 * 
 * DESC     : Translates a character along a set of vectors.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Command",
    menuName = "Cutscene/Character Controls/Movement Sequence", order = 2)]
public class MoveByVectors : CutsceneEvent
{
    #region VARIABLES

    // Inputs
    [SerializeField] int _targetID;
    [SerializeField] float _speed = 4; // Default speed set to feel good for walking.
    public List<Vector2> _movements = new List<Vector2>();

    [SerializeField] bool _overrideMovementAnimation = false;
    [SerializeField] string _overrideAnimation;

    // Object Refs
    GameObject _targetCharacter;
    CharacterAnimator _targetAnimator;

    // Movement Parameters
    Vector2 _movementVector;
    Vector2 _targetPos;
    Vector2 _referencePos = new Vector2 (9999, 9999);

    #endregion

    #region EVENT FUNCTIONALITY
    
    /// <summary>
    /// Sets up movement
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the character the script acts on
        SetTarget();

        // Starts movement loop
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Moves the character in a set direction at a set speed for a set distance
    /// </summary>
    protected override IEnumerator WaitForEventEnd()
    {
        // Sets a reference to where the movement begins
        _referencePos = _targetCharacter.transform.position;

        // Loops through all movement vectors
        foreach (Vector2 step in _movements)
        {
            // Sets variables to determine movement
            _movementVector = step.normalized;
            _targetPos = (Vector2)_targetCharacter.transform.position + step;

            // Determines how to animate during movement
            if (_overrideMovementAnimation)
            {
                _targetAnimator.PlayScriptedAnimation(_overrideAnimation);
            }
            else
            {
                UpdateMoveAnimation();
            }

            // Moves along a vector
            while (Mathf.Abs(Vector2.Distance(_targetCharacter.transform.position, _targetPos))
                >= .05f)
            {
                // Performs Movement
                _targetCharacter.transform.Translate(
                    _movementVector * _speed * Time.fixedDeltaTime);

                // Prevents Avengers Infinity Loop
                yield return new WaitForFixedUpdate();
            }

            // adjusts character to target postiion
            _targetCharacter.transform.position = _targetPos;
        }

        // Sets a reference to where the movement ends
        _referencePos = _targetCharacter.transform.position;

        // Sets character to a finished animation and completes event
        _targetAnimator.PlayScriptedAnimation(_targetAnimator.SetAnimState());
        eventComplete = true;
    }

    /// <summary>
    /// Instantly performs the movement operation when the event is being skipped.
    /// </summary>
    public void MoveInstantly()
    {
        // Sets the character the script acts on
        SetTarget();
        if (_referencePos.Equals((9999, 9999)))
        {
            // Sets a reference position to where it begins
            _referencePos = _targetCharacter.transform.position;
        }

        // Gets starting position and final direction
        _targetPos = _referencePos;
        _movementVector = _movements[_movements.Count - 1];

        // Sets final destination
        foreach (Vector2 step in _movements)
        {
            _targetPos += step;
        }

        // Sets position and rotation
        _targetCharacter.transform.position = _targetPos;
        UpdateMoveAnimation();

        // Ends event
        eventComplete = true;
    }

    /// <summary>
    /// Automatically sets a movement animation when no override is specified. Logic order follows
    /// a circle starting on the positive side of the X axis.
    /// </summary>
    void UpdateMoveAnimation()
    {
        // Horizontal movement
        // Directional animation for rightward movement
        if (Vector2.Angle(Vector2.right, _movementVector) <= 45)
        {
            _targetAnimator.facingDirection = 2;
            _targetAnimator.PlayScriptedAnimation("Right");
        }
        // Directional animation for upward movement
        else if (Vector2.Angle(Vector2.right, _movementVector) >= 135)
        {
            _targetAnimator.facingDirection = 1;
            _targetAnimator.PlayScriptedAnimation("Left");
        }
        // Vertical movement
        // Directional animation for leftward movement
        else if (_movementVector.y < 0)
        {
            _targetAnimator.facingDirection = 0;
            _targetAnimator.PlayScriptedAnimation("Down");
        }
        // Directional animation for downward movement
        else
        {
            _targetAnimator.facingDirection = 3;
            _targetAnimator.PlayScriptedAnimation("Up");
        }
    }

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Sets the object the script acts on.
    /// </summary>
    void SetTarget()
    {
        // Gets objects and components
        _targetCharacter = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];
        _targetAnimator = _targetCharacter.GetComponent<CharacterAnimator>();
    }

    #endregion
}
