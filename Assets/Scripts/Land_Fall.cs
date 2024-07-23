using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land_Behaviour : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        Vector3 currentPosition = transform.position;

        
        if (Mathf.Abs(currentPosition.y) > 0.01f)
        {
            float newY = Mathf.Lerp(currentPosition.y, 0, Time.deltaTime * speed);
            transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}