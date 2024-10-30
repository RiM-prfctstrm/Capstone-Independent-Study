/*=================================================================================================
 * FILE     : EssentialPreserver.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 10/30/24
 * UPDATED  : 10/30/24
 * 
 * DESC     : Used to keep scene essentials when a new scene is loaded.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialPreserver : MonoBehaviour
{
    #region VARIABLES

    // Singleton used to create scene essentials
    public static EssentialPreserver instance;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Awake is called when the script instance is first loaded
    /// </summary>
    void Awake()
    {
        // Sets all commonly used singletons in this and in children
        instance = this;
        DialogueManager.dialogueManager = gameObject.GetComponentInChildren<DialogueManager>();

        // Keeps object and children around when new scenes are loaded
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
