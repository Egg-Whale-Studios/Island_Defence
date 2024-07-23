using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class Player_Tools : MonoBehaviour
{
    [Header("Tool Keys")] 
    [SerializeField] private KeyCode use_key;
    
    [Header("Tool Stats")]
    private Vector3 handle_position;
    
    [Header("Required Thingies")]
    private GameObject tool_prefab;
    private GameObject temp_tool;
    private bool using_tool;
    private Player_Inventory inventory_script;
    public bool equipped_tool;
    public GameObject right_hand;
    private Animator animator;
    private string tool_animation;
    private Tool_Behaviour tool_behaviour;
    Vector3 sphere_position;
    Collider[] hit_colliders;
    GameObject closest;
    

    private void Start()
    {
        inventory_script = GetComponent<Player_Inventory>();
        animator = GetComponent<Animator>();
        
    }
    
    void Update()
    {
        
        if (Input.GetKeyDown(use_key) && inventory_script.inventory_open == false && equipped_tool)
        {
            
            animator.SetTrigger(tool_animation);
            StartCoroutine(use_tool());
            
        }
        
    }


    private IEnumerator use_tool()
    {
        sphere_position = transform.position + transform.forward * 4 + new Vector3(0, 2, 0);
        hit_colliders = Physics.OverlapSphere(sphere_position, 4, LayerMask.GetMask("Source"));
        float min_distance = 10;
        foreach (var collider in hit_colliders)
        {
            
            if ((collider.transform.position - transform.position).magnitude < min_distance)
            {
                closest = collider.gameObject;
                min_distance = (collider.transform.position - transform.position).magnitude;
            
            }
        }
        
        if (closest != null && closest.gameObject.CompareTag(tool_behaviour.target) )
        {
            Source source = closest.GetComponent<Source>();
            yield return new WaitForSeconds(0.4f);
            source.take_damage(tool_behaviour.damage);
        }

        closest = null;
    }
    
    void OnDrawGizmos()
    {
        // Çizim rengini ayarla
        Gizmos.color = Color.red;

        // Kontrol edilen alanı çiz
        Vector3 position = transform.position + transform.forward * 4 + new Vector3(0, 2, 0);
        Gizmos.DrawWireSphere(position, 4);

        // İsteğe bağlı: Çizimin merkezini belirtmek için bir nokta ekleyebilirsiniz.
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(position, 0.1f);
    }
    
    
    
    
    
    private void Equip_Tool()
    {
        temp_tool = Instantiate(tool_prefab, right_hand.transform.position + handle_position, Quaternion.LookRotation(transform.right,new Vector3(0,1,0)));
        temp_tool.transform.SetParent(right_hand.transform);
        tool_behaviour = temp_tool.GetComponent<Tool_Behaviour>();
    }
    
    public void Unequip_Tool()
    {
        if (temp_tool != null)
        {
            Destroy(temp_tool);
            tool_animation = "";
        }
    }
    

    public void Change_Tool(string new_animation, GameObject new_prefab, Vector3 new_handle_position)
    {
        tool_prefab = new_prefab;
        tool_prefab.layer = LayerMask.NameToLayer("Player");
        handle_position = new_handle_position;
        tool_animation = new_animation;
        
        Equip_Tool();
    }
    

    
}
