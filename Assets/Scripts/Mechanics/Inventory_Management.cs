using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Inventory_Management : MonoBehaviour
{
    public GameObject[] slots { get; private set; }
    public Inventory inventory;
    public int chosen_slot_index;
    
    void Start()
    { 
        
        
        slots = new GameObject[transform.childCount]; // Burada null değil transform.GetChild(0).GetChild(0).childCount
        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
        inventory = Inventory.Instance;
        chosen_slot_index = 0;
        Change_Chosen();
        
    }

    public void Inventory_Update() // Event sistemini araştır, önceden izlediğin inventory videosunda vardı
    {
        
        
        
        for (int i = 0; i < inventory.keys.Count; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory.keys[i].icon;
            slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory.keys[i].name;
            slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                inventory.values[i].ToString();

        }
        
        
    }

    public void Change_Chosen() // Optimize edilebilir
    {
        foreach (var i in slots)
        {
            i.GetComponent<Image>().color = Color.white;
        }
        slots[chosen_slot_index].GetComponent<Image>().color = Color.yellow;
    }
    
    public void Clear_Inventory()
    {
        foreach (var i in slots)
        {
            i.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = null;
            i.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
            i.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
        }
    }
}
