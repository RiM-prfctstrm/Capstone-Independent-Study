/*=================================================================================================
 * FILE     : CutsceneSceneTransition.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/7/24
 * UPDATED  : 1/21/25
 * 
 * DESC     : Controls scene transitions in the middle of a cutscene, allowing a single script to
 *            continue across multiple scenes.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scripted Transition", menuName = "Cutscene/Scripted Scene Transition",
                 order = 3)]
public class CutsceneSceneTransition : CutsceneEvent
{
    #region VARIABLES

    // Inputs
    [SerializeField] string _scene;
    [SerializeField] bool _isIndoors = true;
    [SerializeField] Vector3 _playerPos;
    [SerializeField] int _playerDirection;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Loads a new scene and positions the player accordingly
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Loads new scene
        CutsceneManager.cutsceneManager.StartCoroutine(
            SceneTransition.TransitionScene(_scene, _isIndoors, _playerPos, _playerDirection));

        // Makes sure player faces the right direction on loading
        PlayerController.playerController.GetComponent<PlayerAnimator>().PlayScriptedAnimation(
            PlayerController.playerController.GetComponent<PlayerAnimator>().SetAnimState());

        // Prepares the script to wait for its completion signal
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Makes sure the new scene is fully loaded before proceeding
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator WaitForEventEnd()
    {
        // Load wait is preserved to ensure completion isn't signalled before the transition
        // coroutine signals the transition has begun.
        //yield return new WaitUntil(() => SceneTransition.inTransition == false);
        while (SceneTransition.inTransition || SceneManager.GetActiveScene().name != _scene)
        {
            yield return new WaitForFixedUpdate();
        }

        eventComplete = true;
    }

    #endregion
}
