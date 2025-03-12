/*=================================================================================================
 * FILE     : ParallaxScroll.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/9/25
 * UPDATED  : 3/12/25
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
    // Basic Movement
    public float maximum;
    public bool movingVertically;
    public float rate; // Negative for downwards or leftwards movement
    [SerializeField]
    bool _stopAtMaximum = false;
    public Vector2 tpDest;
    // Accelerates
    [SerializeField] bool _accelerates = false;
    [SerializeField] float _accelRate = 0;
    [SerializeField] int _maxSpeed;

    // Control conditions
    public bool inMotion;

    // Movement Vars
    Vector2 _newPos;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _newPos.y = transform.position.y;
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Determines which direction to move the object and begins specified movement
        if (inMotion)
        {
            if (movingVertically)
            {
                ParallaxVertical();
            }
            else
            {
                ParallaxHorizontal();
            }
        }

        // Accelerates movement rate
        if (_accelerates && Mathf.Abs(rate) <= _maxSpeed)
        {
            rate +=  _accelRate * Time.fixedDeltaTime * Mathf.Sign(rate);
        }
    }

    #endregion

    #region MOVEMENT CONTROLS
    
    /// <summary>
    /// Moves the object along X axis to maximum, then determines whether to continue movement
    /// </summary>
    void ParallaxHorizontal()
    {
        // Determines what to do at maximum
        if ((rate >= 0 && transform.position.x >= maximum) ||
            (rate < 0 && transform.position.x <= maximum))
        {
            DetermineMaximumBehavior();
        }
        // Moves object
        else
        {
            _newPos.x = transform.position.x + (rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }

    /// <summary>
    /// Moves the object along Y axis to maximum, then determines whether to continue movement
    /// </summary>
    void ParallaxVertical()
    {
        // Determines what to do at maximum
        if (( rate >= 0 && transform.position.y >= maximum) ||
            (rate < 0 && transform.position.y <= maximum))
        {
            DetermineMaximumBehavior();
        }
        // Moves object
        else
        {
            _newPos.y = transform.position.y + (rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }

    /// <summary>
    /// Determines whether to stop the object's parallax, or repeat it from the beginning
    /// </summary>
    void DetermineMaximumBehavior()
    {
        // Stops motion
        if (_stopAtMaximum)
        {
            inMotion = false;
        }
        // Repeats Parallax
        else
        {
            transform.position = tpDest;
        }
    }

    #endregion
}
