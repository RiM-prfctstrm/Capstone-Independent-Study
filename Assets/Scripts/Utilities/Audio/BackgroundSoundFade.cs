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

    // Parameters
    float _maxVolume;
    float _timingCoefficient;

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
        _maxVolume = _attachedAS.volume;
        _timingCoefficient = 1 / _maxVolume;
        _attachedAS.volume = 0;

        // Stops automatic object destruction when scene is loaded
        DontDestroyOnLoad(gameObject);

        // Fades in sound
        StartCoroutine(FadeSoundIn());
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
            StartCoroutine(FadeSoundOut());
        }
    }

    #endregion

    #region FADE CONTROLS

    /// <summary>
    /// Fades in the sound's volume
    /// </summary>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeSoundIn()
    {
        // keeps script from fading both ways at the same time
        _inFade = true;

        // Incrementally lowers volume
        while (_attachedAS.volume < _maxVolume)
        {
            _attachedAS.volume += (.5f * Time.fixedDeltaTime) / _timingCoefficient;
            yield return new WaitForFixedUpdate();
        }
        
        // enable fading out
        _inFade = false;
    }

    /// <summary>
    /// Fades out the sound's volume
    /// </summary>
    /// <returns>Framerate delay for fading</returns>
    IEnumerator FadeSoundOut()
    {
        // Incrementally lowers volume
        while (_attachedAS.volume > 0)
        {
            _attachedAS.volume -= (.5f * Time.fixedDeltaTime) / _timingCoefficient;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }

    #endregion
}
