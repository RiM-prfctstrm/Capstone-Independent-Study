/*=================================================================================================
 * FILE     : SetSortingLayer.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 2/5/26
 * UPDATED  : 2/5/26
 * 
 * DESC     : Specifies render layer for target gameobject
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Set Render Layer",
    menuName = "Cutscene/Object Manipulation/Render Layer", order = 2)]
public class SetSortingLayer : CutsceneEvent
{
    #region VARIABLES

    // Input
    [SerializeField] int _targetID;
    [SerializeField] int _layerID;
    [SerializeField] int _layerOrder;

    // Object Refs
    SpriteRenderer _target;

    #endregion

    #region EVENT FUNCTIONALITY

    /// <summary>
    /// Sets target's visibility
    /// </summary>
    public override void PlayEventFunction()
    {
        base.PlayEventFunction();

        // Sets the object the script acts on
        _target = CutsceneManager.cutsceneManager.cutsceneObjects[_targetID]
            .GetComponent<SpriteRenderer>();

        // Sets Render Layer
        _target.sortingLayerID = _layerID;
        _target.sortingOrder = _layerOrder;

        // Signals completion
        eventComplete = true;
    }

    #endregion
}
