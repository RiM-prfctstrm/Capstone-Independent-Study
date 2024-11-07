/*=================================================================================================
 * FILE     : CutsceneSceneTransition.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 11/7/24
 * UPDATED  : 11/7/24
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
        SceneTransition.ChangeScene(_scene, _isIndoors, _playerPos, _playerDirection);

        // Prepares the script to wait for its completion signal
        CutsceneManager.cutsceneManager.StartCoroutine(WaitForEventEnd());
    }

    /// <summary>
    /// Makes sure the new scene is fully loaded before proceeding
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator WaitForEventEnd()
    {
        yield return new WaitUntil(() => SceneManager.GetSceneByName(_scene).isLoaded == true);

        _eventComplete = true;
    }

    #endregion
}
