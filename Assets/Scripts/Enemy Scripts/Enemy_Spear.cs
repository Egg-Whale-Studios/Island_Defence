using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy_Spear : MonoBehaviour
{
    private GameObject castle;
    private GameObject target_object;
    private NavMeshAgent agent;
    private Animator enemy_animator;
    private Collider cld;
    private GameObject temp;
    
    [Header("Movement")]
    private Vector3 distance;
    private float stop_distance;
    public float backing_up;
    public float speed;
    private Vector3 look_direction;
    private Collider[] group_colliders;
    public GameObject hand;
    
    
    [Header("Attack")]
    public GameObject weapon;
    private float attack_cooldown;
    private bool attack_time;
    public float attack_distance;
    private string[] target = new string[2];
    
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        cld = GetComponent<Collider>();
        castle = GameObject.FindWithTag("Castle");
        enemy_animator = GetComponent<Animator>();
        agent.speed = speed;
        attack_time = true;
        target[0] = "Player";
        target[1] = "Ally";
        target_object = castle;
        
    }

    
    void Update()
    {
        group_movement();
        Player_Ally_Check();
        
        look_direction = (target_object.transform.position - transform.position);
        look_direction.y = 0;
        look_direction.Normalize();
        transform.forward = look_direction;
        
        
        agent.speed = speed;
        agent.stoppingDistance = 0;
        agent.SetDestination(target_object.transform.position + (transform.position - target_object.transform.position).normalized* backing_up);
        
        
        if (attack_time)
        {

            
            StartCoroutine(Throw());


        }
        
        
        
    }
    
    private IEnumerator Throw()
    {
        
        attack_time = false;
        enemy_animator.SetTrigger("Pick_throwable");
        yield return new WaitForSeconds(0.5f);
        temp = Instantiate(weapon, hand.transform.position, Quaternion.LookRotation(hand.transform.forward), hand.transform);
        
        Throwable_Behaviour spear = temp.GetComponent<Throwable_Behaviour>();
        attack_cooldown = spear.cooldown;
        spear.target = target;
        spear.target_object = target_object;
        yield return new WaitForSeconds(1);
        yield return new WaitUntil((() => Vector3.Distance(transform.position, target_object.transform.position) < attack_distance));
        temp.transform.parent = null;
        
        spear.Attack();
        yield return new WaitForSeconds(attack_cooldown); // cooldown'ı silahtan al
        
        attack_time = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 8);
    }
    
    
    #region Group Behaviour

    private void group_movement()
    {
        /*
        float closest = Mathf.Infinity;
        group_colliders = Physics.OverlapSphere(transform.position,8,LayerMask.GetMask("Enemy"));
        if (group_colliders.Length > 1)
        {
            foreach (var enemy_col in group_colliders)
            {
                if (enemy_col != this.cld && enemy_col.GetComponent<Enemy_Stats>().type == "Melee")
                {
                    if (Vector3.Distance(enemy_col.transform.position , transform.position) <= closest)
                    {
                        closest = Vector3.Distance(enemy_col.transform.position , transform.position);
                        Vector3 destination = enemy_col.transform.position + (transform.position - player.transform.position).normalized * 2;
                        agent.SetDestination(destination);
                    }
                }
            }
        }
        */
        
    }
    
    
    
    
    #endregion


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
    
    
}
