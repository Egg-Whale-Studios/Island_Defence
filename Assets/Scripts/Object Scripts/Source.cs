using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public Materials data;
    private float max_health;
    [NonSerialized] public float current_health;
    private Source_Spawner temp_script;
    Inventory inventory = Inventory.Instance;
    private Loots loot;


    void Start()
    {
        max_health = data.max_health;
        current_health = max_health;
        temp_script = GameObject.FindGameObjectWithTag(data.tag).GetComponent<Source_Spawner>();
        loot = data.loot;
    }


    void Update()
    {
        if (current_health <= 0)
        {
            death();
        }
    }

    public void take_damage(float amount)
    {
        current_health -= amount;
    }


    public void death()
    {
        temp_script.decrease_obj();
        inventory.Add_Item(loot);
        Destroy(gameObject);
    }
}