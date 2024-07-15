using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Crafting_Management : MonoBehaviour
{
    public GameObject[] slots { get; private set; }
    
    public int chosen_slot_index;
    void Awake()
    { 
        
        slots = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }

        chosen_slot_index = 0;
        Change_Chosen();
       
    }
    
    public void Change_Chosen()
    {
        foreach (var i in slots)
        {
            i.GetComponent<Image>().color = Color.white;
        }
        slots[chosen_slot_index].GetComponent<Image>().color = Color.yellow;
    }

    
    
    public void try_crafting()
    {
        
        slots[chosen_slot_index].GetComponent<Crafting_Slot>().Craft();
        
    }
    
}
