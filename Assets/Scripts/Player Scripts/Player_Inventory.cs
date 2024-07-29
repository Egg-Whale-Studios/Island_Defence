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
    [SerializeField] private KeyCode pick_up_key;
    [SerializeField] private KeyCode drop_key;
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
    private GameObject inventory_panel;
    private Inventory_Management management;
    private Inventory inventory;
    [NonSerialized] public bool inventory_open;
    private bool inventory_transition;
    private Vector3 inventory_start_pos;
    private Vector3 inventory_closed_pos;
    private Vector3 inventory_opened_pos;
    private Vector3 inventory_destination;

    /*private Vector3 posnew;
    private Vector3 posold;

    private float speed;*/

    public Inventory_Items chosen_item;
    private Player_Tools tools_script;
    private Player_Combat combat_script;
    private Player_Building building_script;




    IEnumerator Start()
    {
        
        inventory = Inventory.Instance;
        inventory_panel = GameObject.FindWithTag("Canvas").transform.GetChild(0).gameObject;
        inventory_start_pos = inventory_panel.transform.position;
        inventory_destination = inventory_start_pos;
        inventory_opened_pos = inventory_start_pos + new Vector3(-7.5f, 0, 0);
        
        inventory_open = false;
        management = inventory_panel.GetComponentInChildren<Inventory_Management>();
        tools_script = GetComponent<Player_Tools>();
        combat_script = GetComponent<Player_Combat>();
        building_script = GetComponent<Player_Building>();


        yield return new WaitUntil(() => Inventory.Instance != null);
        is_equipable();
        
    }


    private void Update()
    {

        if (!inventory_transition && Input.GetKeyDown(inventory_key))
        {

            inventory_transition = true;
            if (inventory_open == false)
            {
                inventory_destination = inventory_opened_pos;


            }
            else
            {
                inventory_destination = inventory_start_pos;


            }
            
            inventory_open_close();


        }

        inventory_panel.transform.position = Vector3.Lerp(inventory_panel.transform.position, inventory_destination, 0.02f);
       

        if (inventory_open)
        {
            move_in_inventory();
        }
        quick_pick();
        if (Input.GetKeyDown(pick_up_key)) Pick_Up_Item();
        if (Input.GetKeyDown(drop_key)) Drop_Loot();
    }

    

    private void inventory_open_close()
    {
        
        inventory_transition = false;
        inventory_open = !inventory_open;
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
            management.chosen_slot_index -= 4;
            is_equipable();
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(down) && management.chosen_slot_index < management.slots.Length - 4)
        {
            management.chosen_slot_index += 4;
            is_equipable();
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(left) && management.chosen_slot_index > 0)
        {
            management.chosen_slot_index -= 1;
            is_equipable();
            management.Change_Chosen();
        }

        if (Input.GetKeyDown(right) && management.chosen_slot_index < management.slots.Length - 1)
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
        if (management.chosen_slot_index < inventory.Find_Tab(management.tab).Item1.Count)
        {
            chosen_item = inventory.Find_Tab(management.tab).Item1[management.chosen_slot_index];


            if (chosen_item.type == "Tool")
            {
                Tools tool = (Tools)chosen_item;
                tools_script.Change_Tool(tool.animation_bool, tool.prefab, tool.handle_position);
                tools_script.equipped_tool = true;
            }
            else { tools_script.equipped_tool = false; }


            if (chosen_item.type == "Weapon")
            {
                Weapons weapon = (Weapons)chosen_item;
                combat_script.Change_Weapon(weapon.animation_bool, weapon.weapon_type, weapon.dual, weapon.prefab, weapon.handle_position);
                combat_script.equipped_weapon = true;
            }
            else
            {
                combat_script.equipped_weapon = false;
            }

            if (chosen_item.type == "Build")
            {
                Builds build = (Builds)chosen_item;
                building_script.change_build(build.prefab);
                building_script.equipped_build = true;

            }
            else
            {

                building_script.equipped_build = false;

            }
        }



    }
    private void Pick_Up_Item()
    {

        Collider[] loots = Physics.OverlapSphere(transform.position, 8, LayerMask.GetMask("World_Loot"));
        Debug.Log(loots.Length);
        if (loots.Length >= 1)
        {
            GameObject item = loots[0].gameObject;
            World_Loot item_script = item.GetComponent<World_Loot>();

            item_script.Pick_Item();
        }
    }

    private void Drop_Loot()
    {
        GameObject temp = Instantiate(inventory.Drop_Item(), transform.position + new Vector3(0, 3, 0), Quaternion.identity);
        temp.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, 2, 0)) * 10, ForceMode.Impulse);
        is_equipable();
    }

}
