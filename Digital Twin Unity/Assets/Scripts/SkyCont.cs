using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCont : MonoBehaviour
{
    public Material clearSky;
    public Material cloudySky;
    public string WeatherType = "Clear";
    IEnumerator AdjustSkyToWeather(){
        while (true)
        {
            
            if (WeatherType == "Clear")
            {
                RenderSettings.skybox = clearSky;
            }
            else if (WeatherType == "Clouds" || WeatherType == "Rain")
            {
                RenderSettings.skybox = cloudySky;
            }

            yield return new WaitForSeconds(10);
        }
    }
        void Start()
    {
        StartCoroutine(AdjustSkyToWeather());
    }

   
}
