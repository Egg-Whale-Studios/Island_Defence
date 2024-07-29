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
    private string weapon_type;
    private GameObject weapon_prefab;
    private GameObject temp_weapon1;
    private GameObject temp_weapon2;
    private bool is_dual;
    private bool on_cooldown;
    private float cooldown;
    
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
    private string target;
    
    
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
        inventory_script = GetComponent<Player_Inventory>();
        target = "Enemy";
        on_cooldown = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(attack_key) && inventory_script.inventory_open == false && equipped_weapon && !on_cooldown)
        {
            switch (weapon_type)
            {
                case "Melee":
                    Melee_Attack();
                    break;
                case "Throwable":
                    StartCoroutine(Throwable_Attack());
                    break;
            }
            
        }
        
    }


    private void Melee_Attack()
    {
        animator.SetTrigger(weapon_animation);
    }

    private IEnumerator Throwable_Attack()
    {
        Debug.Log(weapon_behaviour1);
        // Attack animation
        animator.SetTrigger("throw throwable");
        yield return new WaitForSeconds(0.25f);
        temp_weapon1.transform.parent = null;
        
        (weapon_behaviour1 as Throwable_Behaviour).Attack();
        on_cooldown = true;
        yield return new WaitForSeconds(4);
        // Reload
        
        //animator.SetTrigger("Pick_throwable");
        yield return new WaitForSeconds(1);
        Equip_Weapon();
        on_cooldown = false;

        // cooldown'ı silahtan al


    }
    
    
    
    
    
    
    
    
    private void Equip_Weapon()
    {
        
        if(is_dual == false)
        {
            temp_weapon1 = Instantiate(weapon_prefab, right_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon1.transform.SetParent(right_hand.transform);
        }
        else
        {
            temp_weapon1 = Instantiate(weapon_prefab, right_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon1.transform.SetParent(right_hand.transform);
            
            temp_weapon2 = Instantiate(weapon_prefab, left_hand.transform.position + handle_position, Quaternion.LookRotation(transform.forward,new Vector3(0,1,0)));
            temp_weapon2.transform.SetParent(left_hand.transform);
            
        }
        Take_Script(temp_weapon1);
        
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
    
    
    public void Change_Weapon(string new_animation ,string new_weapon_type,bool new_duality, GameObject new_prefab, Vector3 new_handle_position)
    {
        
        weapon_prefab = new_prefab;
        weapon_type = new_weapon_type;
        is_dual = new_duality;
        handle_position = new_handle_position;
        weapon_animation = new_animation;
        
        
        Equip_Weapon();
    }

    private void Take_Script(GameObject temp1)
    {
        switch (weapon_type)
        {
                
            case "Melee" :
                
                weapon_behaviour1 = temp1.GetComponent<Melee_Behaviour>();
                weapon_behaviour1.target[0] = target;
                if (is_dual) { weapon_behaviour2 = temp1.GetComponent<Melee_Behaviour>(); weapon_behaviour2.target[0] = target; }
                break;
            
            case "Throwable" :
                
                weapon_behaviour1 = temp1.GetComponent<Throwable_Behaviour>();
                weapon_behaviour1.target[0] = target;
                if (is_dual) { weapon_behaviour2 = temp1.GetComponent<Throwable_Behaviour>(); weapon_behaviour2.target[0] = target; }
                break;
            
            case "Ranged" :
                weapon_behaviour1 = temp1.GetComponent<Ranged_Behaviour>();
                weapon_behaviour1.target[0] = target;
                if (is_dual) { weapon_behaviour2 = temp1.GetComponent<Ranged_Behaviour>(); weapon_behaviour2.target[0] = target; }
                break;
            
            case "Shield":
                weapon_behaviour1 = temp1.GetComponent<Shield_Behaviour>();
                weapon_behaviour1.target[0] = target;
                if (is_dual) { weapon_behaviour2 = temp1.GetComponent<Shield_Behaviour>(); weapon_behaviour2.target[0] = target; }
                break;
        }
    }
    
    
    

    
}
