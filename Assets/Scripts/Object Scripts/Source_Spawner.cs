using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Source_Spawner : MonoBehaviour
{
    public Materials data;
    private GameObject Obj;
    private int source_count;
    private int source_limit;
    private GameObject land;
    private Collider[] land_colliders;
    private float cooldown;
    private bool on_cooldown = true;
    private float target_time;
    public int starting_amount;
    

    private float[] x_range;
    private float[] y_range;

    private void Start()
    {
        
        land = GameObject.FindWithTag("Map Base");
        land_colliders = land.GetComponentsInChildren<MeshCollider>();
        source_count = 0;
        source_limit = data.object_limit;
        Obj = data.the_object;
        cooldown = data.cooldown;
        for (int i = 0; i < starting_amount; i++)
        {
            Spawn_Check();
        }
    }


    void Update()
    {
        
        if (on_cooldown == false && source_count < source_limit)
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
            temp_pos = hit.point;

            Instantiate(Obj, temp_pos, Quaternion.identity);
            source_count++;

        }
        else
        {
            
            Spawn_Check();
        }

    }

    public void decrease_obj()
    {
        source_count -= 1;
    }
}