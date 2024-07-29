using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Behaviour : MonoBehaviour
{
    public Weapons data;
    [NonSerialized] public float damage;
    [NonSerialized] public float cooldown;
    [NonSerialized] public string weapon_type;
    [NonSerialized] public string[] target = new string[3];
    
    
    void Start()
    {
        damage = data.damage;
        weapon_type = data.weapon_type;
        cooldown = data.cooldown;
    }

    public void Attack()
    {
        Debug.Log("Tatsız");
    }

    
}
