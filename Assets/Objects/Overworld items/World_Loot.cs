using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World_Loot : MonoBehaviour
{
    public Inventory_Items item;
    private Image img;
   
    
    void Start()
    {
        img = GetComponentInChildren<Image>();
    }

    
    void Update()
    {
        
        if (Physics.OverlapSphere(transform.position, 8, LayerMask.GetMask("Player")).Length >= 1)
        {
            Color newColor = img.color;
            newColor.a = Mathf.Lerp(img.color.a, 1, 0.05f);
            img.color = newColor;
        }
        else
        {
            Color newColor = img.color;
            newColor.a = Mathf.Lerp(img.color.a, 0, 0.05f);
            img.color = newColor;
        }
    }
    
    public void Pick_Item()
    {
        Inventory.Instance.Add_Item(item);
        Destroy(gameObject);
    }
    
}
