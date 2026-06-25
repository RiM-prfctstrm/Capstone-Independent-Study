/*=================================================================================================
 * FILE     : FishData.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/24/26
 * UPDATED  : 6/25/26
 * 
 * DESC     : Reads and Writes data for which fish have been collected. Also produces and stores
 *            randomized loot tables.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FishSaveManager
{
    #region VARIABLES

    // Data
    public static int gachaOrder;
    public static List<FishData> gachaTable = new List<FishData>();


    // IO Variables
    static string _basePath = Application.dataPath + "/SaveData/ProgressFiles/Slot";
    static string _tempTablePath = Application.dataPath + "/SaveData/Collectibles/FishTable.txt";
    static StreamReader _reader;
    static StreamWriter _writer;

    #endregion

    #region SAVING

    /// <summary>
    /// Saves order in gacha and currently collected fish.
    /// </summary>
    public static void SavePermanentData()
    {
        // Vars
        bool redundant = false;

        // Saves position in gacha
        SaveOrder();

        //Prevents redundancy
        using(_reader = new StreamReader(_basePath + 1 + "/Fish.txt"))
        {
            while (_reader.Peek() != -1)
            {
                if (gachaTable[gachaOrder - 1] != null)
                {
                    if (_reader.ReadLine() == gachaTable[gachaOrder - 1].fishName)
                    {
                        redundant = true;
                    }
                }
            }

            _reader.Dispose();
        }

        // Saves latest fish
        if (!redundant)
        {
            using(_writer = File.AppendText(_basePath + 1 + "/Fish.txt"))
            {
                if (gachaTable[gachaOrder - 1] != null)
                {
                    _writer.WriteLine(gachaTable[gachaOrder - 1].fishName);
                }

                _writer.Dispose();
            }
        }

    }

    /// <summary>
    /// Saves the current fish order by referencing the fish objects' positions in the daily loot
    /// table.
    /// </summary>
    /// <param name="refList">Daily loot table</param>
    public static void SaveCurrentTable(FishLootTable refList)
    {
        using(_writer = new StreamWriter(_tempTablePath))
        {
            // Translates fish objs into ints that can be used to reference their positions in a
            // list
            foreach (FishData fish in gachaTable)
            {
                _writer.WriteLine(refList.tableFish.IndexOf(fish));
            }

            _writer.Dispose();
        }
    }

    /// <summary>
    /// Saves the current order in the gacha table
    /// </summary>
    public static void SaveOrder()
    {
        // Saves position in gacha
        using(_writer = new StreamWriter(_basePath + 1 + "/Fish.txt"))
        {
            _writer.WriteLine(gachaOrder);
            _writer.Dispose();
        }
    }

    #endregion

    #region LOADING

    /// <summary>
    /// Resets previously generated loot table from save data
    /// </summary>
    /// <param name="refList">Table used to translate ints into fish</param>
    /// <returns>List of fish objects for couchfishing to cycle through</returns>
    public static List<FishData> LoadCurrentTable(FishLootTable refList)
    {
        // Vars
        List<FishData> tableConstructor = new List<FishData>();
        int listID;

        // Translates saved ints into fish objects
        using(_reader = new StreamReader(_tempTablePath))
        {
            while (_reader.Peek() != -1)
            {
                listID = int.Parse(_reader.ReadLine());

                // Creates blank table if referenced fish cannot be found
                if (refList.tableFish[listID] == null)
                {
                    return new List<FishData>();
                }

                tableConstructor.Add(refList.tableFish[listID]);
            }
            _reader.Dispose();
        }

        // Loads order in table
        LoadGachaOrder();

        // Sets loot table
        return tableConstructor;
    }

    /// <summary>
    /// Remembers the position in the gacha list
    /// </summary>
    public static void LoadGachaOrder()
    {
        using(_reader = new StreamReader(_basePath + 1 + "/Fish.txt"))
        {
            gachaOrder = int.Parse(_reader.ReadLine());
            _reader.Dispose();
        }
    }

    #endregion

    #region RANDOMIZATION

    /// <summary>
    /// Randomizes couchfishing table and saves it to disc
    /// </summary>
    /// <param name="SeedData">Loot Data to create Table from</param>
    public static void SetRandomTable(FishLootTable SeedData)
    {
        // Creates random table
        gachaTable = SeedGachaTable(SeedData);

        // Saves table
        SaveCurrentTable(SeedData);
    }

    /// <summary>
    /// Creates the list of fish that the player draws from when couchfishing
    /// </summary>
    /// <param name="SeedData">Loot Data to create Table from</param>
    /// <returns>List of fish objects for couchfishing to cycle through</returns>
    public static List<FishData> SeedGachaTable(FishLootTable SeedData)
    {
        // Vars
        List<FishData> tableConstructor = new List<FishData>();
        int tableSize = 0;
        int randomSlot;

        // Creates empty list
        foreach (int i in SeedData.dropRates)
        {
            tableSize += i;
        }
        for (int i = 0; i < tableSize; i++)
        {
            tableConstructor.Add(null);
        }

        // Fills list with fish data
        /// IMPORTANT: Since more common fish are supposed to be seeded first, but its more
        /// efficient to cycle through SeedData linearly than to sort first, ENTER S.O. DATA WITH
        /// MOST COMMON ENTRIES FIRST
        foreach (FishData fish in SeedData.tableFish)
        {
            // Loop spawns fish in random points in tableConstructor an amount of times equal to
            // fish's corresponding dropRate
            tableSize = SeedData.dropRates[SeedData.tableFish.IndexOf(fish)];
            for (int i = 0; i < tableSize; i++)
            {
                randomSlot = Random.Range(0, tableConstructor.Count);
                
                // Checks to make sure slot is not already occupied
                if (tableConstructor[randomSlot] != null)
                {
                    i--;
                }
                else
                {
                    // Assigns fish to slot
                    tableConstructor[randomSlot] = fish;
                }
            }
        }

        // Sets loot table
        return tableConstructor;
    }

    #endregion

    #region DEBUG

    /// <summary>
    /// Tests cycling through gacha table
    /// </summary>
    public static void LogFishName()
    {
        if (gachaOrder >= gachaTable.Count)
        {
            Debug.Log("No bites.");
            return;
        }

        Debug.Log(gachaTable[gachaOrder].name);
        gachaOrder++;
    }

    #endregion
}
