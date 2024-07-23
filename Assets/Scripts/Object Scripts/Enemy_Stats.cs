using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Stats : MonoBehaviour
{
    [Header("Stats")]
    public float max_health;
    private float current_health;
    public string type;
    private Rigidbody body;
    private NavMeshAgent agent;
    
    [Header("Healthbar")]
    public GameObject foreground;
    private Image img;
    
    
    void Start()
    {
        current_health = max_health;
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        img = foreground.GetComponent<Image>();
    }

    
    void Update()
    {
        
        
        img.fillAmount = current_health / max_health;
        img = foreground.GetComponent<Image>();
        
    }
    
    
    
    #region *Health Tweak*
    private void die()
    {
        if (current_health <= 0)
        {
            Destroy(gameObject);
        } 
    }
    
    
    public IEnumerator take_damage(float amount, Vector3 weaponPosition, float knockbackForce)
    {
        
        current_health -= amount;
        Debug.Log(current_health);
        
        
        Vector3 knockbackDirection = (transform.position - weaponPosition).normalized;
        body.isKinematic = false;
        agent.ResetPath();
        body.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        die();
        yield return new WaitForSeconds(0.5f);
        if (body != null) body.isKinematic = true;
        
        
        
    }
    
    
    #endregion
}