/*=================================================================================================
 * FILE     : SnailPickUp.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/17/25
 * UPDATED  : 3/27/26
 * 
 * DESC     : Controls how Snails are saved and inventoried when picked up, as well as snail
 *            movement
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SnailPickUp : Collectible
{
    #region VARIABLES

    // Movement
    int _direction = 1;
    float _moveRate;
    [SerializeField] float _speed;
    [SerializeField] bool _vertical;

    // Collection Effects
    [SerializeField] MusicTrack _pickUpJingle;
    [SerializeField] DialogueEvent _pickUpText;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    private void Awake()
    {
        // Checks whether snail has already been collected and destroys if so
        if (SnailSaveManager.collectedSnails.Contains(collectibleID))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits vars
        _moveRate = _speed * _direction * Time.deltaTime;
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Performs movement
        MoveSnail();
    }

    #endregion

    #region COLLISION CONTROLS

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Changes movement direction
        _direction = -_direction;
        _moveRate = _speed * _direction * Time.deltaTime;

        // Changes sprite orientation
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    #endregion

    #region MOVEMENT FUNCTIONALITY

    /// <summary>
    /// Moves the snail
    /// </summary>
    void MoveSnail()
    {
        // Sets relocation position
        if (!_vertical)
        {
            transform.Translate(new Vector2(_moveRate, 0));
        }
        else
        {
            transform.Translate(new Vector2(0, _moveRate));
        }
    }

    #endregion

    #region COLLECTION FUNCTIONALITY

    /// <summary>
    /// Adds snail to player's inventory and marks corresponding SO as collected.
    /// </summary>
    protected override void OnPickUp()
    {
        base.OnPickUp();

        // Adds to total
        GlobalVariableTracker.snailTotal++;

        // Updates Save Data
        SnailSaveManager.UpdateTempSave(collectibleID);

        // Plays effects
        PlayerController.playerController.StartCoroutine(
            PickUpEffects(GetComponent<SpriteRenderer>().sprite, _pickUpJingle, _pickUpText));

        // Awards Steam Achievement
        if (GlobalVariableTracker.snailTotal == 1)
        {
            AchievementManager.AwardAchievement("ACH_FIRST_SNAIL");
        }

    }

    /// <summary>
    /// Plays pickup sound
    /// </summary>
    void PlayPickUpSound()
    {
        PlayerController.playerController.playerAudioSource.PlayOneShot(_pickUpSound);
    }

    #endregion
}
