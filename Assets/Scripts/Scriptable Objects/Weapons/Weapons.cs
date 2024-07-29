using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Weapons")]
public class Weapons : Inventory_Items
{

    [Header("Info")]
    public string tier;
    public bool dual;
    public string weapon_type;
    public Vector3 handle_position;
    public Animation Walking;
    public Animation Attacking;
    public string animation_bool;


    [Header("Stats")]
    public float damage;
    public float attack_speed;
    public float cooldown;
    public float knockback;












}