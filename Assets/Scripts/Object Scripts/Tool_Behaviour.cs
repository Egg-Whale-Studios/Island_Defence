using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_Behaviour : MonoBehaviour
{
    public Tools data;
    public float damage;
    public string target;
    
    void Awake()
    {
        damage = data.damage;
        target = data.target;
    }
    
    
}
