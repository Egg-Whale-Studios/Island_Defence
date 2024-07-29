using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Source_Takers : MonoBehaviour
{
    [Header("Info")]
    private NavMeshAgent agent;
    public string spawner_tag;
    private GameObject spawner;
    private string source_tag;
    private GameObject[] sources;
    private Animator animator;
    public GameObject home;
    private bool is_working;

    [Header("Stats")] 
    private Tool_Behaviour tool;
    private float damage;

    public float cooldown;
    private bool on_cooldown;
    
    void Start()
    {
        tool = GetComponentInChildren<Tool_Behaviour>();
        agent = GetComponent<NavMeshAgent>();
        spawner = GameObject.FindWithTag(spawner_tag);
        animator = GetComponent<Animator>();
        damage = tool.damage;
        source_tag = tool.target;
        
        Source_Spawner.ChangingObj += Check_Sources;
    }
    


    private void Update()
    {
        
        if (agent.remainingDistance <= 2f)
        {
            is_working = true;
            if(!on_cooldown)StartCoroutine(Chop());
        }
        else
        {
            is_working = false;
        }
        animator.SetBool("is_working", is_working);
    }

    private IEnumerator Chop()
    {
        
        on_cooldown = true;
        Vector3 sphere_position = transform.position + transform.forward * 4 + new Vector3(0, 2, 0);
        Collider[] hit_colliders = Physics.OverlapSphere(sphere_position, 4, LayerMask.GetMask("Source"));
        float min_distance = 20;
        GameObject closest = null;
        foreach (var collider in hit_colliders)
        {
            
            if ((collider.transform.position - transform.position).magnitude < min_distance)
            {
                closest = collider.gameObject;
                min_distance = (collider.transform.position - transform.position).magnitude;
            
            }
        }
        
        if (closest != null && closest.gameObject.CompareTag(source_tag) )
        {
            animator.SetTrigger("chopping");
            Source source = closest.GetComponent<Source>();
            yield return new WaitForSeconds(0.2f);
            source.take_damage(damage);
        }

        yield return new WaitForSeconds(cooldown);
        closest = null;
        on_cooldown = false;
    }
    
    

    private void OnDestroy()
    {
        Source_Spawner.ChangingObj -= Check_Sources;
    }

    private void Check_Sources()
    {
        sources = GameObject.FindGameObjectsWithTag(source_tag);
        
        if (sources.Length == 0)
        {
            //agent.SetDestination(spawner.transform.position);
            
        }
        else
        {
            GameObject closest_source = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject source in sources)
            {
                float distance = Vector3.Distance(transform.position, source.transform.position);
                if (distance < closestDistance)
                {
                    closest_source = source;
                    closestDistance = distance;
                }
            }

            if (closest_source != null)
            {
                
                Vector3 destination = closest_source.transform.position + (transform.position - closest_source.transform.position).normalized * 5;
                agent.SetDestination(destination);
            }
        }
        
    }
    
    
}
