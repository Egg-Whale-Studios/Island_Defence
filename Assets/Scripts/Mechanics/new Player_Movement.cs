using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class new_Player_Movement : MonoBehaviour
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
    public float max_stamina;
    [NonSerialized] public float current_stamina;
    private Rigidbody body;
    private Vector3 direction;
    private Vector3 moveDirection;
    private bool is_dodging;
    

    private Animator animator;
    
    
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        run_speed = walk_speed * 1.5f;
        current_stamina = max_stamina;
    }
    
    void Update()
    {
        if (!is_dodging)
        {
            run_check();
            direction_check();
        }
        move();
        if (Input.GetKeyDown(dodge_key) && !is_dodging) StartCoroutine(dodge());
        
    }
    

    #region Movement
    private void run_check()
    {
        if (Input.GetKey(run) && current_stamina>0)
        {
            speed = run_speed;
            current_stamina -= 0.2f;
        }
        else if (!Input.GetKey(run) && current_stamina<max_stamina)
        {
            speed = walk_speed;
            current_stamina+=0.5f;
        }
        else
        {
            speed = walk_speed;
        }
    }
    
    private void move()
    {
        

        if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            Quaternion current = transform.localRotation;
            Quaternion target = Quaternion.LookRotation(new Vector3(moveDirection.x,0, moveDirection.z));
            transform.localRotation = Quaternion.Slerp(current, target, 5*Time.deltaTime);
            //transform.Translate(0, 0, speed * Time.deltaTime);
            //body.velocity = moveDirection * speed;
            animator.SetBool("is_moving", true);
        }
        else
        {
            animator.SetBool("is_moving", false);
        }
    }

    private void direction_check()
    {
        moveDirection = new Vector3(0,body.velocity.y,0);

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
    }

    

    
    
    #endregion
    
    

    private IEnumerator dodge()
    {
        is_dodging = true;
        animator.SetTrigger("dodge");
        yield return new WaitForSeconds(0.2f);
        current_stamina -= 10.0f;
        moveDirection = transform.forward;
        speed = 25;
        yield return new WaitForSeconds(0.2f);
        speed = 20;
        yield return new WaitForSeconds(0.5f);
        is_dodging = false;
        
        
    }
    
}
