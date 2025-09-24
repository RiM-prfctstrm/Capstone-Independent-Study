/*=================================================================================================
 * FILE     : SnailTerrarium.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 9/23/25
 * UPDATED  : 9/24/25
 * 
 * DESC     : Tells the player how many snails they have and creates a list of snail names.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

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
    Dialogue _maxSnailMsg = new Dialogue("There's 101 snails in your terrarium. Any more, and " +
        "they'd collapse under the force of their own gravity and form a black hole.");
    Dialogue _noSnailMsg = new Dialogue("It's a terratium with nothing living inside. It looks " +
        "like it would make a good habitat for snails.");
    Dialogue _oneSnailMsg = new Dialogue("Your snail creeps around the terrarium. " +
        "She's called Amelia Snailhart. Snail for short.");
    Dialogue _oneSnailMsgPt2 = new Dialogue("She looks lonely.");
    Dialogue _fullMessage;

    // Components of text string
    string _introText;
    string _latestName;
    string _nameChunk;
    List<Dialogue> _nameDialogues = new List<Dialogue>();

    // Object refs
    [SerializeField] TextMeshProUGUI _testBox;

    #endregion

    #region UNIVERSAL EVENTS

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();

        //Initialize vars
        _nameFile = Application.dataPath + "/SaveData/Collectibles/SnailNames.txt";
    }

    #endregion

    #region INTERACTION FUNCTIONALITY

    /// <summary>
    /// Arranges display before creating event
    /// </summary>
    public override void OnInteractedWith()
    {
        NameSpider();

        base.OnInteractedWith();
    }

    #endregion

    #region STRING CONSTRUCTION

    /// <summary>
    /// Strings together the names of all collected snails.
    /// </summary>
    void NameSpider()
    {
        // Resets dialogue
        _NPCLines[0].dialogueBoxes.Clear();

        // Unique messages for specific snail counts
        switch (GlobalVariableTracker.snailTotal)
        {
            case 0:
                _NPCLines[0].dialogueBoxes.Add(_noSnailMsg);
                return;
            case 1:
                _NPCLines[0].dialogueBoxes.Add(_oneSnailMsg);
                _NPCLines[0].dialogueBoxes.Add(_oneSnailMsgPt2);
                return;
            case 101:
                _NPCLines[0].dialogueBoxes.Add(_maxSnailMsg);
                break;

        }

        // Sets introductory message
        if (GlobalVariableTracker.snailTotal >= 2 && GlobalVariableTracker.snailTotal <= 24)
        {
            _introText = "Your terrarium contains " + GlobalVariableTracker.snailTotal +
                " snails.";
            _NPCLines[0].dialogueBoxes.Add(new Dialogue(_introText));
        }
        else if (GlobalVariableTracker.snailTotal >= 25 && GlobalVariableTracker.snailTotal <= 49)
        {
            _introText = "Your terrarium is stuffed with " + GlobalVariableTracker.snailTotal +
                " snails.";
            _NPCLines[0].dialogueBoxes.Add(new Dialogue(_introText));
        }
        else if (GlobalVariableTracker.snailTotal >= 50 && GlobalVariableTracker.snailTotal <= 100)
        {
            _introText = "Your terrarium is overflowing with " + GlobalVariableTracker.snailTotal +
                " snails.";
            _NPCLines[0].dialogueBoxes.Add(new Dialogue(_introText));
        }

        // Loops through name file to create dialogue stings
        using (var reader = new StreamReader(_nameFile))
        {
            _testBox.text = "Their names are ";
            for (int i = 1; i <= GlobalVariableTracker.snailTotal; i++)
            {
                // Determines how to write latest name
                if (i == GlobalVariableTracker.snailTotal)
                {
                    _latestName = "and " + reader.ReadLine() + ".";
                }
                else
                {
                    _latestName = reader.ReadLine();
                }

                // Tests whether text fits in dialogue
                if (TestStringFit())
                {
                    // Adds to current name chunk
                    _nameChunk = _testBox.text;
                }
                else
                {
                    // Adds name chunk to dialogue list
                    _nameDialogues.Add(new Dialogue(_nameChunk));

                    // Resets text info to create next chunk
                    _testBox.text = _latestName + ", ";
                }

                // Finishes name list
                if (i == GlobalVariableTracker.snailTotal)
                {
                    _nameDialogues.Add(new Dialogue(_nameChunk));
                }
            }
        }

        // Adds messages with names
        foreach(Dialogue j in _nameDialogues)
        {
            _NPCLines[0].dialogueBoxes.Add(j);
        }

        // Adds final message
        _NPCLines[0].dialogueBoxes.Add(new Dialogue("All of them are called snail for short."));
        if (GlobalVariableTracker.snailTotal == 101)
        {
            _NPCLines[0].dialogueBoxes.Add(new Dialogue("Yes, even Tadd."));
        }
    }

    /// <summary>
    /// Tests whether adding a new snail name would fit inside dialogue box
    /// </summary>
    /// <returns>True if can fit, otherwise false</returns>
    bool TestStringFit()
    {
        // updates test box
        _testBox.text += _latestName + ",";
        _testBox.ForceMeshUpdate();

        // Performs check
        if (_testBox.textInfo.lineCount <= 3)
        {
            _testBox.text += " ";
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
