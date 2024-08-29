/*=================================================================================================
 * FILE     : PlayerController.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/27/24
 * UPDATED  : 8/27/24
 * 
 * DESC     : Controls the player character
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* VARS */

    //Components
    SpriteRenderer _playerRenderer;
    Animator _playerAnimator;
    Rigidbody2D _rb2D;

    //Parameters
    [SerializeField] float _walkSpeed;
    [SerializeField] float _maxBikeSpeed;
    [SerializeField] float _accelRate;
    [SerializeField] float _decelRate;
    [SerializeField] bool _isWalking;

    //

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets components
        _playerAnimator = GetComponent<Animator>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Controls which kind of movement to perform.
        if (_isWalking)
        {

        }
        else
        {

        }
    }
}
