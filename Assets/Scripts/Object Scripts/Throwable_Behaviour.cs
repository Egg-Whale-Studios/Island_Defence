using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Throwable_Behaviour : Weapon_Behaviour
{
    
    
    [NonSerialized] public Rigidbody body;
    public GameObject target_object;
    
    private Vector3 direction;
    
    private Vector3 destination;
    private bool is_thrown;
    
    private Collider throwable_collider;

    private Collider[] enemy_to_assist;
    private GameObject assist_target;
    public int destroy_cooldown;
    public bool is_sticky;
    
    
    void Awake()
    {
        throwable_collider = GetComponent<Collider>();
        
    }


    private void Update()
    {
        
        if(is_thrown && body.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(body.velocity);
            body.rotation = Quaternion.Lerp(body.rotation, targetRotation, 30 * Time.deltaTime);
            
        }

        

    }

    public new void Attack()
    {
        if (target_object == null)
        {
            return;
        }
        if (target.Contains("Player"))
        {
            destination = (target_object.transform.position - transform.position);
        }
        else if (target.Contains("Enemy"))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 300))
            {
                destination = hit.point - transform.position;
                
                
                /*enemy_to_assist = Physics.OverlapSphere(hit.point, 5f, LayerMask.NameToLayer("Enemy"));
                
                if (enemy_to_assist.Length > 0)
                {
                    is_assisting = true;
                    assist_target = enemy_to_assist[0].gameObject;
                }
                */
            }
            
        }
        
        
        is_thrown = true;
        gameObject.AddComponent<Rigidbody>();
        body = GetComponent<Rigidbody>();
        
        transform.forward = (destination).normalized;
        //body.velocity = new Vector3(Mathf.Sqrt(3*Physics.gravity.magnitude*destination.magnitude/8)*destination.x/destination.magnitude,Mathf.Sqrt(2*destination.magnitude*Physics.gravity.magnitude/3),Mathf.Sqrt(3*Physics.gravity.magnitude*destination.magnitude/8)*destination.z/destination.magnitude);
        body.velocity = new Vector3(Mathf.Sqrt(3*Physics.gravity.magnitude*destination.magnitude/8)*destination.x/destination.magnitude,0,Mathf.Sqrt(3*Physics.gravity.magnitude*destination.magnitude/8)*destination.z/destination.magnitude)*4;
    }



    IEnumerator destroy_object()
    {
        yield return new WaitForSeconds(destroy_cooldown);
        Destroy(gameObject);
    }
    
    

    private void OnTriggerEnter(Collider other)
    {
        if (is_thrown)
        {
            
            is_thrown = false;
            body.constraints = RigidbodyConstraints.FreezeAll;
            body.useGravity = false;

            if (target.Contains(other.tag))
            {
                if (is_sticky) transform.SetParent(other.transform.GetChild(1));
                if(target.Contains("Player"))
                {
                    if (other.CompareTag("Player"))
                    {
                        Player_Stats stats = other.GetComponent<Player_Stats>();
                        stats.change_health(-damage);
                    }
                    else
                    {
                        AllyStats ally = other.GetComponent<AllyStats>();
                        StartCoroutine(ally.take_damage(damage, transform.position, 0));
                    }
                }
                else if (target.Contains("Enemy"))
                {
                    Enemy_Stats stats = other.GetComponent<Enemy_Stats>();
                    StartCoroutine(stats.take_damage(damage, transform.position, 0));
                }
                
            }
            

            
            StartCoroutine(destroy_object());
        }
        
    }
}
