/*=================================================================================================
 * FILE     : CameraConstraints.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 3/31/25
 * UPDATED  : 3/31/25
 * 
 * DESC     : Defines bounds beyond which the camera cannot travel. Used to mark edges of overworld
 *            maps.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConstraints : MonoBehaviour
{
    #region VARIABLES

    // Constraints (organized with mins first instead of alphabetically for user-friendliness)
    [SerializeField] float _minX;
    [SerializeField] float _minY;
    [SerializeField] float _maxX;
    [SerializeField] float _maxY;

    // Dependent Vectors
    Vector3 _constrainingVector;
    Vector2 _unconstrainedPos;

    // Misc
    bool _cameraSynced;
    Vector3 _defaultPos = new Vector3(0, 0, -10);

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Initializes camera position
        InitializeCameraPosition();
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    void FixedUpdate()
    {
        // Gets quick reference for camera position
        _unconstrainedPos = PlayerController.playerController.transform.position;
        _constrainingVector = PlayerController.playerController.transform.position;

        // Sets constrained position
        if (_unconstrainedPos.x < _minX || _unconstrainedPos.x > _maxX)
        {
            _constrainingVector.x = Mathf.Clamp(_unconstrainedPos.x, _minX, _maxX);
        }
        if (_unconstrainedPos.y < _minY || _unconstrainedPos.y > _maxY)
        {
            _constrainingVector.y = Mathf.Clamp(_unconstrainedPos.y, _minY, _maxY);
        }

        // Applies constrained position
        if (_constrainingVector.x != _unconstrainedPos.x || 
            _constrainingVector.y != _unconstrainedPos.y)
        {
            _cameraSynced = false;
            _constrainingVector.z = -10;
            PlayerController.playerCamera.transform.position = _constrainingVector;
        }
        // Corrects Camera shake
        else if (!_cameraSynced)
        {
            PlayerController.playerCamera.transform.localPosition = _defaultPos;
            _cameraSynced = true;
        }
    }

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Initializes the camera's position to the player's
    /// </summary>
    public void InitializeCameraPosition()
    {
        // Gets player position and sets vars
        _unconstrainedPos = PlayerController.playerController.transform.position;
        _constrainingVector = _unconstrainedPos;

        // Sets initial camera position
        PlayerController.playerCamera.transform.localPosition = _defaultPos;
        _cameraSynced = true;
    }

    #endregion
}
