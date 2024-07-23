using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    private Rigidbody body;
    public float max_health;
    [NonSerialized] public float current_health;
    public float max_stamina;
    [NonSerialized] public float current_stamina;
    public bool cut_movement;
    private Animator player_animator;
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        Camera.main.GetComponent<Cam_Movement>().player_spawned = true;
        player_animator = GetComponent<Animator>();
        current_health = max_health;
        current_stamina = max_stamina;
    }

    public IEnumerator Spawn()
    {
        cut_movement = true;
        player_animator.SetTrigger("spawned");
        yield return new WaitForSeconds(5f);
        cut_movement = false;
    }
    

    public void change_stamina(float amount)
    {
        current_stamina += amount;
    }
    
    public void change_health(float amount)
    {
        current_health += amount;
        
    }

    public IEnumerator knockback(Vector3 cause_position,float knockbackForce, float time)
    {
        cut_movement = true;
        Vector3 knockbackDirection = (transform.position - cause_position).normalized;
        //body.constraints = RigidbodyConstraints.None;
        body.constraints &= ~RigidbodyConstraints.FreezeRotation;
        //Debug.Log("Player position : " + transform.position + " Cause position : " + cause_position + " Knockback direction : " + knockbackDirection + " Knockback force : " + knockbackForce);
        body.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(time);
        body.constraints |= RigidbodyConstraints.FreezeRotation;
        //body.constraints &= ~RigidbodyConstraints.FreezeRotation;
        cut_movement = false;

    }
}
