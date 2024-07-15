using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting_Slot : MonoBehaviour
{
    public Inventory_Items item;
    public CraftingRequirement[] required;
    private Inventory inventory;
    public Crafting_Management management;
    private bool craftable;
    void Start()
    {
        if (item.type == "Build")
        {
            Builds build = (Builds)item;
            required = build.crafting_cost;
            
        }
        else if (item.type == "Weapon")
        {
            Weapons weapon = (Weapons)item;
            
        }
        
        inventory = Inventory.Instance;

        craftable = true;
    }
    
    
    public void Craft()
    {
        foreach (CraftingRequirement temp in required)
        {
            Inventory_Items component = temp.item;
            int value = temp.amount;
            if (!(inventory.keys.Contains(component) && inventory.values[inventory.keys.IndexOf(component)] >= value))
            {
                craftable = false;
            }
        }   
        
        
        if(!craftable)
        {
            Debug.Log("Not enough materials!");
        }
        else
        {
            
            foreach (CraftingRequirement temp in required)
            {
                Inventory_Items component = temp.item;
                int value = temp.amount;
                
                inventory.Remove_Item(component,value);
                 
                
            }

            inventory.Add_Item(item);
        }
        

        craftable = true;
    }
    
    
    
}
