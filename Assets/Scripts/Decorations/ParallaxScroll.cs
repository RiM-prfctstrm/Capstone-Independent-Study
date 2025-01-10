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
    public float maximum;
    public bool movingVertically;
    public float rate; // Negative for downwards or leftwards movement
    public Vector2 tpDest;

    // Control conditions
    public bool inMotion;

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
    }

    #endregion

    #region MOVEMENT CONTROLS
    
    /// <summary>
    /// Moves the object along X axis to maximum, then teleports to tpDest
    /// </summary>
    void ParallaxHorizontal()
    {
        // Resets Position
        if ((rate >= 0 && transform.position.x >= maximum) ||
            (rate < 0 && transform.position.x <= maximum))
        {
            transform.position = tpDest;
        }
        // Moves object
        else
        {
            _newPos.x = transform.position.x + (rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }

    /// <summary>
    /// Moves the object along Y axis to maximum, then teleports to tpDest
    /// </summary>
    void ParallaxVertical()
    {
        // Resets Position
        if (( rate >= 0 && transform.position.y >= maximum) ||
            (rate < 0 && transform.position.y <= maximum))
        {
            transform.position = tpDest;
        }
        // Moves object
        else
        {
            _newPos.y = transform.position.y + (rate * Time.fixedDeltaTime);
            transform.position = _newPos;
        }
    }



    #endregion
}
