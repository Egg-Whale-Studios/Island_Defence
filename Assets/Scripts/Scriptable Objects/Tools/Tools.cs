using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Tools")]
public class Tools : Inventory_Items
{

    [Header("Info")]
    public string tier;
    public string target;
    public string animation_bool;
    
    
    [Header("Stats")]
    public float rotation_speed;
    public float rotation_angle;
    public float radius = 3.0f;
    public float damage;
    public Vector3 handle_position;
    
    
    
    [Header("Upgrading cost")]
    public Loots[] upgrade_cost;

    




}
