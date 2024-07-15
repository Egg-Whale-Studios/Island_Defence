using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Cam_Movement : MonoBehaviour
{
    public GameObject player1;
    //public GameObject player2;
    private Vector3 center;
    private Vector3 difference;
    private Vector3 target;
    private Vector3 smooth;
    private Vector3 velocity = Vector3.zero;
    

    

    private void Update()
    {
        center = player1.transform.position;
        target = new Vector3(center.x, 10, center.z-12);
        Vector3 smooth = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.5f);
        transform.position = smooth;
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
