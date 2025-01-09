/*=================================================================================================
 * FILE     : ElevatorProgression.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/9/25
 * UPDATED  : 1/9/25
 * 
 * DESC     : Controls bespoke visual effects triggered by the space elevator rising. Specifically,
 *            changes backgrounds and warps the player to the destination chamber. Changes are
 *            triggered by an elevator background element obscuring everything behind it so the
 *            background can be swapped seamlessly.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorProgression : MonoBehaviour
{
    #region VARIABLES

    // Tracking variables
    public int polarity = 1; // 1 upward, -1 downward
    int _level = -1; //Tracks execution order

    // External References
    [SerializeField] Sprite[] _backGrounds;
    [SerializeField] SpriteRenderer _bgObject;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Ignores collisions with player
        /*Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
            PlayerController.playerController.GetComponent<Collider2D>());*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sets Current Elevator level
        _level += polarity;

        // Changes elevator state when the trigger object reaches this point
        switch (_level)
        {
            // Sends player to Ground Floor
            case -1:

                break;

            // Changes background, nothing else
            case <= 2:
                _bgObject.sprite = _backGrounds[_level];
                break;

            // Sends Player to Top Floor
            case 3:
                Debug.Log("Made it!");
                break;
        }

        /*if (collision.CompareTag(gameObject.tag))
        {

        }*/
    }

    #endregion
}
