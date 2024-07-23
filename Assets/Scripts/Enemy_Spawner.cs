using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject[] melee;
    public GameObject[] ranged;
    public GameObject[] tank;
    private GameObject land;
    private Collider[] land_colliders;

    public float cooldown;
    private bool on_cooldown = true;
    public int starting_amount;
    private float target_time;
    
    
    private void Start()
    {
        on_cooldown = true;
        target_time = Time.fixedTime + cooldown;
        land = GameObject.FindWithTag("Map Base");
        land_colliders = land.GetComponentsInChildren<MeshCollider>();
        
        for (int i = 0; i < starting_amount; i++)
        {
            Spawn_Check();
        }
    }

    
    void Update()
    {
        if (on_cooldown == false)
        {
            Spawn_Check();
            on_cooldown = true;
            target_time = Time.fixedTime + cooldown;
        }

        if (target_time <= Time.fixedTime)
        {
            on_cooldown = false;
        }
    }

    
    private void Spawn_Check()
    {
        int random_ind = Random.Range(0, land_colliders.Length - 1);
        Vector3 temp_pos;
        Bounds bounds = land_colliders[random_ind].bounds;
        float randomx = Random.Range(bounds.min.x, bounds.max.x);
        float randomz = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 position = new Vector3(randomx, 20, randomz);

        Ray ray = new Ray(position, Vector3.down);
        Debug.DrawRay(position, Vector3.down * 50, Color.red, 5f);
        if (Physics.Raycast(ray, out RaycastHit hit, 50, LayerMask.GetMask("Ground")))
        {
            temp_pos = hit.point + new Vector3(0, 3, 0);

            Instantiate(melee[Random.Range(0,melee.Length-1)], temp_pos + new Vector3(4,0,4), Quaternion.identity);
            Instantiate(ranged[Random.Range(0,ranged.Length-1)], temp_pos, Quaternion.identity);
            Instantiate(tank[Random.Range(0,tank.Length-1)], temp_pos + new Vector3(-4,0,-4), Quaternion.identity);
            

        }
        else
        {

            Spawn_Check();
        }
    }
}
