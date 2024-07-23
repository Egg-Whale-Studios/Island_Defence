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
    public GameObject crafting_panel;
    void Start()
    {
        player_inventory = GetComponent<Player_Inventory>();
        management = GameObject.FindWithTag("Canvas").GetComponentInChildren<Crafting_Management>();
    }

    
    void Update()
    {
        if (crafting_open)
        {
            move_in_craft();
            if(Input.GetKeyDown(craft_key))
            {
                
                management.try_crafting();
            }
            
        }
        if(Input.GetKeyDown(craft_panel_key) && !player_inventory.inventory_open && Physics.OverlapSphere(transform.position,5,LayerMask.GetMask("Castle")).Length > 0) // && Physics.OverlapSphere(transform.position,1,LayerMask.GetMask("Castle")).Length > 0
        {
            
            crafting_open_close();
        }
        
    }

    public void crafting_open_close()
    {
        
        if (crafting_open)
        {
            crafting_panel.SetActive(false);
            crafting_open = false;
        }
        else
        {
            crafting_panel.SetActive(true);
            crafting_open = true;
        }
        
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
