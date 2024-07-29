using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Melee_Behaviour : Weapon_Behaviour
{
    
    public GameObject user;
    private Animator animator;
    
    private TrailRenderer trail;
    [NonSerialized] public bool on_cooldown;
    
    [Header("Shield Detection")]
    
    public Vector3 boxCenter;
    public Vector3 boxSize;
    public LayerMask shield_layer;
    
    void Start()
    {
        boxCenter = Vector3.zero;
        boxSize = new Vector3(0.5f, 6, 1);
        user = GameObject.FindWithTag("Player");
        
        animator = user.GetComponent<Animator>();
        
        
        damage = data.damage;
        weapon_type = data.weapon_type;
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
    }
    
    public IEnumerator use_it() // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Bunu animasyonla hallet
    {
        
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        trail.enabled = false;
        
    }

    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (target.Contains(other.tag) && !on_cooldown)
        {
            if (other.CompareTag("Shield"))
            {
                animator.SetTrigger("blocked");
                yield break;
            }
            if (target.Contains("Enemy"))
            {
                on_cooldown = true;
                Enemy_Stats enemy = other.GetComponent<Enemy_Stats>();
                StartCoroutine(enemy.take_damage(damage, transform.position, 200));
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            else if (target.Contains("Player"))
            {
                on_cooldown = true;
                if (other.CompareTag("Player"))
                {
                    Player_Stats stats = other.GetComponent<Player_Stats>();
                    stats.change_health(-damage);
                }
                else
                {
                    AllyStats ally = other.GetComponent<AllyStats>();
                    StartCoroutine(ally.take_damage(damage, transform.position, 200));
                }
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            
            
        }
        
    }
}
