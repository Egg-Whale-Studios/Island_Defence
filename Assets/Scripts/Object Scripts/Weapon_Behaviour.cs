using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Behaviour : MonoBehaviour
{
    public Weapons data;
    public GameObject user;
    private Animator animator;
    private float damage;
    private TrailRenderer trail;
    [NonSerialized] public string target;
    [NonSerialized] public bool on_cooldown;
    
    void Awake()
    {
        user = transform.root.gameObject;
        animator = user.GetComponent<Animator>();
        damage = data.damage;
        trail = GetComponentInChildren<TrailRenderer>();
        trail.enabled = false;
    }
    
    public IEnumerator use_it()
    {
        
        trail.enabled = true;
        yield return new WaitForSeconds(1f);
        trail.enabled = false;
        
    }

    
    private IEnumerator OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag(target) && !on_cooldown)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Shield"))
            {
                // Shield block
            }
            else if (target == "Enemy")
            {
                on_cooldown = true;
                Enemy_Stats enemy = other.GetComponent<Enemy_Stats>();
                StartCoroutine(enemy.take_damage(damage, transform.position, 200));
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            else if (target == "Player")
            {
                on_cooldown = true;
                Player_Stats stats = other.GetComponent<Player_Stats>();
                stats.change_health(-damage);
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            
            
        }
        
    }
    
}
