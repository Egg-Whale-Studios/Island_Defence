using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Shield : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Animator enemy_animator;
    private Collider cld;
    
    [Header("Movement")]
    private Vector3 move_target;
    public float default_speed;
    private Vector3 look_direction;
    private Collider[] group_colliders;
    
    private Collider shield_target;
    
    
    
    [Header("Attack")]
    public Shield_Behaviour shield;
    private string target;
    public float attack_cooldown;
    private float target_time;
    private bool is_agro;
    private bool is_shielding;
    private Collider weapon_collider;
    
    
    
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        cld = GetComponent<Collider>();
        player = GameObject.FindWithTag("Player");
        enemy_animator = GetComponent<Animator>();
        weapon_collider = shield.GetComponent<Collider>();
        target = "Player";
        shield.target = target;
        agent.speed = default_speed;
        target_time = 0;
        
    }

    
    void Update()
    {
        group_movement();
        
        
        Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.005f);
        
        

        
        if (Time.fixedTime > target_time) { is_agro = true; }

        if (shield_target != null)
        {
            
            agent.speed = shield_target.gameObject.GetComponent<NavMeshAgent>().speed;
            
            move_target = shield_target.transform.position + (player.transform.position - shield_target.transform.position).normalized * 8;
            
            agent.SetDestination(move_target);
            
            if ((Vector3.Distance(player.transform.position,transform.position)) < 8 && is_agro)
            {
                StartCoroutine(Attack());
            }
                
        }
        else
        {
            agent.speed = default_speed;
            move_target = player.transform.position + (transform.position - player.transform.position).normalized * 6;
            agent.SetDestination(move_target);
            
            
            if ((transform.position - player.transform.position).magnitude < 7 && is_agro)
            {
                StartCoroutine(Attack());
            }
        }
        
        
    }

    private IEnumerator Attack()
    {
        
        is_agro = false;
        weapon_collider.isTrigger = true;
        target_time = Time.fixedTime + attack_cooldown;
        enemy_animator.SetTrigger("Shield Bash");
        yield return new WaitForSeconds(1f);
        weapon_collider.isTrigger = false;

    }

    private void Block_Attack()
    {
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 15);
    }
    
    #region Group Behaviour

    private void group_movement()
    {
        shield_target = null;
        group_colliders = Physics.OverlapSphere(transform.position,15,LayerMask.GetMask("Enemy"));
        
        if (group_colliders.Length > 1)
        {
            foreach (var enemy_col in group_colliders)
            {
                if (enemy_col != cld && enemy_col.GetComponent<Enemy_Stats>().type == "Ranged")
                {
                    shield_target = enemy_col;
                    
                }
                else if (enemy_col != cld && enemy_col.GetComponent<Enemy_Stats>().type == "Tank")
                {
                    Vector3 move_target =
                        enemy_col.transform.position + (enemy_col.transform.position - transform.position).normalized * 1;
                    enemy_col.transform.position = Vector3.MoveTowards(enemy_col.transform.position, move_target, 0.1f);
                }
            }
        }
    }
    
    
    
    
    #endregion


    
}
