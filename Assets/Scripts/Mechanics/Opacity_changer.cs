using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity_changer : MonoBehaviour
{   
    public GameObject player1;
    public GameObject player2;
    private Vector3 direction;
    private bool opacity_check1;
    private bool opacity_check2;
    public LayerMask layermask;
    private Renderer obj_renderer1;
    private Renderer obj_renderer2;

    private void Start()
    {
        direction = new Vector3(0,9.5f, -16.45f).normalized;
        opacity_check1 = true;
        opacity_check2 = true;
    }

    void Update()
    {
        check_player1();
        check_player2();
        
        /*
        Ray ray = new Ray(player1.transform.position, direction);
        Debug.DrawLine(player1.transform.position,player1.transform.position + direction * 20f,Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, 20f,layermask) && opacity_check)
        {
            opacity_check = false;
            obj_renderer = hit.collider.gameObject.GetComponent<Renderer>();
            Color temp_color = obj_renderer.material.color;
            temp_color.a = 0.5f;
            obj_renderer.material.color = temp_color;
            Debug.Log("Saydamlaşyom");
        }
        else if (Physics.Raycast(ray, out hit, 20f,layermask) == false && opacity_check == false)
        {
            Color temp_color = obj_renderer.material.color;
            temp_color.a = 1f;
            obj_renderer.material.color = temp_color;
            Debug.Log("Matlaşıyom");
            opacity_check = true;
        }*/
        
    }
    private void check_player1()
    {
        Ray ray = new Ray(player1.transform.position, direction);
        Debug.DrawLine(player1.transform.position,player1.transform.position + direction * 20f,Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit1, 20f,layermask) && opacity_check1)
        {
            opacity_check1 = false;
            obj_renderer1 = hit1.collider.gameObject.GetComponent<Renderer>();
            Color temp_color = obj_renderer1.material.color;
            temp_color.a = 0.5f;
            obj_renderer1.material.color = temp_color;
        }
        else if (Physics.Raycast(ray, out hit1, 20f,layermask) == false && opacity_check1 == false && obj_renderer1 != null)
        {
            Color temp_color = obj_renderer1.material.color;
            temp_color.a = 1f;
            obj_renderer1.material.color = temp_color;
            opacity_check1 = true;
        } 
    }
    
    
    private void check_player2()
    {
        Ray ray = new Ray(player2.transform.position, direction);
        Debug.DrawLine(player2.transform.position,player2.transform.position + direction * 20f,Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit2, 20f,layermask) && opacity_check2)
        {
            opacity_check2 = false;
            obj_renderer2 = hit2.collider.gameObject.GetComponent<Renderer>();
            Color temp_color = obj_renderer2.material.color;
            temp_color.a = 0.5f;
            obj_renderer2.material.color = temp_color;
        }
        else if (Physics.Raycast(ray, out hit2, 20f,layermask) == false && opacity_check2 == false && obj_renderer2 != null)
        {
            Color temp_color = obj_renderer2.material.color;
            temp_color.a = 1f;
            obj_renderer2.material.color = temp_color;
            opacity_check2 = true;
        } 
    }
    
    /*private void SetOpacity(Renderer renderer, float opacity)
    {
        Color temp_color = renderer.material.color;
        temp_color.a = opacity;
        renderer.material.color = temp_color;
    }*/
}
