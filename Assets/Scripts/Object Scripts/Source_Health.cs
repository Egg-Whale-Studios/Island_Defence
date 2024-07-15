using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Source_health : MonoBehaviour
{
    [Header("Stats")]

    public Materials data;
    public GameObject Obj;
    private Source source;
    public GameObject foreground;
    private Image img;
    private CanvasGroup bar;
    private bool is_faded;
    private float temp;
    private float temp_time;
    
    
    void Start()
    {
        source = Obj.GetComponent<Source>();
        img = foreground.GetComponent<Image>();
        bar = GetComponent<CanvasGroup>();
        temp = data.max_health;

    }

    
    void Update()
    {
        
        
        img.fillAmount = source.current_health / data.max_health;
        
        if (temp > source.current_health && is_faded)
        {
            temp_time = Time.fixedTime;
            is_faded = false;
            bar.alpha = 1;
        }
        else if (temp_time + 5 < Time.fixedTime && !is_faded)
        {
            StartCoroutine(Fade_Out());
            is_faded = true;
        }

        temp = source.current_health;

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