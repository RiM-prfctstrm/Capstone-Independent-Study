/*=================================================================================================
 * FILE     : DetectObjects.cs
 * AUTHOR   : Peter "prfctstrm479"Campbell
 * CREATION : 6/5/23
 * UPDATED  : 10/1/24
 * 
 * DESC     : Gets props with in the player's interaction space and returns the nearest one
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObjects : MonoBehaviour
{
    /* VARS */

    // GameObjects
    List<GameObject> props = new List<GameObject>();
    GameObject _targetProp;
    public GameObject TargetProp => _targetProp;
    GameObject _player;

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
}
