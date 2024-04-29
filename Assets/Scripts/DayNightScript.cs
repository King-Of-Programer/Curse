using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    [SerializeField]
    private Light sun;
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;

    private float nightFactor = .25f;
    private float dayPeriod = 20f;
    private float dayPhase;
    
    void Start()
    {
        dayPhase = 0;
        RenderSettings.skybox = daySkybox;
    }

    
    void Update()
    {
        dayPhase += Time.deltaTime / dayPeriod;
        if (dayPhase > 1)
        {
            dayPhase -= 1f;
        }
        this.transform.eulerAngles = new Vector3(0, 0, -360 * dayPhase);
        bool isNight = dayPhase > 0.25f && dayPhase < 0.75f;
        if (isNight)
        {
            if(RenderSettings.skybox != nightSkybox) 
            { 
                RenderSettings.skybox = nightSkybox;
            }
            
        }
        else
        {
            if (RenderSettings.skybox != daySkybox)
            {
                RenderSettings.skybox = daySkybox;
            }
        }

        float k = LuxFactor(dayPhase);
        sun.intensity = RenderSettings.ambientIntensity = isNight ?  k * nightFactor : k;
        RenderSettings.skybox.SetFloat("_Exposure", k);
      
    }
    float LuxFactor(float t)
    {
        return (1 + Mathf.Cos(4f * Mathf.PI * t)) / 2f; //*(t > 0.25f && t < 0.75f ? 1f : nightFactor);
    }
}
