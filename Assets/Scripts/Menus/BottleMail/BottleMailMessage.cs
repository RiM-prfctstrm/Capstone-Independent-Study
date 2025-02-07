/*=================================================================================================
 * FILE     : Dialogue.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 1/16/25
 * UPDATED  : 1/16/25
 * 
 * DESC     : Container for data used for BottleMail messages.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BottleMailMessage : ScriptableObject
{
    #region VARIABLES

    // Vars here ordered by order you'd see in the game, not alphabetically
    // Message Data
    [SerializeField] string _msgSubject;
    [SerializeField] string _msgSender;
    [SerializeField] string _msgReceivers;
    [SerializeField] [TextArea(3, 50)] string _msgContents;

    // Getters
    public string msgSubject => _msgSubject;
    public string msgSender => _msgSender;
    public string msgReceivers => _msgReceivers;
    public string msgContents => _msgContents;

    #endregion
}
