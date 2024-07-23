using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Building : MonoBehaviour
{
    
    [Header("Build Keys")] 
    [SerializeField] private KeyCode build_key;
    [SerializeField] private KeyCode rotate_clockwise;
    [SerializeField] private KeyCode rotate_anticlockwise;

    [Header("Required Thingies")] 
    public GameObject build_prefab;
    public GameObject ghost_prefab;
    public GameObject temp_ghost;
    private Build_Behaviour build_behaviour;
    private bool ghost_active;
    public bool equipped_build;
    private Vector3 build_position;
    private Player_Inventory inventory_script;
    private Inventory inventory;
    
    void Start()
    {
        inventory = Inventory.Instance;
        inventory_script = GetComponent<Player_Inventory>();
    }

    
    void Update()
    {
        if (equipped_build)
        {
            ghost_placement();
        }
    }

    private void ghost_placement()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 300, LayerMask.GetMask("Ground")) && Vector3.Distance(hit.point,transform.position) < 40)
        {
            Vector3 position = hit.point;
            position.y = 0.5f;
            if (!ghost_active)
            {
                create_ghost(position);
                ghost_active = true;
            }
            else
            {
                temp_ghost.transform.position = position;
                if (Input.GetKey(rotate_clockwise))
                {
                    temp_ghost.transform.Rotate(Vector3.up, -2f);
                }

                if (Input.GetKey(rotate_anticlockwise))
                {
                    temp_ghost.transform.Rotate(Vector3.up, 2f);
                }
                
                if (Input.GetKeyDown(build_key) && build_behaviour.can_place)
                {
                    build();
                    
                }
            }
        }
        else
        {
            Destroy(temp_ghost);
            ghost_active = false;
            
        }
    }

    private void build()
    {
        Instantiate(build_prefab, temp_ghost.transform.position - new Vector3(0,0.5f,0), temp_ghost.transform.rotation);
        inventory.Remove_Item(build_prefab.name);
        inventory_script.is_equipable();
    }

    private void create_ghost(Vector3 position)
    {
        temp_ghost = Instantiate(ghost_prefab, position, Quaternion.identity);
        build_behaviour = temp_ghost.GetComponent<Build_Behaviour>();
        build_behaviour.ghost_mode();
    }
    
    public void change_build(GameObject new_prefab)
    {
        build_prefab = new_prefab;
        build_prefab.layer = LayerMask.NameToLayer("Player");
        ghost_prefab = build_prefab;
        
    }

    public void Unequip_Build()
    {
        equipped_build = false;
        ghost_active = false;
        if (temp_ghost != null) Destroy(temp_ghost);
    }
}
