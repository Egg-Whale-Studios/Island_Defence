using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Position : MonoBehaviour
{
    void LateUpdate()
    {
        
            transform.rotation = Quaternion.Euler(45,0,0);
            
    }
}
