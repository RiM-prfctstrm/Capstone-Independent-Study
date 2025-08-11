/*=================================================================================================
 * FILE     : BackgroundSoundFade.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 8/11/25
 * UPDATED  : 8/11/25
 * 
 * DESC     : Fades out background sounds after leaving a scene.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSoundFade : MonoBehaviour
{
    #region VARIABLES

    // Object refs
    AudioSource _attachedAS;
    string _initialSceneName;

    // Control Switches
    bool _inFade = false;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Inits vars
        _attachedAS = GetComponent<AudioSource>();
        _initialSceneName = SceneManager.GetActiveScene().name;

        // Stops automatic object destruction when scene is loaded
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    ///  Update is called once per frame
    /// </summary>
    void Update()
    {
        // Detects scene change
        if (_initialSceneName != SceneManager.GetActiveScene().name && !_inFade)
        {
            _inFade = true;
            StartCoroutine(FadeSound());
        }
    }

    #endregion

    #region FADE CONTROLS

    /// <summary>
    /// Fades out the sound's volume
    /// </summary>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeSound()
    {
        // Incrementally lowers volume
        while (_attachedAS.volume > 0)
        {
            _attachedAS.volume -= .5f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    #endregion
}
