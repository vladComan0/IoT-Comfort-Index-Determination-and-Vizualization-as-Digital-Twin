using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Toggle fullToggle;
    public Slider sensitivitySlider;

    void Start() {
        
        resolutions=Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width +" x " +resolutions[i].height;
            options.Add(option);
        }

        resolutionDropdown.AddOptions(options);
        if(PlayerPrefs.GetInt("FirstTime")!=1){
        PlayerPrefs.SetInt("Resolution", (int)resolutions.Length-1);
        Screen.SetResolution(resolutions[PlayerPrefs.GetInt("Resolution")].width, resolutions[PlayerPrefs.GetInt("Resolution")].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Graphics", 5);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics"));
        PlayerPrefs.SetInt("Full", (true ? 1 : 0));
        Screen.fullScreen = PlayerPrefs.GetInt("Full")!=0;
        PlayerPrefs.SetFloat("Sensitivity", 3f);
        PlayerPrefs.SetInt("FirstTime",1);
        }

        resolutionDropdown.value=PlayerPrefs.GetInt("Resolution");
        graphicsDropdown.value=PlayerPrefs.GetInt("Graphics");
        fullToggle.isOn=PlayerPrefs.GetInt("Full")!=0;
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
     
    public void SetQuality(int qualityIndex){
        PlayerPrefs.SetInt("Graphics", (int)qualityIndex);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Graphics"));

    }

    public void SetFullscreen(bool isFullscreen) {
        PlayerPrefs.SetInt("Full", (isFullscreen ? 1 : 0));
        Screen.fullScreen = PlayerPrefs.GetInt("Full")!=0;
    }

    public void setResolution(int resolutionIndex){
        PlayerPrefs.SetInt("Resolution", (int)resolutionIndex);
        Resolution resolution = resolutions[PlayerPrefs.GetInt("Resolution")];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setSensitivity(float sensitivity){
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        GameObject cameraLook=GameObject.Find("Main Camera");
        mouseLook lookScript = cameraLook.GetComponent<mouseLook>();
        lookScript.mouseSensitivityVar = PlayerPrefs.GetFloat("Sensitivity");

    }
}
