using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Shield : MonoBehaviour
{
    private GameObject castle;
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
    private string[] target = new string[3];
    public float attack_cooldown;
    private bool is_agro;
    private bool is_shielding;
    private Collider weapon_collider;
    private GameObject target_object;
    
    
    
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        cld = GetComponent<Collider>();
        castle = GameObject.FindWithTag("Castle");
        enemy_animator = GetComponent<Animator>();
        weapon_collider = shield.GetComponent<Collider>();
        target[0] = "Player";
        target[1] = "Ally";
        shield.target = target;
        agent.speed = default_speed;
        //target_time = 0;
        target_object = castle;
        Debug.Log(shield.cooldown);
        Debug.Log(attack_cooldown);
        attack_cooldown = shield.cooldown;
        is_agro = true;
    }

    
    void Update()
    {
        group_movement();
        Player_Ally_Check();
        
        Quaternion targetRotation = Quaternion.LookRotation(target_object.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.005f);
        
        

        
        

        if (shield_target != null)
        {
            
            agent.speed = shield_target.gameObject.GetComponent<NavMeshAgent>().speed;
            
            move_target = shield_target.transform.position + (target_object.transform.position - shield_target.transform.position).normalized * 8;
            
            agent.SetDestination(move_target);
            
            if ((Vector3.Distance(target_object.transform.position,transform.position)) < 8 && is_agro)
            {
                StartCoroutine(Attack());
            }
                
        }
        else
        {
            agent.speed = default_speed;
            move_target = target_object.transform.position + (transform.position - target_object.transform.position).normalized * 6;
            agent.SetDestination(move_target);
            
            
            if ((transform.position - target_object.transform.position).magnitude < 7 && is_agro)
            {
                StartCoroutine(Attack());
            }
        }
        
        
    }

    private IEnumerator Attack()
    {
        
        is_agro = false;
        weapon_collider.isTrigger = true;
        
        enemy_animator.SetTrigger("Shield Bash");
        Debug.Log(attack_cooldown);
        yield return new WaitForSeconds(attack_cooldown);
        weapon_collider.isTrigger = false;
        is_agro = true;

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
    
    
    private void Player_Ally_Check()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 30, LayerMask.GetMask("Ally") + LayerMask.GetMask("Player"));
        
        if (enemies.Length == 0)
        {
            target_object = castle;
        }
        else
        {
            float closest = Mathf.Infinity;
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < closest)
                {
                    closest = Vector3.Distance(enemy.transform.position, transform.position);
                    target_object = enemy.gameObject;
                }
            }
        }
    }
    
    #endregion


    
}
