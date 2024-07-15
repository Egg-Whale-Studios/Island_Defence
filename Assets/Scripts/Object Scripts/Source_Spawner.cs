using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Source_Spawner : MonoBehaviour
{
    public Materials data;
    private GameObject Obj;
    private int source_count;
    private int source_limit;
    public MeshCollider land;
    private float cooldown;
    private bool on_cooldown = true;
    private float target_time;

    private float[] x_range;
    private float[] y_range;

    private void Start()
    {
        source_count = 0;
        source_limit = data.object_limit;
        Obj = data.the_object;
        cooldown = data.cooldown;

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
        Vector3 temp_pos;
        Bounds bounds = land.bounds;
        float randomx = Random.Range(bounds.min.x, bounds.max.x);
        float randomz = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 position = new Vector3(randomx, bounds.max.y + 5, randomz);

        Ray ray = new Ray(position, Vector3.down);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 10, LayerMask.GetMask("Ground")))
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