using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
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
        
        
        img.fillAmount = player_script.current_stamina / player_script.max_stamina;

        if (player_script.current_stamina == player_script.max_stamina && !is_faded)
        {
            StartCoroutine(Fade_Out());
        }
        else if (player_script.current_stamina < player_script.max_stamina)
        {
            is_faded = false;
            bar.alpha = 1;

        }
        
    }
    
    
    private IEnumerator Fade_Out()
    {
        is_faded = true;
        yield return new WaitForSeconds(1f);

        
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bar.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            yield return null;
        }
        
        bar.alpha = 0f;
    }
    
    
}
