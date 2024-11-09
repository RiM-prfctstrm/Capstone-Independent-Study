/*=================================================================================================
 * FILE     : MoveByVectors.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/8/24
 * UPDATED  : 11/8/24
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
    [SerializeField] List<Vector2> _movements = new List<Vector2>();

    [SerializeField] bool _overrideMovementAnimation = false;
    [SerializeField] string _overrideAnimation;

    // Object Refs
    GameObject _targetCharacter;
    CharacterAnimator _targetAnimator;

    // Movement Parameters
    Vector2 _movementVector;
    Vector3 _targetPos;

    #endregion

    #region EVENT FUNCTIONALITY
    
    /// <summary>
    /// Sets up movement
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the character the script acts on
        _targetCharacter = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID];
        _targetAnimator = _targetCharacter.GetComponent<CharacterAnimator>();

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
            while (_targetCharacter.transform.position != _targetPos)
            {
                // Performs Movement
                _targetCharacter.transform.Translate(
                    _movementVector * _speed * Time.fixedDeltaTime);

                // Prevents Avengers Infinity Loop
                yield return new WaitForFixedUpdate();
            }
        }

        // Sets character to a finished animation and completes event
        _targetAnimator.PlayScriptedAnimation(_targetAnimator.SetAnimState());
        _eventComplete = true;
    }

    /// <summary>
    /// Automatically sets a movement animation when no override is specified. Logic order follows
    /// a circle starting on the positive side of the X axis
    /// </summary>
    void UpdateMoveAnimation()
    {
        // Directional animation for rightward movement
        if (Vector2.Angle(Vector2.right, _movementVector) <= 45 ||
            Vector2.Angle(Vector2.right, _movementVector) > 315)
        {
            _targetAnimator.facingDirection = 2;
            _targetAnimator.PlayScriptedAnimation("Right");
        }
        // Directional animation for upward movement
        else if (Vector2.Angle(Vector2.right, _movementVector) <= 135)
        {
            _targetAnimator.facingDirection = 3;
            _targetAnimator.PlayScriptedAnimation("Up");
        }
        // Directional animation for leftward movement
        else if (Vector2.Angle(Vector2.right, _movementVector) <= 225)
        {
            _targetAnimator.facingDirection = 1;
            _targetAnimator.PlayScriptedAnimation("Left");
        }
        // Directional animation for downward movement
        else if (Vector2.Angle(Vector2.right, _movementVector) <= 315)
        {
            _targetAnimator.facingDirection = 0;
            _targetAnimator.PlayScriptedAnimation("Down");
        }
    }

    #endregion
}
