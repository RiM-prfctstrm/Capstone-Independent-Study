/*=================================================================================================
 * FILE     : SceneTransition
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/29/24
 * UPDATED  : 1/8/25
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

    // Signals
    static bool _inTransition = false;
    public static bool inTransition => _inTransition;

    #endregion

    #region TRANSITION TIMELINE

    /// <summary>
    /// Controls the order of methods used in scene transitions, performin each step at a time
    /// </summary>
    /// <param name="sceneName">The new scene to load</param>
    /// <param name="isIndoors">Determines how the method should determine whether the
    ///                         the player should start mounted in the new scene</param>
    /// <param name="startPos">The player's starting position in the new scene</param>
    /// <param name="startDirection">The player's starting direction</param>
    /// <returns>Delays until each function is done</returns>
    public static IEnumerator TransitionScene(string sceneName, bool isIndoors, Vector3 startPos,
                                              int startDirection)
    {
        // Prepares transition
        PrepareForTransition();

        // Plays scene transition sound
        _player.playerAudioSource.PlayOneShot(_player.sceneShift);

        // Fades out
        ScreenEffects.fadingOut = true;
        yield return new WaitUntil(() => ScreenEffects.fadingOut == false);

        // Used when the player is meant to keep current direction
        if (startDirection >= 4)
        {
            startDirection = _player.GetComponent<PlayerAnimator>().facingDirection;
        }

        // Changes Scene
        ChangeScene(sceneName, isIndoors, startPos, startDirection);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded == true);

        // Fades back in
        ScreenEffects.fadingIn = true;
        yield return new WaitUntil(() => ScreenEffects.fadingIn == false);

        // Reenables Movement
        if (!CutsceneManager.inCutscene)
        {
            PlayerController.playerController.TogglePlayerInput();
        }

        // Signals that transition is complete
        _inTransition = false;
    }

    #endregion

    #region SCENE TRANSITION METHODS

    /// <summary>
    /// Sets up game state for scene transitioning
    /// </summary>
    static void PrepareForTransition()
    {
        // Gets objects for reference
        _currentScene = SceneManager.GetActiveScene().name;
        _player = PlayerController.playerController;

        // Resets list of Cutscene-controllable objects
        CutsceneManager.cutsceneManager.ResetCharacterList();

        // Signals that a scene transition is in effect
        _inTransition = true;

        // Stops player from moving
        if (!CutsceneManager.inCutscene)
        {
            PlayerController.playerController.TogglePlayerInput();
        }
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
