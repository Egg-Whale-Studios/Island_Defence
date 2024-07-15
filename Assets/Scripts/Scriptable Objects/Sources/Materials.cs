using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Materials")]
public class Materials : ScriptableObject
{

    public new string name;
    
    [Header("Obtaining")]
    
    public GameObject[] gather_tool;
    public Loots loot;
    
    [Header("Stats")]
    public float max_health;
    public float tier;


    [Header("Spawning")] 
    public GameObject the_object;
    public int object_limit;
    public float cooldown;
    public string tag;




}
