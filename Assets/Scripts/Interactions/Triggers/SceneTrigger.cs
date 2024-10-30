/*=================================================================================================
 * FILE     : SceneTrigger
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/29/24
 * UPDATED  : 10/29/24
 * 
 * DESC     : Sends the player to a new scene when contacted.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    #region VARIABLES

    // Settings for new scene
    [SerializeField] string _sceneName;
    [SerializeField] bool _isIndoors;
    [SerializeField] Vector3 _position;
    [SerializeField] int _direction;

    // Controls for performing transition
    [SerializeField] bool _setsDirection = false;

    #endregion

    #region COLLISION LOGIC

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider2d other enters the trigger
    /// </summary>
    /// <param name="collision">Object that collides with this</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determines how to perform a scene transition
        if (collision.gameObject.tag == "Player")
        {
            if (!_setsDirection)
            {
                // Changes scene with automated player placement
                SceneTransition.ChangeScene(_sceneName, _isIndoors, _position);
            }
            else
            {
                // Changes scene with manual player placement
                SceneTransition.ChangeScene(_sceneName, _isIndoors, _position, _direction);
            }
        }
    }

    #endregion
}
