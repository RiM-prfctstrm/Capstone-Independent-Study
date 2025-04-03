/*=================================================================================================
 * FILE     : BootManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 4/2/25
 * 
 * DESC     : Performs functionality that only occurs when the game is first started.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    #region VARIABLES

    // Prefab to load if none exists
    [SerializeField] GameObject _essentials;

    // Objects
    GameObject _player;

    // Parameters for correctly loading player
    [SerializeField] Vector3 _startPos;
    [SerializeField] int _startDirection;

    // List of NPCs in scene that can be used in cutscenes
    [Tooltip("Objects that can be used in a cutscene. " +
        "When referring to one, its ID is its position in the list + 1.")]
    [SerializeField] List<GameObject> _cutsceneObjs = new List<GameObject>();

    // Level-specific music
    //SerializeField] bool _hasLocalMusic = false;
    [SerializeField] AudioClip _localMusic;
    [SerializeField] bool _noLoop = false;
    [SerializeField] bool _noMusic = false;

    // Map parameters
    [SerializeField] bool _indoorScene = true;
    public bool indoorScene => _indoorScene;
    [SerializeField] Vector2 _mapOffset;
    public Vector3 mapOffset => _mapOffset;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Creates Scene Essentials if none exist
        if (EssentialPreserver.instance == null)
        {
            Instantiate(_essentials);

            // Initializes Player
            _player = PlayerController.playerController.gameObject;
            _player.transform.position = _startPos;
            _player.GetComponent<PlayerAnimator>().facingDirection = _startDirection;
            CutsceneManager.cutsceneManager.cutsceneObjects.Add(_player);
        }

        // Adds Cutscene-capable NPCs to the list of potential actors
        CutsceneManager.cutsceneManager.cutsceneObjects.AddRange(_cutsceneObjs);
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Plays the local music
        if (_noMusic)
        {
            StartCoroutine(MusicManager.musicManager.FadeOutSong());
        }
        else
        {
            MusicManager.musicManager.SwapSong(_localMusic, true);
            MusicManager.musicManager.SetLooping(!_noLoop);
        }

        // Tells PlayerController whether you can bike indoors
        PlayerController.playerController.inBikeableArea = !_indoorScene;
    }

    #endregion
}
