/*=================================================================================================
 * FILE     : FishLootTable.cs
 * AUTHOR   : Peter "prfctstrm479" Campbell
 * CREATION : 6/11/26
 * UPDATED  : 6/11/26
 * 
 * DESC     : ScriptableObject that stores drop information for loot tables. Since dicts aren't
 *            serializeable by default, fish and drop rates are stored in separate lists. The
 *            fish and rates with the same order in the lists correspond to each other.
=================================================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Couchfishing/Table", order = 0)]
public class FishLootTable : ScriptableObject
{
    #region VARIABLES

    // Data lists
    public List<FishData> tableFish;
    public List<int> dropRates;

    #endregion
}
