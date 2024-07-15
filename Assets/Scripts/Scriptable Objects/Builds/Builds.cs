using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[System.Serializable]
public class CraftingRequirement
{
    public Inventory_Items item;
    public int amount;
}

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Builds")]
public class Builds : Inventory_Items
{

    [Header("Info")]
    public string tier;

    [Header("Crafting cost")] 
    public Inventory_Items[] Item_Names;
    public int[] Item_Values;
    public CraftingRequirement[] crafting_cost;
    
    [Header("Upgrading cost")]
    public Loots[] upgrading_cost;


}
