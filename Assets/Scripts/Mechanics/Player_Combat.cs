using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    [Header("Combat Keys")] 
    [SerializeField] private KeyCode attack_key;
    
    
    [Header("Stats")]
    private float cooldown;
    private GameObject weapon_prefab;
    private GameObject temp_weapon1;
    private GameObject temp_weapon2;
    private bool is_dual;
    private string target;
    
    [Header("Required Thingies")]
    
    private Animator animator;
    public bool equipped_weapon;
    public GameObject right_hand;
    public GameObject left_hand;
    Vector3 handle_position;
    private Player_Inventory inventory_script;
    private string weapon_animation;
    private Weapon_Behaviour weapon_behaviour1;
    private Weapon_Behaviour weapon_behaviour2;
    
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        inventory_script = GetComponent<Player_Inventory>();
        target = "Enemy";
    }
    
    void Update()
    {
        if (Input.GetKeyDown(attack_key) && inventory_script.inventory_open == false && equipped_weapon)
        {
            animator.SetTrigger(weapon_animation);
            StartCoroutine(weapon_behaviour1.use_it());
        }
        
    }


    private void Equip_Weapon()
    {
        
        if(is_dual == false)
        {
            temp_weapon1 = Instantiate(weapon_prefab, right_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon1.transform.SetParent(right_hand.transform);
            weapon_behaviour1 = temp_weapon1.GetComponent<Weapon_Behaviour>();
            
        }
        else
        {
            temp_weapon1 = Instantiate(weapon_prefab, right_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon1.transform.SetParent(right_hand.transform);
            
            temp_weapon2 = Instantiate(weapon_prefab, left_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon2.transform.SetParent(left_hand.transform);
            weapon_behaviour1 = temp_weapon1.GetComponent<Weapon_Behaviour>();
            weapon_behaviour2 = temp_weapon1.GetComponent<Weapon_Behaviour>();
            
            weapon_behaviour2.target = target;
        }

        weapon_behaviour1.target = target;
        
    }
    
    public void Unequip_Weapon()
    {
        if(temp_weapon1 != null)
        {
            Destroy(temp_weapon1);
        }
        if(temp_weapon2 != null)
        {
            Destroy(temp_weapon2);
        }
        weapon_animation = "";
    }
    
    
    public void Change_Weapon(string new_animation ,float new_cooldown,bool new_duality, GameObject new_prefab, Vector3 new_handle_position)
    {
        
        weapon_prefab = new_prefab;
        cooldown = new_cooldown;
        is_dual = new_duality;
        handle_position = new_handle_position;
        weapon_animation = new_animation;
        
        Equip_Weapon();
    }
    

    
}
