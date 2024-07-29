using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSlot : MonoBehaviour
{
    private Inventory_Management management;
    private Inventory inventory;
    public Player_Inventory Ply_Inv;

    private void Start()
    {
        inventory = Inventory.Instance;
        management = GameObject.FindWithTag("Canvas").transform.GetChild(0).gameObject.GetComponentInChildren<Inventory_Management>();
        Ply_Inv = GameObject.FindWithTag("Player").GetComponent<Player_Inventory>();
    }

    public void ChangeIndexByClick()
    {
        management.chosen_slot_index = Array.IndexOf(management.slots, this.gameObject);
        Ply_Inv.is_equipable();
        management.Change_Chosen();
    }
}
