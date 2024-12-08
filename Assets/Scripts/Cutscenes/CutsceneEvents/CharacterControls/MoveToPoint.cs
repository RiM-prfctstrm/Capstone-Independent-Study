/*=================================================================================================
 * FILE     : MoveToPoints.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 12/8/24
 * UPDATED  : 12/8/24
 * 
 * DESC     : Moves a character to a specified point.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Destination",
    menuName = "Cutscene/Character Controls/Movement Destination", order = 3)]
public class MoveToPoint : MoveByVectors
{
    #region VARIABLES

    // Inputs
    [SerializeField] Vector2 _endPoint;
    [SerializeField] float _threshold;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Moves the character towards a specified point
    /// </summary>
    protected override IEnumerator WaitForEventEnd()
    {
        // Skips event if target object is already close enough to point
        if (Vector2.Distance(_targetCharacter.transform.position, _endPoint) > _threshold)
        {
            // Sets variables to determine movement
            _movementVector = (_endPoint - (Vector2)_targetCharacter.transform.position).normalized;

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
            while (Mathf.Abs(Vector2.Distance(_targetCharacter.transform.position, _endPoint))
                >= .05f)
            {
                // Performs Movement
                _targetCharacter.transform.Translate(
                    _movementVector * _speed * Time.fixedDeltaTime);

                // Prevents Avengers Infinity Loop
                yield return new WaitForFixedUpdate();
            }

            // adjusts character to target postiion
            _targetCharacter.transform.position = _endPoint;
        }

        // Sets character to a finished animation and completes event
        _targetAnimator.PlayScriptedAnimation(_targetAnimator.SetAnimState());
        eventComplete = true;
    }

    /// <summary>
    /// Instantly performs the movement operation when the event is being skipped.
    /// </summary>
    public override void MoveInstantly()
    {
        // Sets the character the script acts on
        SetTarget();

        // Sets position and rotation
        _targetCharacter.transform.position = _endPoint;
        UpdateMoveAnimation();

        // Ends event
        eventComplete = true;
    }

    #endregion
}
