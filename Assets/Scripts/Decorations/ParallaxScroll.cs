/*=================================================================================================
 * FILE     : ParallaxScroll.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/9/25
 * UPDATED  : 1/9/25
 * 
 * DESC     : Moves attached object at a set rate, teleporting it back so it can move continuously.
 *            Currently only rigged for vertical motion
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    #region VARIABLES

    // Parameters
    [SerializeField] int _maximum;
    [SerializeField] bool _movingVertically;
    [SerializeField] float _rate; // Negativew for downwars or leftwards movement
    [SerializeField] Vector2 _tpDest;

    // Control conditions
    [SerializeField] bool _inMotion;

    // Movement Vars
    Vector2 _newPos;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Determines which direction to move the object and begins specified movement
        if (_inMotion)
        {
            if (_movingVertically)
            {
                ParallaxVertical();
            }
            else
            {
                ParallaxHorizontal();
            }
        }
    }

    #endregion

    #region MOVEMENT CONTROLS

    /// <summary>
    /// Begins movement according to input parameters
    /// </summary>
    /// <param name="max">Maximum point for movement</param>
    /// <param name="speed">Speed at which object moves. Negative speed to move down/left</param>
    /// <param name="direction">Direction to move object, true=up, false=down</param>
    /// <param name="tpDest">Point to teleport object after reaching max</param>
    public void StartParallax(int max, float speed, bool direction, Vector2 tpDest)
    {
        // Sets parameters
        _maximum = max;
        _rate = speed;
        _tpDest = tpDest;
        _movingVertically = direction;
        _newPos = transform.position;

        // Begins motion
        _inMotion = true;
    }
    
    /// <summary>
    /// Moves the object along X axis to maximum, then teleports to tpDest
    /// </summary>
    void ParallaxHorizontal()
    {
        // Resets Position
        if ((_rate >= 0 && transform.position.x >= _maximum) ||
            (_rate < 0 && transform.position.x <= _maximum))
        {
            transform.position = _tpDest;
        }
        // Moves object
        else
        {
            _newPos.x = transform.position.x + (_rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }

    /// <summary>
    /// Moves the object along Y axis to maximum, then teleports to tpDest
    /// </summary>
    void ParallaxVertical()
    {
        // Resets Position
        if (( _rate >= 0 && transform.position.y >= _maximum) ||
            (_rate < 0 && transform.position.y <= _maximum))
        {
            transform.position = _tpDest;
        }
        // Moves object
        else
        {
            _newPos.y = transform.position.y + (_rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }



    #endregion
}
