using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory_Items : ScriptableObject
{
    public string description;
    public new string name;
    public Sprite icon;
    public GameObject prefab;
    public string type;
    public bool stackable;


}
