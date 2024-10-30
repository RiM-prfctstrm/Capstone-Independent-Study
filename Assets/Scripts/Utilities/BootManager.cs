/*=================================================================================================
 * FILE     : EssentialPreserver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 10/30/24
 * 
 * DESC     : Performs functionality that only occurs when the game is first started.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    #region VARIABLES

    // Prefab to load if none exists
    [SerializeField] GameObject _essentials;

    // Objects
    GameObject _player;

    // Parameters for correctly loading player
    [SerializeField] Vector3 _startPos;
    [SerializeField] int _startDirection;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Init Vars
        _player = _essentials.GetComponentInChildren<PlayerController>().gameObject;

        // Creates Scene Essentials if none exist
        if (EssentialPreserver.instance == null)
        {
            Instantiate(_essentials);

            // Initializes Player
            _player.transform.position = _startPos;
            _player.GetComponent<PlayerAnimator>().facingDirection = _startDirection;
        }
    }

    #endregion
}
