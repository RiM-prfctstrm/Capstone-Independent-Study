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
    public static int polarity = 1; // 1 upward, -1 downward
    public static int level = -1; //Tracks execution order

    // External References
    [SerializeField] Sprite[] _backGrounds;
    [SerializeField] GameObject[] _bgObjects;
    [SerializeField] SpriteRenderer _bgSprite;
    Vector2[] _startPositions = new Vector2[3];

    // References
    Transform _player;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    ///  Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits vars
        _player = PlayerController.playerController.transform;

        // Gets initial object positions
        int j = 0;
        foreach(GameObject i in _bgObjects)
        {
            _startPositions[j] = i.transform.position;

            j++;
        }
    }

    #endregion

    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sets Current Elevator level
        level += polarity;
        Debug.Log(level);

        // Changes elevator state when the trigger object reaches this point
        switch (level)
        {
            // Sends player to Ground Floor
            case -1:
                StartCoroutine(ExitElevator());
                break;

            // Changes background, nothing else. Would love to know how to write this with fewer
            // Cases
            case 0:
            case 1:
            case 2:
                _bgSprite.sprite = _backGrounds[level];
                break;

            // Sends Player to Top Floor
            case 3:
                level--;
                StartCoroutine(ExitElevator());
                break;
        }
    }

    #endregion

    #region EXIT FUNCTIONALITY

    /// <summary>
    /// Sends player to the elevator endpoint and resets the shaft
    /// </summary>
    /// <returns>Delay for fade</returns>
    IEnumerator ExitElevator()
    {
        // Fades out and delays
        ScreenEffects.fadingOut = true;
        yield return new WaitUntil(() => ScreenEffects.fadingOut == false);

        // Moves Player
        _player.position = new Vector2(_player.position.x + (50 * polarity), _player.position.y);

        // Resets set objects
        int j = 0;
        foreach (GameObject i in _bgObjects)
        {
            // Resets object
            i.GetComponent<ParallaxScroll>().rate = 0;
            i.transform.position = _startPositions[j];

            // Iterates loop
            j++;
        }

        // Fades back in
        ScreenEffects.fadingIn = true;
    }

    #endregion
}
