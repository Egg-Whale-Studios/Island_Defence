using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shield_Behaviour : Weapon_Behaviour
{
    
    
    [NonSerialized] public bool on_cooldown;
    private Rigidbody body;

    // Kalkan kırılabilir
    public float max_health;
    private float current_health;
    
    
    void Start()
    {
        damage = data.damage;
        cooldown = data.cooldown;
        body = GetComponent<Rigidbody>();
    }

    
    
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
      
        if (target.Contains(other.tag) && !on_cooldown)
        {
            
            if (target.Contains("Enemy"))
            {
                on_cooldown = true;
                Enemy_Stats enemy = other.gameObject.GetComponent<Enemy_Stats>();
                StartCoroutine(enemy.take_damage(damage, transform.position, 200));
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            else if (target.Contains("Player"))
            {
                on_cooldown = true;
                if(other.CompareTag("Player"))
                {
                    
                    Player_Stats stats = other.gameObject.GetComponent<Player_Stats>();
                    stats.change_health(-damage);
                    StartCoroutine(stats.knockback(transform.position, 100, 2));
                }
                else
                {
                    AllyStats ally = other.gameObject.GetComponent<AllyStats>();
                    StartCoroutine(ally.take_damage(damage, transform.position, 200));
                }
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            
        }
    }
}
