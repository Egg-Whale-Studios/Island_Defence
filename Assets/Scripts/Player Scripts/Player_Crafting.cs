using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Crafting : MonoBehaviour
{ 
    [Header("Crafting Keys")]
    [SerializeField] private KeyCode craft_key;
    [SerializeField] private KeyCode craft_panel_key;
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;

    [Header("Required Thingies")] 
    public Crafting_Management management;
    private Player_Inventory player_inventory;
    public bool crafting_open;
    private GameObject crafting_panel;
    private bool craft_transition;
    private Vector3 craft_start_pos;
    private Vector3 craft_opened_pos;
    private Vector3 craft_destination;
    void Start()
    {
        player_inventory = GetComponent<Player_Inventory>();
        management = GameObject.FindWithTag("Canvas").GetComponentInChildren<Crafting_Management>();
        crafting_panel = GameObject.FindWithTag("Canvas").transform.GetChild(1).gameObject;
        craft_start_pos = crafting_panel.transform.position;
        craft_destination = craft_start_pos;
        craft_opened_pos = craft_start_pos + new Vector3(0, 10.5f, 0);
    }

    
    void Update()
    {
        if (!craft_transition && Input.GetKeyDown(craft_panel_key))
        {

            craft_transition = true;
            if (crafting_open == false)
            {
                craft_destination = craft_opened_pos;


            }
            else
            {
                craft_destination = craft_start_pos;
                
            }
            
            crafting_open_close();


        }
        
        
        if (crafting_open)
        {
            move_in_craft();
            if(Input.GetKeyDown(craft_key))
            {
                management.try_crafting();
            }
            
        }
        
        crafting_panel.transform.position = Vector3.Lerp(crafting_panel.transform.position, craft_destination, 0.02f);
        
    }

    public void crafting_open_close()
    {
        craft_transition = false;
        crafting_open = !crafting_open;
        
    }
    
    private void move_in_craft()
    {
        if (Input.GetKeyDown(up) && management.chosen_slot_index > 4)
        {
            management.chosen_slot_index -= 5;
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(down) && management.chosen_slot_index < management.slots.Length-5)
        {
            management.chosen_slot_index += 5;
            management.Change_Chosen();
        }
        if (Input.GetKeyDown(left) && management.chosen_slot_index > 0)
        {
            management.chosen_slot_index -= 1;
            management.Change_Chosen();
        }
        
        if (Input.GetKeyDown(right) && management.chosen_slot_index < management.slots.Length-1)
        {
            management.chosen_slot_index += 1;
            management.Change_Chosen();
        }
    }
    
}
