using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParameterDIsplay : MonoBehaviour
{

    private string Time;
    private string lastTime=" ";
    private float Room1Temperature;
    private float lastRoom1Temperature;
    private float Room1Humidity;
    private float lastRoom1Humidity=-1f;
    private float Room1Fatigue;
    private float lastRoom1Fatigue=-1f;
    private float Room1Comfort;
    private float lastRoom1Comfort=-1f;

    private float Room2Temperature;
    private float lastRoom2Temperature;
    private float Room2Humidity;
    private float lastRoom2Humidity=-1f;
    private float Room2Fatigue;
    private float lastRoom2Fatigue=-1f;
    private float Room2Comfort;
    private float lastRoom2Comfort=-1f;

    private float Room3Temperature;
    private float lastRoom3Temperature;
    private float Room3Humidity;
    private float lastRoom3Humidity=-1f;
    private float Room3Fatigue;
    private float lastRoom3Fatigue=-1f;
    private float Room3Comfort;
    private float lastRoom3Comfort=-1f;
    private string HydrogenRoom3;
    private string LPGRoom3;
    private string MethaneRoom3;
    private string CarbonRoom3;
    private string AlcoholRoom3;
    private string SmokeRoom3;
    private string PropaneRoom3;
    private string lastHydrogenRoom3=" ";
    private string lastLPGRoom3=" ";
    private string lastMethaneRoom3=" ";
    private string lastCarbonRoom3=" ";
    private string lastAlcoholRoom3=" ";
    private string lastSmokeRoom3=" ";
    private string lastPropaneRoom3=" ";

    private float outsideHumidity;
    private float lastOutsideHumidity=-1f;
    private float outsideTemperature;
    private float lastOutsideTemperature;
    private float outisdeFatigue;
    private float lastOutsideFatigue=-1f;
    private float OutsideComfort;
    private float lastOutsideComfort=-1f;
    private float OutsideMoisture;
    private float lastOutsideMoisture=-1f;

    public TextMeshPro[] room1Text = new TextMeshPro[5];
    public TextMeshPro[] room2Text = new TextMeshPro[5];
    public TextMeshPro[] room3Text = new TextMeshPro[12];
    public TextMeshPro[] OutsideText = new TextMeshPro[6];
    void Start()
    {
        StartCoroutine(updateParametes());
    }

    IEnumerator updateParametes(){
        GameObject dataBase=GameObject.Find("DataBase");
        DataBase dataScript = dataBase.GetComponent<DataBase>();
        while(true){
           
           Time = dataScript.Time;
            if(Time!=lastTime){
            room1Text[0].text = Time;
            room2Text[0].text = Time;
            room3Text[0].text = Time;
            OutsideText[0].text = Time;
            lastTime = Time;
            }

            Room1Temperature = dataScript.TemperatureRoom1;
            if(Room1Temperature!=lastRoom1Temperature){
            room1Text[1].text = "Temperature " + Room1Temperature.ToString() + " C";
            lastRoom1Temperature = Room1Temperature;
            }


            Room1Humidity = dataScript.HumidityRoom1;
            if(Room1Humidity!=lastRoom1Humidity){
            room1Text[2].text = "Humidity " + Room1Humidity.ToString() + "%";
            lastRoom1Humidity = Room1Humidity;
            }

            Room1Fatigue = dataScript.FatigueRoom1;
            if(Room1Fatigue!=lastRoom1Fatigue){
            room1Text[3].text = "Fatigue Index " + Room1Fatigue.ToString() + "%";
            lastRoom1Fatigue = Room1Fatigue;
            }

            Room1Comfort = dataScript.ComfortRoom1;
            if(Room1Comfort!=lastRoom1Comfort){
            room1Text[4].text = "Comfort Index " + Room1Comfort.ToString();
            lastRoom1Comfort = Room1Comfort;
            }

            Room2Temperature = dataScript.TemperatureRoom2;
            if(Room2Temperature!=lastRoom2Temperature){
            room2Text[1].text = "Temperature " + Room2Temperature.ToString() + " C";
            lastRoom2Temperature = Room2Temperature;
            }

            Room2Humidity = dataScript.HumidityRoom2;
            if(Room2Humidity!=lastRoom2Humidity){
            room2Text[2].text = "Humidity " + Room2Humidity.ToString() + "%";
            lastRoom2Humidity = Room2Humidity;
            }

            Room2Fatigue = dataScript.FatigueRoom2;
            if(Room2Fatigue!=lastRoom2Fatigue){
            room2Text[3].text = "Fatigue Index " + Room2Fatigue.ToString() + "%";
            lastRoom2Fatigue = Room2Fatigue;
            }

            Room2Comfort = dataScript.ComfortRoom2;
            if(Room2Comfort!=lastRoom2Comfort){
            room2Text[4].text = "Comfort Index " + Room2Comfort.ToString();
            lastRoom2Comfort = Room2Comfort;
            }


            Room3Temperature = dataScript.TemperatureRoom3;
            if(Room3Temperature!=lastRoom3Temperature){
            room3Text[1].text = "Temperature " + Room3Temperature.ToString() + " C";
            lastRoom3Temperature = Room3Temperature;
            }

            Room3Humidity = dataScript.HumidityRoom3;
            if(Room3Humidity!=lastRoom3Humidity){
            room3Text[2].text = "Humidity " + Room3Humidity.ToString() + "%";
            lastRoom3Humidity = Room3Humidity;
            }

            Room3Fatigue = dataScript.FatigueRoom3;
            if(Room3Fatigue!=lastRoom3Fatigue){
            room3Text[3].text = "Fatigue Index " + Room3Fatigue.ToString() + "%";
            lastRoom3Fatigue = Room3Fatigue;
            }

            Room3Comfort = dataScript.ComfortRoom3;
            if(Room3Comfort!=lastRoom3Comfort){
            room3Text[4].text = "Comfort Index " + Room3Comfort.ToString();
            lastRoom3Comfort = Room3Comfort;
            }

            HydrogenRoom3 = dataScript.HydrogenRoom3;
            if(HydrogenRoom3!=lastHydrogenRoom3){
            room3Text[5].text = "Hydrogen " + HydrogenRoom3;
            lastHydrogenRoom3 = HydrogenRoom3;
            }

            LPGRoom3 = dataScript.LPGRoom3;
            if(LPGRoom3!=lastLPGRoom3){
            room3Text[6].text = "LPG   " + LPGRoom3;
            lastLPGRoom3 = LPGRoom3;
            }

            MethaneRoom3 = dataScript.MethaneRoom3;
            if(MethaneRoom3!=lastMethaneRoom3){
            room3Text[7].text = "Methane " + MethaneRoom3;
            lastMethaneRoom3 = MethaneRoom3;
            }

            CarbonRoom3 = dataScript.CarbonRoom3;
            if(CarbonRoom3!=lastCarbonRoom3){
            room3Text[8].text = "Carbon Monoxide " + CarbonRoom3;
            lastCarbonRoom3 = CarbonRoom3;
            }

            AlcoholRoom3 = dataScript.AlcoholRoom3;
            if(AlcoholRoom3!=lastAlcoholRoom3){
            room3Text[9].text = "Alcohol   " + AlcoholRoom3;
            lastAlcoholRoom3 = AlcoholRoom3;
            }


            SmokeRoom3 = dataScript.SmokeRoom3;
            if(SmokeRoom3!=lastSmokeRoom3){
            room3Text[10].text = "Smoke  " + SmokeRoom3;
            lastSmokeRoom3 = SmokeRoom3;
            }

            PropaneRoom3 = dataScript.PropaneRoom3;
            if(PropaneRoom3!=lastPropaneRoom3){
            room3Text[11].text = "Propane   " + PropaneRoom3;
            lastPropaneRoom3 = PropaneRoom3;
            }

            

            outsideTemperature=dataScript.TemperatureOutside;
            if(lastOutsideTemperature!=outsideTemperature){
            OutsideText[1].text = "Temperature " + outsideTemperature + "C";
            lastOutsideTemperature = outsideTemperature;
            }
            
            outsideHumidity=dataScript.outsideHumidity;
            if(lastOutsideHumidity!=outsideHumidity){
            RenderSettings.fogDensity = 0.035f*outsideHumidity;
            OutsideText[2].text = "Humidity " + (outsideHumidity*100) + "%";
            lastOutsideHumidity = outsideHumidity;
            }

            outisdeFatigue=dataScript.FatigueOutside;
            if(lastOutsideFatigue!=outisdeFatigue){
            OutsideText[3].text = "Fatigue Index " + outisdeFatigue + "%";
            lastOutsideFatigue = outisdeFatigue;
            }

            OutsideComfort=dataScript.ComfortOutside;
            if(lastOutsideComfort!=OutsideComfort){
            OutsideText[4].text = "Comfort Index " + OutsideComfort;
            lastOutsideComfort = OutsideComfort;
            }

            OutsideMoisture=dataScript.MoistureOutside;
            if(lastOutsideMoisture!=OutsideMoisture){
            OutsideText[5].text = "Soil Moisture " + OutsideMoisture + "%";
            lastOutsideMoisture = OutsideMoisture;
            }

            yield return new WaitForSeconds(0);
        }
    }
}
