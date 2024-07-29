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
    Inventory inventory;
    private Loots loot;


    void Start()
    {
        inventory = Inventory.Instance;
        max_health = data.max_health;
        current_health = max_health;
        temp_script = GameObject.FindGameObjectWithTag(data.tag).GetComponent<Source_Spawner>();
        loot = data.loot;
    }


    

    public void take_damage(float amount)
    {
        current_health -= amount;
        if (current_health <= 0)
        {
            StartCoroutine(death());
        }
    }


    public IEnumerator death()
    {
        
        
        transform.position -= new Vector3(0, -100, 0);
        yield return new WaitForSeconds(0.1f);
        inventory.Add_Item(loot);
        temp_script.decrease_obj();
        Destroy(gameObject);
        
    }

    
}