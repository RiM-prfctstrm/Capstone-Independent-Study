/*=================================================================================================
 * FILE     : SceneTransition
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/29/24
 * UPDATED  : 12/8/24
 * 
 * DESC     : Switches scenes and sets variables to initialize that scene's state after transition.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition
{
    #region VARIABLES

    // External objects
    static PlayerController _player;
    static string _currentScene;

    #endregion

    #region SCENE TRANSITION METHODS

    /// <summary>
    /// Loads a new scene with the minimum information specified
    /// </summary>
    /// <param name="sceneName">The new scene to load</param>
    /// <param name="isIndoors">Determines how the method should determine whether the
    ///                         the player should start mounted in the new scene</param>
    /// <param name="startPos">The player's starting position in the new scene</param>
    public static void ChangeScene(string sceneName, bool isIndoors, Vector3 startPos)
    {
        // Gets objects for reference
        _currentScene = SceneManager.GetActiveScene().name;
        _player = PlayerController.playerController;

        // Plays scene transition sound
        _player.playerAudioSource.PlayOneShot(_player.sceneShift);

        // Warps the player without performing loads if the target scene is the current scene
        /*if (_currentScene == sceneName)
        {
            UtilityFunctions.WarpToPoint(startPos,
                _player.GetComponent<PlayerAnimator>().facingDirection);
            return;
        }*/

        // Resets list of Cutscene-controllable objects
        CutsceneManager.cutsceneManager.ResetCharacterList();

        // Loads new scene and initializes variables
        SceneManager.LoadScene(sceneName);
        
        // Determines whether the player should remain biking
        if (isIndoors)
        {
            _player.isWalking = true;
            _player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // Sets player's position
        _player.transform.position = startPos;

        // Cleans player's interaction list so it works in new scene
        _player.GetComponentInChildren<DetectObjects>().CleanUpInteractionList();

        // Unloads previous Scene
        SceneManager.UnloadSceneAsync(_currentScene);
        Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// Loads a new scene and sets the player's position and direction within it.
    /// </summary>
    /// <param name="sceneName">The new scene to load</param>
    /// <param name="isIndoors">Determines how the method should determine whether the
    ///                         the player should start mounted in the new scene</param>
    /// <param name="startPos">The player's starting position in the new scene</param>
    /// <param name="startDirection">The player's starting direction</param>
    public static void ChangeScene(string sceneName, bool isIndoors, Vector3 startPos, 
                                   int startDirection)
    {
        // Gets objects for reference
        _currentScene = SceneManager.GetActiveScene().name;
        _player = PlayerController.playerController;

        // Plays scene transition sound
        _player.playerAudioSource.PlayOneShot(_player.sceneShift);

        // Warps the player without performing loads if the target scene is the current scene
        /*if (_currentScene == sceneName)
        {
            UtilityFunctions.WarpToPoint(startPos, startDirection);
            return;
        }*/

        // Resets list of Cutscene-controllable objects
        CutsceneManager.cutsceneManager.ResetCharacterList();

        // Loads new scene and initializes variables
        SceneManager.LoadScene(sceneName);

        // Determines whether the player should remain biking
        if (isIndoors)
        {
            _player.isWalking = true;
            _player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        // Sets player's position and direction
        _player.transform.position = startPos;
        _player.GetComponent<PlayerAnimator>().facingDirection = startDirection;

        // Cleans player's interaction list so it works in new scene
        _player.GetComponentInChildren<DetectObjects>().CleanUpInteractionList();

        // Unloads previous Scene
        //SceneManager.UnloadSceneAsync(_currentScene);
        Resources.UnloadUnusedAssets();
    }

    #endregion
}
