using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Sword : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    private Player_Combat player_combat_script;
    private Animator enemy_animator;
    private Animator player_animator;
    private Rigidbody body;
    private Collider cld;
    
    [Header("Movement")]
    private Transform player_transform;
    private Vector3 distance;
    private float stop_distance;
    public float backing_up;
    public float closing_in;
    public float speed;
    private Vector3 look_direction;
    private Collider[] group_colliders;
    
    
    [Header("Attack")]
    public Weapon_Behaviour weapon;
    private string target;
    public float attack_cooldown;
    private float target_time;
    private bool is_agro;
    
    
    
    void Start()
    {
        
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        cld = GetComponent<Collider>();
        player = GameObject.FindWithTag("Player");
        player_combat_script = player.GetComponent<Player_Combat>();
        enemy_animator = GetComponent<Animator>();
        player_animator = player.GetComponent<Animator>();
        target = "Player";
        weapon.target = target;
        agent.speed = speed;
        target_time = 0;
    }

    
    void Update()
    {
        group_movement();
        
        
        look_direction = (player.transform.position - transform.position);
        look_direction.y = 0;
        look_direction.Normalize();
        transform.forward = look_direction;
        
        agent.SetDestination(player.transform.position);

        
        if (Time.fixedTime > target_time) { is_agro = true; }
        
        if (is_agro)
        {
            agent.stoppingDistance = closing_in;
            
            agent.speed = speed * 2f;
            if ((transform.position - player.transform.position).magnitude < closing_in)
            {
                StartCoroutine(Attack());
            }
        }
        else
        {
            
            agent.speed = speed;
            agent.stoppingDistance = 0;
            agent.SetDestination(player.transform.position + (transform.position - player.transform.position).normalized* backing_up);
            
        }
        
        
    }

    private IEnumerator Attack()
    {
        
        is_agro = false;
        target_time = Time.fixedTime + attack_cooldown;
        yield return new WaitForSeconds(0.2f);
        enemy_animator.SetTrigger("Attack");
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 6);
    }
    
    #region Group Behaviour

    private void group_movement()
    {
        group_colliders = Physics.OverlapSphere(transform.position,6,LayerMask.GetMask("Enemy"));
        if (group_colliders.Length > 1)
        {
            foreach (var enemy_col in group_colliders)
            {
                if (enemy_col != this.cld)
                {
                    Vector3 move_target =
                        enemy_col.transform.position + (enemy_col.transform.position - transform.position).normalized * 5;
                    enemy_col.transform.position = Vector3.MoveTowards(enemy_col.transform.position, move_target, 0.1f);
                }
            }
        }
    }
    
    
    
    
    #endregion
    
    
}
