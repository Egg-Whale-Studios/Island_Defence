using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    
    public List<Inventory_Items> keys;
    public List<int> values;
    public Inventory_Management management;
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
        yield return new WaitUntil(() => Instance != null);
        foreach(Inventory_Items i in starter_tools)
        {
            Add_Item(i);
        }
        
    }

    
    
    public void Add_Item(Inventory_Items item) // Stackeable itemler için
    {
        if (keys.Contains(item))
        {
            values[keys.IndexOf(item)] += 1;
        }
        else
        {
            keys.Add(item);
            values.Add(1);
        }
        
        management.Inventory_Update();
        
    }

    public void Remove_Item(Inventory_Items item) // Stackeable itemler için
    {
        if (keys.Contains(item))
        {
            values[keys.IndexOf(item)] -= 1;
        }
        if (values[keys.IndexOf(item)] == 0)
        {
            values.RemoveAt(keys.IndexOf(item));
            keys.Remove(item);
            management.Clear_Inventory();
        }
        
        management.Inventory_Update();
    }

    public void Remove_Item(string item) // Stackeable itemler için
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].name == item)
            {
                values[i] -= 1;
                if (values[i] == 0)
                {
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
        if (keys.Contains(item) && values[keys.IndexOf(item)] >= count)
        {
            values[keys.IndexOf(item)] -= count;
        }
        else
        {
            Debug.Log("Not enough items!");
        }
        if (values[keys.IndexOf(item)] == 0)
        {
            values.RemoveAt(keys.IndexOf(item));
            keys.Remove(item);
            management.Clear_Inventory();
        }
        
        management.Inventory_Update();
    }

    
}
