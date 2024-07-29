using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Keys")]
    [SerializeField] private KeyCode west;
    [SerializeField] private KeyCode east;
    [SerializeField] private KeyCode north;
    [SerializeField] private KeyCode south;
    [SerializeField] private KeyCode dodge_key;
    [SerializeField] private KeyCode run;
    
    
    [Header("Stats")]
    public float walk_speed;
    public float jump_force;
    private float run_speed;
    private float speed;
    
    [Header("Required Thingies")]
    private Rigidbody body;
    private Vector3 direction;
    private Vector3 moveDirection;
    private bool is_dodging;
    private Player_Stats stats;
    private Player_Crafting crafting_script;
    private Animator animator;
    private bool dodge_turn_timer;
    private bool against_something;
    
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stats = GetComponent<Player_Stats>();
        crafting_script = GetComponent<Player_Crafting>();
        run_speed = walk_speed * 1.5f;
        //StartCoroutine(stats.Spawn());
    }
    
    void Update()
    {
        if (!stats.cut_movement && !crafting_script.crafting_open)
        {
            Check_For_Walls();
            if (!is_dodging)
            {
                run_check();
                direction_check();
            }
            
            if (!against_something) move();

            if (Input.GetKeyDown(dodge_key) && !is_dodging)
            {
                dodge_turn_timer = true;
                
                StartCoroutine(dodge());
            }

            if (dodge_turn_timer)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection),0.05f);
            }
        }
    }
    


    #region Movement
    private void run_check()
    {
        if (Input.GetKey(run) && stats.current_stamina>0)
        {
            speed = run_speed;
            stats.change_stamina(-0.5f);
        }
        else if (!Input.GetKey(run) && stats.current_stamina<stats.max_stamina)
        {
            speed = walk_speed;
            stats.change_stamina(0.5f);
        }
        else
        {
            speed = walk_speed;
        }
    }
    
    private void move()
    {
        
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            
            body.velocity = new Vector3(moveDirection.x * speed,body.velocity.y,moveDirection.z * speed);
            animator.SetBool("is_moving", true);
        }
        else
        {
            animator.SetBool("is_moving", false);
            body.velocity = new Vector3(0, body.velocity.y, 0);
        }
    }

    private void direction_check()
    {
        moveDirection = Vector3.zero;

        if (Input.GetKey(north))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(south))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(east))
        {
            moveDirection += Vector3.right;
        }
        if (Input.GetKey(west))
        {
            moveDirection += Vector3.left;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 300f, LayerMask.GetMask("Ground")))
        {
            Vector3 targetPosition = hitInfo.point;
            targetPosition.y = transform.position.y; 
            
            Vector3 directionToMouse = (targetPosition - transform.position).normalized;
            
            Quaternion current = transform.localRotation;
            Quaternion target = Quaternion.LookRotation(directionToMouse);
            transform.localRotation = Quaternion.Slerp(current, target, 10 * Time.deltaTime);
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + moveDirection.normalized * 1 + new Vector3(0,3,0), 0.5f);
    }

    private void Check_For_Walls()
    {
        
        against_something = Physics.CheckSphere(transform.position + moveDirection.normalized * 1 + new Vector3(0,3,0), 0.5f, ~(1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("World_Loot") | 1 << LayerMask.NameToLayer("Equipment")));
        if (against_something)
        {
            animator.SetBool("is_moving", false);
        }
    }
    
    #endregion
    
    

    private IEnumerator dodge()
    {
        
        is_dodging = true;
        animator.SetTrigger("dodge");
        
        yield return new WaitForSeconds(0.2f);
        stats.change_stamina(50);
        
        speed = 30;
        yield return new WaitForSeconds(0.2f);
        speed = 25;
        yield return new WaitForSeconds(0.5f);
        is_dodging = false;
        dodge_turn_timer = false;

    }
    
    
    
    
}
