using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player_Backup : MonoBehaviour
{
    [Header("Movement Keys")]
    [SerializeField] private KeyCode west;
    [SerializeField] private KeyCode east;
    [SerializeField] private KeyCode north;
    [SerializeField] private KeyCode south;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode run;
    [SerializeField] private KeyCode remove_wood;
    [SerializeField] private KeyCode remove_stone;

    [Header("Other Keys")] 
    [SerializeField] private KeyCode axe_key;
    [SerializeField] private KeyCode inventory_key;
    
    [Header("Stats")]
    public float health;
    public float walk_speed;
    public float jump_force;
    private float run_speed;
    private float speed;
    
    public float radius = 3.0f; 
    private float rotation_angle = 0.0f;
    
    [Header("Other")]
    public float max_stamina;
    [NonSerialized] public float current_stamina;
    public GameObject axe;
    private GameObject temp_axe;
    private bool using_tool;
    public GameObject inventory_panel;
    
    public Inventory inventory;
    public Loots stone;
    public Loots wood;
    
    
    private Rigidbody body;
    private Vector3 direction;
    private bool is_grounded;
    
    void Start()
    {
        inventory = Inventory.Instance;
        body = GetComponent<Rigidbody>();
        run_speed = walk_speed * 1.5f;
        current_stamina = max_stamina;
    }

    
    void FixedUpdate()
    {
        
        run_check();
        move_check();
        jump_check();
        
        use_axe();
        
    }

    private void Update()
    {
        inventory_interaction();

        if (Input.GetKeyDown(remove_stone))
        {
           inventory.Remove_Item(stone);
        }
        if (Input.GetKeyDown(remove_wood))
        {
            inventory.Remove_Item(wood);
        }
    }

    #region Movement
    private void run_check()
    {
        if (Input.GetKey(run) && current_stamina>0)
        {
            speed = run_speed;
            current_stamina--;
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
    
    private void move_check()
    {
        Vector3 moveDirection = Vector3.zero;

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

        if (moveDirection != Vector3.zero)
        {
            Quaternion current = transform.localRotation;
            Quaternion target = Quaternion.LookRotation(moveDirection);
            transform.localRotation = Quaternion.Slerp(current, target, 5*Time.deltaTime);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    private void jump_check()
    {
        if (Input.GetKey(jump) && is_grounded)
        {
            body.velocity = new Vector3(0, jump_force, 0);
            is_grounded = false;
        }
    }
    #endregion

    
    private void inventory_interaction()
    {
        if (Input.GetKeyDown(inventory_key))
        {
            if (inventory_panel.activeSelf == false)
            {
                inventory_panel.SetActive(true);
            }
            else
            {
                inventory_panel.SetActive(false);
            }
        }
        
    }

    private void use_axe()
    {
        if (Input.GetKey(axe_key))
        {
            if (using_tool == false)
            {
                temp_axe = Instantiate(axe, transform.position + gameObject.transform.forward*radius, Quaternion.identity);
                using_tool = true;
            }
            rotation_angle += speed * Time.deltaTime;
            
            float x = transform.position.x + Mathf.Cos(rotation_angle) * radius;
            float z = transform.position.z + Mathf.Sin(rotation_angle) * radius;
            
            temp_axe.transform.position = new Vector3(x, transform.position.y, z);
        }
        else
        {
            if (temp_axe != null)
            {
                Destroy(temp_axe);
                using_tool = false;
            }
        }
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        is_grounded = true;
    }

    
}
