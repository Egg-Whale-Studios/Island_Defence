using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar : MonoBehaviour
{
    [Header("Stats")]
    
    public GameObject player;
    private Player_Stats player_script;
    public GameObject foreground;
    private Image img;
    private CanvasGroup bar;
    private bool is_faded;
    
    
    void Start()
    {
        player_script = player.GetComponent<Player_Stats>();
        img = foreground.GetComponent<Image>();
        bar = GetComponent<CanvasGroup>();
    }

    
    void Update()
    {
        
        
        img.fillAmount = player_script.current_health / player_script.max_health;
        
        
    }
    
    
}
