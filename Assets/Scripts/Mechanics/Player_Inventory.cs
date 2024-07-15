using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [Header("Debug Keys")]

    [Header("Inventory Keys")] 
    [SerializeField] private KeyCode inventory_key;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    
    [SerializeField] private KeyCode key1;
    [SerializeField] private KeyCode key2;
    [SerializeField] private KeyCode key3;
    [SerializeField] private KeyCode key4;
    [SerializeField] private KeyCode key5;
    
    
    
    [Header("Required Thingies")]
    public GameObject inventory_panel;
    public Inventory_Management management;
    public Inventory inventory;
    public Loots stone;
    public Loots wood;
    public bool inventory_open;
    
    public Inventory_Items chosen_item;
    private Player_Tools tools_script;
    private Player_Combat combat_script;
    private Player_Building building_script;
    
    
    IEnumerator Start()
    {
        inventory = Inventory.Instance;
        tools_script = GetComponent<Player_Tools>();
        combat_script = GetComponent<Player_Combat>();
        building_script = GetComponent<Player_Building>();
        
        yield return new WaitUntil(() => Inventory.Instance != null); 
        inventory_panel.SetActive(true);
        inventory_panel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        is_equipable();
        
    }
    

    private void Update()
    {
        
        inventory_open_close();
        
        if (inventory_open)
        {
            move_in_inventory();
        }
        quick_pick();
    }

    
    private void inventory_open_close()
    {
        if (Input.GetKeyDown(inventory_key))
        {
            if (inventory_panel.activeSelf == false)
            {
                inventory_panel.SetActive(true);
                inventory_open = true;
            }
            else
            {
                inventory_panel.SetActive(false);
                inventory_open = false;
            }
        }
        
    }

    private void quick_pick()
    {
        if (Input.GetKeyDown(key1))
        {
            management.chosen_slot_index = 0;
            management.Change_Chosen();
            is_equipable();
        }
        if (Input.GetKeyDown(key2))
        {
            management.chosen_slot_index = 1;
            management.Change_Chosen();
            is_equipable();
        }
        if (Input.GetKeyDown(key3))
        {
            management.chosen_slot_index = 2;
            management.Change_Chosen();
            is_equipable();
        }
        if (Input.GetKeyDown(key4))
        {
            management.chosen_slot_index = 3;
            management.Change_Chosen();
            is_equipable();
        }

        if (Input.GetKeyDown(key5))
        {
            management.chosen_slot_index = 4;
            management.Change_Chosen();
            is_equipable();
        }
        
    }

    private void move_in_inventory()
    {
        if (Input.GetKeyDown(up) && management.chosen_slot_index > 4)
        {
            management.chosen_slot_index -= 5;
            is_equipable();
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(down) && management.chosen_slot_index < management.slots.Length-5)
        {
            management.chosen_slot_index += 5;
            is_equipable();
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(left) && management.chosen_slot_index > 0)
        {
            management.chosen_slot_index -= 1;
            is_equipable();
            management.Change_Chosen();
        }
        
        if (Input.GetKeyDown(right) && management.chosen_slot_index < management.slots.Length-1)
        {
            management.chosen_slot_index += 1;
            is_equipable();
            management.Change_Chosen();
        }
        
    }
    
    public void is_equipable()
    {
        combat_script.Unequip_Weapon();
        tools_script.Unequip_Tool();
        building_script.Unequip_Build();
        if (management.chosen_slot_index < inventory.keys.Count)
        {
            chosen_item = inventory.keys[management.chosen_slot_index];
            
            
            if (chosen_item.type == "Tool")
            {
                Tools tool = (Tools) chosen_item;
                tools_script.Change_Tool(tool.animation_bool, tool.prefab, tool.handle_position);
                tools_script.equipped_tool = true;
            }
            else { tools_script.equipped_tool = false; }
            
            
            if (chosen_item.type == "Weapon")
            {
                Weapons weapon = (Weapons) chosen_item;
                combat_script.Change_Weapon(weapon.animation_bool ,weapon.damage,weapon.dual, weapon.prefab, weapon.handle_position);
                combat_script.equipped_weapon = true;
            }
            else
            {
                combat_script.equipped_weapon = false;
            }

            if (chosen_item.type == "Build")
            {
                Builds build = (Builds) chosen_item;
                building_script.change_build(build.prefab);
                building_script.equipped_build = true;
                
            }
            else
            {
                
                building_script.equipped_build = false;
                
            }
        }
        
        
    }
}
