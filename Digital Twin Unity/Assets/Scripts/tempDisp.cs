using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempDisp : MonoBehaviour
{
    public Material Displaycolor;
    public bool x = false;
    IEnumerator displayTemperature() {
        while (true)
        {
            if (x == true)
            {
                Displaycolor.color = Color.red;
            }
            else
            {
                Displaycolor.color = Color.blue;
            }
            yield return new WaitForSeconds(5);
        }
    }
    void Start()
    {
        StartCoroutine(displayTemperature());
    }

}
