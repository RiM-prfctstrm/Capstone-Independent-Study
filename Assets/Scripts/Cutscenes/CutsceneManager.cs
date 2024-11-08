/*=================================================================================================
 * FILE     : CutsceneManager.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1X/X/24
 * UPDATED  : 11/8/24
 * 
 * DESC     : Controls the progression of scripted events.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    #region VARIABLES

    // Manager Singleton
    public static CutsceneManager cutsceneManager;

    // Object References, typically to allow event SOs to refer to objects in the scene
    [SerializeField] GameObject _UISpace;
    public GameObject UISpace => _UISpace;
    // A list of characters that can be used in cutscenes. This keeps the cutscene functions
    // uncluttered by NPCs that aren't used by them, making the object references easier to keep
    // track of.
    public List<GameObject> cutsceneObjects = new List<GameObject>();

    // Signals
    static bool _inCutscene = false;
    public static bool inCutscene => _inCutscene;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    #region CUTSCENE PROGRESS

    /// <summary>
    /// Puts the game in a cutscene state, disabling player input and setting up for cutscene
    /// functions
    /// </summary>
    public void StartCutscene(Cutscene cutscene)
    {
        // Sets up cutscene mode
        _inCutscene = true;
        PlayerController.playerController.TogglePlayerInput();

        // Plays cutscene
        StartCoroutine(PlayCutscene(cutscene));
    }

    /// <summary>
    /// Progresses a cutscene along a linear series of events with varying functionality, including
    /// but not limited to, dialogue events, animating and moving characters, and image
    /// manipulation
    /// </summary>
    /// <returns>Delay Between</returns>
    IEnumerator PlayCutscene(Cutscene cutscene)
    {   
        // Plays each event in sequence
        foreach (CutsceneEvent i in cutscene.cutsceneScript)
        {
            i.PlayEventFunction();
            yield return new WaitUntil(() => i.eventComplete == true);
        }

        // Ends cutscene
        EndCutscene();
    }

    /// <summary>
    /// Returns Game to normal gameplay State
    /// </summary>
    void EndCutscene()
    {
        // Deactivates cutscene mode
        _inCutscene = false;
        PlayerController.playerController.TogglePlayerInput();
    }

    #endregion

    #region DATA MANAGEMENT

    /// <summary>
    /// Resets the list of NPCs used in cutscenes. Typically occurs during scene transitions.
    /// </summary>
    public void ResetCharacterList()
    {
        // Removes all objects from the list
        cutsceneObjects.Clear();
        cutsceneObjects.TrimExcess();

        // Re-adds the player;
        cutsceneObjects.Add(PlayerController.playerController.gameObject);
    }

    #endregion
}
