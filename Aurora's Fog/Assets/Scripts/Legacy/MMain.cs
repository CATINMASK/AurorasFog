using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
// Main menu Main script (OLD)
public class MMain : MonoBehaviour {

    public string playerName;
    public Material skyboxDay;
    public Material skyboxNight;

    void Awake()
    {
        if (PlayerPrefs.GetString("playerName") == "")
        {
            playerName = SystemInfo.deviceName;
        }
    }

    void Start()
    {
        //SetSkybox();
    }

    void Update()
    {
    }

    public void LoadLevel(int value)
    {
        SceneManager.LoadScene(value);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetSkybox()
    {
        float computerTime = ((float)System.DateTime.Now.Hour + ((float)System.DateTime.Now.Minute * 0.01f));
        if (computerTime > 6.00f && computerTime < 18.00f)
            RenderSettings.skybox = skyboxDay;
        else
            RenderSettings.skybox = skyboxNight;
    }
}
