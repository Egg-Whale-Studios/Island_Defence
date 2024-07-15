using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Behaviour : MonoBehaviour
{
    public Weapons data;
    private float damage;
    [NonSerialized] public BoxCollider weapon_collider;
    private TrailRenderer trail;
    [NonSerialized] public string target;
    void Awake()
    {
        damage = data.damage;
        weapon_collider = GetComponent<BoxCollider>();
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
    }

    public IEnumerator use_it()
    {
        weapon_collider.isTrigger = true;
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        weapon_collider.isTrigger = false;
        trail.enabled = false;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(target))
        {
            if (target == "Enemy")
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.take_damage(damage);
            }
            else if (target == "Player")
            {
                Player_Stats stats = other.GetComponent<Player_Stats>();
                stats.change_health(-damage);
            }
            
            
        }
        
    }
}
