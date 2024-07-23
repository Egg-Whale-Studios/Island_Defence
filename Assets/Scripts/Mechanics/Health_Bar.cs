using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    [Header("Stats")]
    
    private GameObject player;
    private Player_Stats player_script;
    private GameObject foreground;
    private Image img;
    private bool spawned;
    
    
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        spawned = true;
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<Player_Stats>();
        foreground = transform.GetChild(0).gameObject;
        img = foreground.GetComponent<Image>();
    }

    
    void Update()
    {
        
        if (spawned)
        {
            img.fillAmount = player_script.current_health / player_script.max_health;
        }
        
        
    }
    
    
}
