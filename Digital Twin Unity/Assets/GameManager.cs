using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI startText;
    public Canvas myCanvas;
    void Start()
    {
        myCanvas.enabled = true;
        Time.timeScale = 0;
       

    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (myCanvas.enabled == false)
            {
                Time.timeScale = 0;
                myCanvas.enabled = true;
                Cursor.lockState = CursorLockMode.None;
                startText.text = "Resume";
            }
            else
                if (myCanvas.enabled == true)
                Application.Quit();
        }
             
    }
    public void startButton() {
        myCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void ExitButton() {
        Application.Quit();
    }
}
