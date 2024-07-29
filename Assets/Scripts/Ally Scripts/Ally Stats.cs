using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AllyStats : MonoBehaviour
{
    private Rigidbody body;
    private NavMeshAgent agent;
    public float max_health;
    [NonSerialized] public float current_health;
    
    [Header("Healthbar")]
    public GameObject foreground;
    private Image img;
    
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Camera.main.GetComponent<Cam_Movement>().player_spawned = true;
        current_health = max_health;
        img = foreground.GetComponent<Image>();
        
    }


    private void Update()
    {
        img.fillAmount = current_health / max_health;
    }


    public IEnumerator take_damage(float amount, Vector3 weaponPosition, float knockbackForce)
    {
        
        current_health -= amount;
        
        
        Vector3 knockbackDirection = (transform.position - weaponPosition).normalized;
        body.isKinematic = false;
        agent.ResetPath();
        body.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        die();
        yield return new WaitForSeconds(0.5f);
        if (body != null) body.isKinematic = true;
        
    }
    
    private void die()
    {
        if (current_health <= 0)
        {
            Destroy(gameObject);
        } 
    }
    
}
