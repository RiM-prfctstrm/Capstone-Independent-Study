/*=================================================================================================
 * FILE     : MoveByVectors.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/8/24
 * UPDATED  : 3/25/25
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
    [SerializeField] protected int _targetID;
    [SerializeField] protected float _speed = 4; // Default speed set to feel good for walking.
    public List<Vector2> _movements = new List<Vector2>();

    // Extra animation controls
    [SerializeField] protected bool _overrideMovementAnimation = false;
    [SerializeField] protected string _overrideAnimation;

    // Object Refs
    protected GameObject _targetCharacter;
    protected CharacterAnimator _targetAnimator;

    // Movement Parameters
    protected Vector2 _movementVector;
    Vector2 _targetPos;
    [SerializeField] Vector2 _referencePos; // Hardcoding is an inelegant solution to the
    // displacement bug, but at this point, implementation speed trumps everything.

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

        // Sets character to a finished animation and completes event
        _targetAnimator.animState += "Idle";
        _targetAnimator.PlayScriptedAnimation(_targetAnimator.animState);
        eventComplete = true;
    }

    /// <summary>
    /// Instantly performs the movement operation when the event is being skipped.
    /// </summary>
    public virtual void MoveInstantly()
    {
        // Sets the character the script acts on
        SetTarget();

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
        _targetAnimator.animState += "Idle";
        _targetAnimator.PlayScriptedAnimation(_targetAnimator.animState);

        // Ends event
        eventComplete = true;
    }

    /// <summary>
    /// Automatically sets a movement animation when no override is specified. Logic order follows
    /// a circle starting on the positive side of the X axis.
    /// </summary>
    protected void UpdateMoveAnimation()
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
    protected void SetTarget()
    {
        // Gets objects and components
        _targetCharacter = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];
        _targetAnimator = _targetCharacter.GetComponent<CharacterAnimator>();
    }

    #endregion
}
