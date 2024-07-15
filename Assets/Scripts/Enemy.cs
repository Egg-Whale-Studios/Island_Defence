using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Player_Combat player_combat_script;
    private Animator enemy_animator;
    private Animator player_animator;
    
    public float max_health;
    private float current_health;
    private Transform player_transform;
    private Vector3 distance;
    private string target;
    
    public Weapon_Behaviour weapon;
    
    void Start()
    {
        current_health = max_health;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        player_combat_script = player.GetComponent<Player_Combat>();
        enemy_animator = GetComponent<Animator>();
        player_animator = player.GetComponent<Animator>();
        target = "Player";
        weapon.target = target;
    }

    
    void Update()
    {
        agent.SetDestination(player.transform.position);
        
        //dodge();
        if (agent.remainingDistance < 5)
        {
            enemy_animator.SetBool("is_attacking", true);
            weapon.weapon_collider.isTrigger = true;
            
        }
        else
        {
            enemy_animator.SetBool("is_attacking", false);
            weapon.weapon_collider.isTrigger = false;
        }
    }
    
    private void die()
    {
       if (current_health <= 0)
       {
           Destroy(gameObject);
       } 
    }
    
    public void take_damage(float amount)
    {
        
        current_health -= amount;
        Debug.Log(current_health);
        die();
    }
    
    /*private void dodge()
    {
        if (player_combat_script.is_attacking && Vector3.Distance(player.transform.position, transform.position) < 6)
        {
            enemy_animator.SetBool("is_dodging", true);
            
        }
        else
        {
            enemy_animator.SetBool("is_dodging", false);
        }
        
    }*/
}
