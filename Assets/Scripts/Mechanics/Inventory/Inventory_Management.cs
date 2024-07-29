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

    public Sprite[] backgrounds;
    public Sprite[] slot_sprites;
    public Sprite[] short_sprites;
    public Sprite[] long_sprites;

    public GameObject[] buttons;
    public GameObject background;

    public Sprite empty;
    public string tab;

    private Color renk;


    

    void Start()
    {
        
        tab = "Tool";
        
        Debug.Log("management started");
        slots = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
        
        inventory = Inventory.Instance;
        chosen_slot_index = 0;
        Change_Chosen();

    }

   
    
    public void Inventory_Update() 
    {

        
        if (tab == "Weapon")
        {
            for (int i = 0; i < inventory.keys_1.Count; i++)
            {
                
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory.keys_1[i].icon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory.keys_1[i].name;
                if (!inventory.keys[i].stackable)
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    
                }
                else
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        inventory.values_1[i].ToString();
                }
                
                
            }
        }
        if (tab == "Material")
        {
            for (int i = 0; i < inventory.keys_2.Count; i++)
            {
                
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory.keys_2[i].icon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory.keys_2[i].name;
                if (!inventory.keys_2[i].stackable)
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    
                }
                else
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        inventory.values_2[i].ToString();
                }
            
            }
        }
        if (tab == "Tool")
        {
            for (int i = 0; i < inventory.keys_3.Count; i++)
            {
            
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory.keys_3[i].icon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory.keys_3[i].name;
                if (!inventory.keys_3[i].stackable)
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    
                }
                else
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        inventory.values_3[i].ToString();
                }
                
            }
        }
        if (tab == "Build")
        {
            for (int i = 0; i < inventory.keys_4.Count; i++)
            {
                
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory.keys_4[i].icon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory.keys_4[i].name;
                if (!inventory.keys_4[i].stackable)
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    
                }
                else
                {
                    slots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                        inventory.values_4[i].ToString();
                }
                
                
            }
        }





    }

    public void Weapons_section()
    {
        //1 - Yeşil
        //Weapon bazlı 
        //renk = Color.cyan;
        tab = "Weapon";
        background.GetComponent<Image>().sprite = backgrounds[0];

        buttons[0].GetComponent<Image>().sprite = long_sprites[0];
        buttons[1].GetComponent<Image>().sprite = short_sprites[1];
        buttons[2].GetComponent<Image>().sprite = short_sprites[2];
        buttons[3].GetComponent<Image>().sprite = short_sprites[3];

        for (int i= 0;i< slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = slot_sprites[0];
        }
        Debug.Log("Weapon bazlı ");
        Inventory_Update();
        Clear_Inventory();
    }

    public void Materials_section()
    {
        //2 - Mavi
        //Material bazlı 
        //renk = Color.magenta;
        tab = "Material";
        background.GetComponent<Image>().sprite = backgrounds[1];

        buttons[0].GetComponent<Image>().sprite = short_sprites[0];
        buttons[1].GetComponent<Image>().sprite = long_sprites[1];
        buttons[2].GetComponent<Image>().sprite = short_sprites[2];
        buttons[3].GetComponent<Image>().sprite = short_sprites[3];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = slot_sprites[1];
        }
        Debug.Log("Material bazlı ");
        Inventory_Update();
        Clear_Inventory();
        
    }

    public void Tools_section()
    {
        //3 - Kırmızı
        //Tool bazlı 
        //renk = Color.cyan;
        tab = "Tool";
        background.GetComponent<Image>().sprite = backgrounds[2];

        buttons[0].GetComponent<Image>().sprite = short_sprites[0];
        buttons[1].GetComponent<Image>().sprite = short_sprites[1];
        buttons[2].GetComponent<Image>().sprite = long_sprites[2];
        buttons[3].GetComponent<Image>().sprite = short_sprites[3];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = slot_sprites[2];
        }
        Debug.Log("Tool bazlı ");
        Inventory_Update();
        Clear_Inventory();
    }
    public void Builds_section()
    {
        //4- Sarı
        //Build bazlı 
        //renk = Color.gray;
        tab = "Build";
        background.GetComponent<Image>().sprite = backgrounds[3];

        buttons[0].GetComponent<Image>().sprite = short_sprites[0];
        buttons[1].GetComponent<Image>().sprite = short_sprites[1];
        buttons[2].GetComponent<Image>().sprite = short_sprites[2];
        buttons[3].GetComponent<Image>().sprite = long_sprites[3];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = slot_sprites[3];
        }
        Debug.Log("Build bazlı ");
        Inventory_Update();
        Clear_Inventory();
        
    }

    
    
    public void Change_Chosen() // Optimize edilebilir
    {
        foreach (var i in slots)
        {
            i.GetComponent<Image>().color = Color.white;
        }
        slots[chosen_slot_index].GetComponent<Image>().color = Color.gray;
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