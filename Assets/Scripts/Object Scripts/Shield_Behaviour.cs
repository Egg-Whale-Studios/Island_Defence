using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Behaviour : MonoBehaviour
{
    
    public Weapons data;
    private float damage;
    [NonSerialized] public string target;
    [NonSerialized] public bool on_cooldown;
    private Rigidbody body;

    // Kalkan kırılabilir
    public float max_health;
    private float current_health;
    
    
    void Start()
    {
        damage = data.damage;
        body = GetComponent<Rigidbody>();
    }

    
    
    
    private IEnumerator OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag(target) && !on_cooldown)
        {

            if (target == "Enemy")
            {
                on_cooldown = true;
                Enemy_Stats enemy = other.gameObject.GetComponent<Enemy_Stats>();
                StartCoroutine(enemy.take_damage(damage, transform.position, 200));
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
            else if (target == "Player")
            {
                on_cooldown = true;
                Player_Stats stats = other.gameObject.GetComponent<Player_Stats>();
                stats.change_health(-damage);
                StartCoroutine(stats.knockback(transform.position, 100,2));
                yield return new WaitForSeconds(1);
                on_cooldown = false;
            }
        }
    }
}
