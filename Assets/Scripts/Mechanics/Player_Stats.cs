using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    public float max_health;
    [NonSerialized] public float current_health;
    public float max_stamina;
    [NonSerialized] public float current_stamina;
    void Start()
    {
        current_health = max_health;
        current_stamina = max_stamina;
    }

    
    void Update()
    {
        
    }

    public void change_stamina(float amount)
    {
        current_stamina += amount;
    }
    
    public void change_health(float amount)
    {
        current_health += amount;
    }
}
