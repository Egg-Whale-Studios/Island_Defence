using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [NonSerialized] public Rigidbody body;
    private GameObject player;
    private Vector3 direction;
    public float speed;
    private Vector3 distance;
    private bool is_thrown;
    private float start_time;
    private Collider throwable_collider;
    public ParticleSystem end_particles;
    public int destroy_cooldown;
    public bool is_sticky;
    
    void Awake()
    {
        throwable_collider = GetComponent<Collider>();
        body = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        start_time = Time.fixedTime;
        throwable_collider.isTrigger = false;
    }


    private void Update()
    {
        
        distance = (player.transform.position - transform.position);
        if(body.velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(body.velocity);
            body.rotation = Quaternion.Lerp(body.rotation, targetRotation, 30 * Time.deltaTime);
        }
        
        
    }

    public void Throw_It()
    {
        is_thrown = true;
        throwable_collider.isTrigger = true;
        transform.forward = (distance).normalized;
        body.velocity = new Vector3(Mathf.Sqrt(3*Physics.gravity.magnitude*distance.magnitude/8)*distance.x/distance.magnitude,Mathf.Sqrt(2*distance.magnitude*Physics.gravity.magnitude/3),Mathf.Sqrt(3*Physics.gravity.magnitude*distance.magnitude/8)*distance.z/distance.magnitude);
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
            if (Time.fixedTime - start_time > 0.2f)

            {
                is_thrown = false;
                body.constraints = RigidbodyConstraints.FreezeAll;
                body.useGravity = false;

                if (other.CompareTag("Player"))
                {
                    if (is_sticky) this.transform.SetParent(other.transform.GetChild(1));
                    
                    Player_Stats stats = other.GetComponent<Player_Stats>();
                    stats.change_health(-10);
                    
                }

            }
            StartCoroutine(destroy_object());
        }
        
    }
}
