/*=================================================================================================
 * FILE     : SnailTerrarium.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/23/25
 * UPDATED  : 9/23/25
 * 
 * DESC     : Tells the player how many snails they have and creates a list of snail names.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SnailTerrarium : NPCInteraction
{
    #region VARIABLES

    // Big Honkin' List of Snail Names
    // This array alone justifies my second monitor
    // Turns out this whole thing was unnecessary but I leave the half I had made as a monument to
    // my hubris.
    /*string[] _snailNames = { "Amelia Snailhart, ", "Snailid Snake, ", "Miles \"Snails\" Prower, ",
    "Snail Fast Snail Furious, ", "Snailexander Snailmilton, ", "SnailBob SnailPants, ",
    "SnaiIvagunner, ", "Snailcraft Snaily Snail: A SnailSnail Games Series, ", "Snailon Musk, ",
    "Snow Snailation, ", "Snail Capone, ", "Snaily Potter and the Snailosopher's Stone, ",
    "Edward Snailric, ", "Alphonse Snailric, ", "Snailexandria, ", "Snailt Shaker, ",
    "Snailphiroth,", "SnailWasTaken, ", "Luke Snailwalker, ", "Peter Campsnail III, ",
    "Snail Earnhardt, Jr., ", "SNAILROY JENKINS!, ", "Snas Thundersnail, ",
    "Snailtino's Pizza Rolls, ", "Hideo Snailjima, ", "Snailgeru Snailamoto, ",
    "Reggie Snails-aime, ", "Dwayne \"The Snail\" Johnson, ", "Snail Steve Harvey, ",
    "Snaildeline Celeste, ", "Snailbow Grease, ", "Popeye the Snailor Man, ",
    "Snailshi (Tax Fraud Committer), ", "Big Snailgus, ",
    "Snail doesn't even sound like a word anymore, ", "White Snaildow, ", "Call me Ishmasnail,",
    "Snail Steve Harvey 2, ", "Gasnailiel the Elder, ", "Neilsnail, ", "Snaillow Knight, ",
    "Snailuigi, ", "Muhammed Snaili, ", "Snailphrodite, ", "Asnailterasu, ",
    "The Wreck of the Edmund Fitsnailrald, ", "Madame la Snaillotine, ",
    "Snail the Size of Golf Balls, ", }; */

    // Link to the real Snail List
    string _nameFile;

    // Messages
    Dialogue _noSnailMsg = new Dialogue("It's a terratium with nothing living inside. It looks " +
        "like it would make a good habitat for snails");
    Dialogue _oneSnailMsg = new Dialogue("Your snail creeps around the terrarium. " +
        "She's called Amelia Snailhart. Snail for short.");
    Dialogue _oneSnailMsgPt2 = new Dialogue("She looks lonely.");
    Dialogue _fullMessage;

    // Components of text string
    string _introText;
    Dialogue _nameList;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _nameFile = Application.dataPath + "/SaveData/Collectibles/SnailNames.txt";
    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    #endregion

    #region STRING CONSTRUCTION

    /// <summary>
    /// Strings together the names of all collected snails.
    /// </summary>
    void NameSpider()
    {
        // Resets dialogue
        _NPCLines[0].dialogueBoxes.Clear();

        // Unique messages for low snail counts
        switch (GlobalVariableTracker.snailTotal)
        {
            case 0:
                _NPCLines[0].dialogueBoxes.Add(_noSnailMsg);
                return;
            case 1:
                _NPCLines[0].dialogueBoxes.Add(_oneSnailMsg);
                _NPCLines[0].dialogueBoxes.Add(_oneSnailMsgPt2);
                return;
        }

        // Sets introductory message
        if (GlobalVariableTracker.snailTotal >= 2 && GlobalVariableTracker.snailTotal <= 24)
        {
            _introText = "Your terrarium contains " + GlobalVariableTracker.snailTotal +
                " snails.";
            _NPCLines[0].dialogueBoxes.Add(new Dialogue(_introText));
        }

        // Loops through name file
    }

    #endregion
}
