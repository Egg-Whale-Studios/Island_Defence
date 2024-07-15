using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Build_Behaviour : MonoBehaviour
{
    public Material positive_ghost;
    public Material negative_ghost;
    public bool is_ghost;
    private Renderer ghost_renderer;
    private NavMeshObstacle navMeshObstacle;
    private Collider ghost_collider;
    MaterialPropertyBlock mpb;
    public bool can_place;

    public void Awake()
    {
        ghost_renderer = GetComponent<Renderer>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        ghost_collider = GetComponent<Collider>();
        mpb = new MaterialPropertyBlock();

        can_place = true;
        is_ghost = false;
    }

    public void ghost_mode()
    {
        ghost_collider.isTrigger = true;
        navMeshObstacle.enabled = false;
        for (int i = 0; i < ghost_renderer.materials.Length; i++)
        {
            ghost_renderer.GetPropertyBlock(mpb, i);
            mpb.SetColor("_Color", positive_ghost.color);
            mpb.SetFloat("_Alpha", positive_ghost.color.a);
                
            ghost_renderer.SetPropertyBlock(mpb, i);
        }

        is_ghost = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (is_ghost)
        {
            for (int i = 0; i < ghost_renderer.materials.Length; i++)
            {
                ghost_renderer.GetPropertyBlock(mpb, i);
                mpb.SetColor("_Color", negative_ghost.color);
                mpb.SetFloat("_Alpha", negative_ghost.color.a);
                
                ghost_renderer.SetPropertyBlock(mpb, i);
            }

            can_place = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (is_ghost)
        {
            for (int i = 0; i < ghost_renderer.materials.Length; i++)
            {
                ghost_renderer.GetPropertyBlock(mpb, i);
                mpb.SetColor("_Color", positive_ghost.color);
                mpb.SetFloat("_Alpha", positive_ghost.color.a);
                
                ghost_renderer.SetPropertyBlock(mpb, i);
            }

            can_place = true;
        }
    }
}
