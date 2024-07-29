using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<Inventory_Items> keys;
    public List<int> values;
    
    
    public List<Inventory_Items> keys_1;
    public List<int> values_1;
    
    public List<Inventory_Items> keys_2;
    public List<int> values_2;
    
    public List<Inventory_Items> keys_3;
    public List<int> values_3;
    
    public List<Inventory_Items> keys_4;
    public List<int> values_4;
    
    private Inventory_Management management;
    public Inventory_Items[] starter_tools;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

    }

    IEnumerator Start()
    {

        management = GameObject.FindWithTag("Canvas").transform.GetChild(0).gameObject.GetComponentInChildren<Inventory_Management>();

        yield return new WaitUntil(() => Instance != null);


        foreach (Inventory_Items i in starter_tools)
        {
            Add_Item(i);
        }

    }

    public Tuple<List<Inventory_Items>, List<int>> Find_Tab(string type)
    {
        
        if (type == "Weapon")
        {
            return new Tuple<List<Inventory_Items>, List<int>>(keys_1, values_1);
        }
        else if (type == "Material")
        {
            return new Tuple<List<Inventory_Items>, List<int>>(keys_2, values_2);
        }
        else if (type == "Tool")
        {
            return new Tuple<List<Inventory_Items>, List<int>>(keys_3, values_3);
        }
        else if (type == "Build")
        {
            return new Tuple<List<Inventory_Items>, List<int>>(keys_4, values_4);
        }
        else
        {
            return new Tuple<List<Inventory_Items>, List<int>>(null, null);
        }
    }

    public void Add_Item(Inventory_Items item) // Stackeable itemler için
    {
        
        if (item.stackable)
        {
            if (Find_Tab(item.type).Item1.Contains(item))
            {
                Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] += 1;
                values[keys.IndexOf(item)] += 1;

            }
            else
            {
                Find_Tab(item.type).Item1.Add(item);
                Find_Tab(item.type).Item2.Add(1);
                keys.Add(item);
                values.Add(1);

            }
        }
        else
        {
            Find_Tab(item.type).Item1.Add(item);
            Find_Tab(item.type).Item2.Add(1);
            keys.Add(item);
            values.Add(1);

        }
        management.Inventory_Update();

    }

    public void Remove_Item(Inventory_Items item) // Stackeable itemler için
    {
        
        if (Find_Tab(item.type).Item1.Contains(item))
        {
            Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] -= 1;
            values[keys.IndexOf(item)] -= 1;

        }
        if (Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] == 0)
        {
            Find_Tab(item.type).Item2.RemoveAt(Find_Tab(item.type).Item1.IndexOf(item));
            Find_Tab(item.type).Item1.Remove(item);
            values.RemoveAt(keys.IndexOf(item));
            keys.Remove(item);

            management.Clear_Inventory();
        }

        management.Inventory_Update();
    }

    public void Remove_Item(string item) // Stackeable itemler için
    {
        
        for (int i = 0; i < keys_1.Count; i++)
        {
            if (keys_1[i].name == item)
            {
                values_1[i] -= 1;
                values[i] -= 1;

                if (values_1[i] == 0)
                {
                    values_1.RemoveAt(i);
                    keys_1.RemoveAt(i);
                    values.RemoveAt(i);
                    keys.RemoveAt(i);

                    management.Clear_Inventory();
                }
                management.Inventory_Update();
                return;
            }
        }
        
        for (int i = 0; i < keys_2.Count; i++)
        {
            if (keys_2[i].name == item)
            {
                values_2[i] -= 1;
                values[i] -= 1;

                if (values_2[i] == 0)
                {
                    values_2.RemoveAt(i);
                    keys_2.RemoveAt(i);
                    values.RemoveAt(i);
                    keys.RemoveAt(i);

                    management.Clear_Inventory();
                }
                management.Inventory_Update();
                return;
            }
        }
        
        for (int i = 0; i < keys_3.Count; i++)
        {
            if (keys_3[i].name == item)
            {
                values_3[i] -= 1;
                values[i] -= 1;

                if (values_3[i] == 0)
                {
                    values_3.RemoveAt(i);
                    keys_3.RemoveAt(i);
                    values.RemoveAt(i);
                    keys.RemoveAt(i);

                    management.Clear_Inventory();
                }
                management.Inventory_Update();
                return;
            }
        }
        
        for (int i = 0; i < keys_4.Count; i++)
        {
            if (keys_4[i].name == item)
            {
                values_4[i] -= 1;
                values[i] -= 1;

                if (values_4[i] == 0)
                {
                    values_4.RemoveAt(i);
                    keys_4.RemoveAt(i);
                    values.RemoveAt(i);
                    keys.RemoveAt(i);

                    management.Clear_Inventory();
                }
                management.Inventory_Update();
                return;
            }
        }
        
    }

    public void Remove_Item(Inventory_Items item, int count) // Stackeable itemler için
    {
       
        if (Find_Tab(item.type).Item1.Contains(item) && Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] >= count)
        {
            Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] -= count;
            values[keys.IndexOf(item)] -= count;

        }
        else
        {
            Debug.Log("Not enough items!");
        }
        if (Find_Tab(item.type).Item2[Find_Tab(item.type).Item1.IndexOf(item)] == 0)
        {
            Find_Tab(item.type).Item2.RemoveAt(Find_Tab(item.type).Item1.IndexOf(item));
            Find_Tab(item.type).Item1.Remove(item);
            values.RemoveAt(keys.IndexOf(item));
            keys.Remove(item);

            management.Clear_Inventory();
        }

        management.Inventory_Update();
    }

    public GameObject Drop_Item()
    {
        
        
        GameObject dropping_loot = Find_Tab(management.tab).Item1[management.chosen_slot_index].world_object;
        Remove_Item(Find_Tab(management.tab).Item1[management.chosen_slot_index]);
        management.Inventory_Update();
        return dropping_loot;




    }
}