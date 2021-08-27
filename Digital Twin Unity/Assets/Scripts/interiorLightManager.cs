using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interiorLightManager : MonoBehaviour
{    
    private float lastRoom1Light=10f;
    private float Room1Light;
    private float lastRoom2Light=10f;
    private float Room2Light;
    private float lastRoom3Light=10f;
    private float Room3Light;
    public Light[] sceneLightR1 = new Light[10];
    public Light[] sceneLightR2 = new Light[3];
    public Light[] sceneLightR3 = new Light[8];

    void Start()
    {
        StartCoroutine(updateLight());
    }

    // Update is called once per frame
    IEnumerator updateLight(){
        GameObject dataBase=GameObject.Find("DataBase");
        DataBase dataScript = dataBase.GetComponent<DataBase>();
        while(true){
            Room1Light = dataScript.currentLightRoom1;
            if (lastRoom1Light != Room1Light){
                for(int i=0;i<10;i++)
                sceneLightR1[i].intensity = Room1Light;
            lastRoom1Light = Room1Light;
            }
            
            Room2Light = dataScript.currentLightRoom2;
            if (lastRoom2Light != Room2Light){
                for(int i=0;i<3;i++)
                sceneLightR2[i].intensity = Room2Light;
            lastRoom2Light = Room2Light;
            }

             Room3Light = dataScript.currentLightRoom3;
            if (lastRoom3Light != Room3Light){
                for(int i=0;i<8;i++)
                sceneLightR3[i].intensity = Room3Light;
            lastRoom3Light = Room3Light;
            }

        yield return new WaitForSeconds(0);
        }
    }
}
