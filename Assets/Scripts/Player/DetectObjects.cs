/*=================================================================================================
 * FILE     : DetectObjects.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/5/23
 * UPDATED  : 10/11/24
 * 
 * DESC     : Gets props with in the player's interaction space and returns the nearest one
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    #region VARIABLES

    // GameObjects
    List<GameObject> _interactables = new List<GameObject>();
    GameObject _player;
    GameObject _targetProp;
    public GameObject TargetProp => _targetProp;

    // Position vectors
    Vector2 _playerPos;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initializes objects
        _player = transform.parent.gameObject;
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Variable updates
        _playerPos = transform.parent.transform.position;

        // Ensures list is sorted
        if (_interactables.Count > 1)
        {
            SortTarget();
        }
        // If no sorting is needed, determines target prop
        else if (_interactables.Count == 1)
        {
            _targetProp = _interactables[0];
        }
        else
        {
            _targetProp = null;
        }
    }

    #endregion

    #region OBJECT DETECTION AND SORTING

    /// <summary>
    /// Called when the collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object this touches</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Adds prop objects to list
        if (collision.gameObject.tag == "Interactable")
        {
            _interactables.Add(collision.gameObject);
        }
    }

    /// <summary>
    /// Called when the collider2d other exits the trigger
    /// </summary>
    /// <param name="collision">Object this touches</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Removes any objects that were in the list that leave
        if (_interactables.Contains(collision.gameObject))
        {
            _interactables.Remove(collision.gameObject);
        }
    }

    /// <summary>
    /// Controls which direction the player can interact.
    /// </summary>
    public void SetInteractionDirection(int direction)
    {
        switch (direction)
        {
            case 0:
                transform.position = (Vector2)_player.transform.position + (Vector2.down * 2);
                break;

            case 1:
                transform.position = (Vector2)_player.transform.position + (Vector2.left * 1.5f)
                    + (Vector2.down / 2);
                break;

            case 2:
                transform.position = (Vector2)_player.transform.position + (Vector2.right * 1.5f)
                    + (Vector2.down / 2);
                break;

            case 3:
                transform.position = (Vector2)_player.transform.position + (Vector2.up);
                break;
        }
    }

    /// <summary>
    /// Finds the closest object in props, and singles it out
    /// </summary>
    void SortTarget()
    {
        // Vars
        float distance;
        float minDistance = 10;

        // Sorts objects by length and returns nearest one
        foreach (GameObject i in _interactables)
        {
            distance = Vector2.Distance(_playerPos, i.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _targetProp = i;
            }
        }
    }

    #endregion
}
