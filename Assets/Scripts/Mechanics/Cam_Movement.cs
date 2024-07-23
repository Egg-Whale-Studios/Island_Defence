using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam_Movement : MonoBehaviour
{
    public GameObject[] player;
    public bool player_spawned;
    private GameObject map;
    
    private Vector3 center;
    private Vector3 difference;
    private Vector3 target;
    private Vector3 smooth;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private KeyCode turn_right;
    [SerializeField] private KeyCode turn_left;
    private bool turning_right;
    private bool turning_left;
    private int position_index;
    Vector3 cam_position;
    private Camera cam;

    
    
    
    private IEnumerator Start()
    {
        cam = Camera.main;
        map = GameObject.FindWithTag("Map Base");
        center = map.transform.position;
        target = new Vector3(center.x, 80, center.z - 120);
        
        transform.position = target;
        yield return new WaitUntil(() => player_spawned);
        player = GameObject.FindGameObjectsWithTag("Player");
        
    }

    
    
    private void Update()
    {
        
        if(player_spawned)
        {
            
            center = Vector3.zero;
            
            foreach (var player_pos in player)
            {
                center += player_pos.transform.position;
                center /= player.Length;
            }
            target = new Vector3(center.x, 80, center.z - 120);
            Vector3 smooth_pos = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.1f);
            
            transform.position = smooth_pos;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 25, 0.01f);
        }
        

    }
    
    

    /*void FixedUpdate()
    {

        center = (player1.transform.position + player2.transform.position) / 2;
        
        transform.position = new Vector3(center.x, 10, center.z-12);

        difference = player1.transform.position - player2.transform.position;

        if (Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.z, 2))>8)
        {
            cam.orthographicSize = Mathf.Sqrt(Mathf.Pow(difference.x, 2) + Mathf.Pow(difference.z, 2));
        }
        
      
        
    
    }*/
    

    
}
