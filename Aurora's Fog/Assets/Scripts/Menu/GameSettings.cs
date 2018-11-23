using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

// Loads general and video game settings.
public class GameSettings : MonoBehaviour {

	public GameObject optionsMenu;
    public AudioMixer audioMixer;
    [HideInInspector]
	public bool generalSaved = true;
    [HideInInspector]
    public bool optionsSaved = true;
    [HideInInspector]
    public bool videoSaved = true;

	void Awake() {
		CreateResolutionArray ();
    }

	void Start() {
        if (PlayerPrefs.GetInt("firstTime") == 0) {
            print("First game launch.");
            SetGeneralDefault();
            SetVideoDefault();
            SaveVideo();

            GetComponent<InputManager>().SetKeysDefault();
            GetComponent<InputManager>().SaveKeys();


            PlayerPrefs.SetInt("firstTime", 1);

        }
        else {
            print("Second game launch.");
            LoadGeneral();
            LoadVideo();
            GetComponent<InputManager>().SetKeysDefault();
            GetComponent<InputManager>().LoadKeys();
        }

        generalSaved = true;
		optionsSaved = true;
		videoSaved = true;
    }

	void Update(){
        // FOR DEBUG.
        if (Input.GetKeyDown(KeyCode.F1)) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
	}

	public void SaveAllSettings(){
		SaveGeneral ();
		SaveVideo ();
		GetComponent<InputManager> ().SaveKeys ();
	}

		// --- GENERAL settings

	void SetGeneralDefault(){
        Transform generalPanel = optionsMenu.transform.Find ("general_pnl");
		PlayerPrefs.SetInt ("language", generalPanel.Find ("gameLanguage_drop").GetComponent<Dropdown>().value);
		PlayerPrefs.SetFloat ("soundVolume", generalPanel.Find ("sound_sli").GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("musicVolume", generalPanel.Find ("music_sli").GetComponent<Slider> ().value);
        audioMixer.SetFloat("soundVolume", PlayerPrefs.GetFloat("soundVolume"));
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
    }

	public void SaveGeneral(){
		Transform generalPanel = optionsMenu.transform.Find ("general_pnl");
		PlayerPrefs.SetInt ("language", generalPanel.Find ("gameLanguage_drop").GetComponent<Dropdown>().value);
		PlayerPrefs.SetFloat ("soundVolume", generalPanel.Find ("sound_sli").GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("musicVolume", generalPanel.Find ("music_sli").GetComponent<Slider> ().value);
	}
	public void LoadGeneral(){
		Transform generalPanel = optionsMenu.transform.Find ("general_pnl");
		generalPanel.Find ("gameLanguage_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("language");
        generalPanel.Find ("sound_sli").GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
        generalPanel.Find ("music_sli").GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");
        audioMixer.SetFloat("soundVolume", PlayerPrefs.GetFloat("soundVolume"));
        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
    }

	public void SetSoundVolume(float value){
        audioMixer.SetFloat("soundVolume", value);
        PlayerPrefs.SetFloat("soundVolume", value);
    }

	public void SetMusicVolume(float value){
        audioMixer.SetFloat("musicVolume", value);
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void SetGameLanguage (int languageIndex)
    {
        PlayerPrefs.SetInt("language", languageIndex);
    }

		// --- VIDEO settings

	void SetVideoDefault(){
		int resLength = UnityEngine.Screen.resolutions.Length;
		Screen.SetResolution (UnityEngine.Screen.resolutions [resLength-1].width, UnityEngine.Screen.resolutions [resLength-1].height, true);
		optionsMenu.transform.Find ("video_pnl/resolution_drop").GetComponent<Dropdown> ().value = resLength - 1;
        SetVideoQuality(5);
	}

	public void SetVideoQuality(int value){
        QualitySettings.SetQualityLevel(value);
		//UnityEngine.QualitySettings.SetQualityLevel (optionsMenu.transform.Find ("video_pnl/qualityLevel_drop").GetComponent<Dropdown> ().value);
	}

	public void SaveVideo(){
		Transform videoPanel = optionsMenu.transform.Find ("video_pnl");
		PlayerPrefs.SetInt ("videoQuality", videoPanel.Find ("qualityLevel_drop").GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt ("screenResolution", videoPanel.Find ("resolution_drop").GetComponent<Dropdown> ().value);
		PlayerPrefs.SetInt ("textureQuality", videoPanel.Find("textureQuality_drop").GetComponent<Dropdown> ().value);
		PlayerPrefs.SetInt ("shadowQuality", videoPanel.Find("shadowResolution_drop").GetComponent<Dropdown> ().value);
		PlayerPrefs.SetInt ("vsync", videoPanel.Find("vSync_drop").GetComponent<Dropdown> ().value);
		PlayerPrefs.SetInt ("antiAliasing", videoPanel.Find("aa_drop").GetComponent<Dropdown> ().value);
		PlayerPrefs.SetInt ("aniso", videoPanel.Find("aniso_drop").GetComponent<Dropdown> ().value);
		//PlayerPrefs.SetFloat ("brightness", videoPanel.Find ("brightness_sli").GetComponent<Slider> ().value);
	}

	public void LoadVideo(){
		Transform videoPanel = optionsMenu.transform.Find ("video_pnl");
		videoPanel.Find ("qualityLevel_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("videoQuality");
		videoPanel.Find ("resolution_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("screenResolution");
		videoPanel.Find ("textureQuality_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("textureQuality");
		videoPanel.Find ("shadowResolution_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("shadowQuality");
		videoPanel.Find ("vSync_drop").GetComponent<Dropdown> ().value = PlayerPrefs.GetInt ("vsync");
		videoPanel.Find("aa_drop").GetComponent<Dropdown> ().value =  PlayerPrefs.GetInt ("antiAliasing");
		videoPanel.Find("aniso_drop").GetComponent<Dropdown> ().value =  PlayerPrefs.GetInt ("aniso");
		//videoPanel.Find ("brightness_sli").GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("brightness");
	}

	public void UpdateValues(Transform videoPanel) {
		videoPanel.Find ("textureQuality_drop").GetComponent<Dropdown> ().value = -QualitySettings.masterTextureLimit + 3;

		switch (QualitySettings.shadowResolution) {
	        case(ShadowResolution.Low):
		        videoPanel.Find ("shadowResolution_drop").GetComponent<Dropdown> ().value = 0;
		        break;
	        case(ShadowResolution.Medium):
		        videoPanel.Find ("shadowResolution_drop").GetComponent<Dropdown> ().value = 1;
		        break;
	        case(ShadowResolution.High):
		        videoPanel.Find ("shadowResolution_drop").GetComponent<Dropdown> ().value = 2;
		        break;
	        case(ShadowResolution.VeryHigh):
		        videoPanel.Find ("shadowResolution_drop").GetComponent<Dropdown> ().value = 3;
		        break;
		}

		videoPanel.Find ("vSync_drop").GetComponent<Dropdown> ().value = QualitySettings.vSyncCount;
		videoPanel.Find ("aa_drop").GetComponent<Dropdown> ().value = QualitySettings.antiAliasing;
		switch (QualitySettings.anisotropicFiltering) {
	        case(AnisotropicFiltering.Disable):
		        videoPanel.Find ("aniso_drop").GetComponent<Dropdown> ().value = 0;
		        break;
	        case(AnisotropicFiltering.Enable):
		        videoPanel.Find ("aniso_drop").GetComponent<Dropdown> ().value = 1;
		        break;
	        case(AnisotropicFiltering.ForceEnable):
		        videoPanel.Find ("aniso_drop").GetComponent<Dropdown> ().value = 2;
		        break;
		}
	}

	public void SetFullScreen(bool value){
        Screen.fullScreen = value;
	}

	public void GetTextureQuality(int value){
		QualitySettings.masterTextureLimit = (value - 3) * -1;
	}

	public void GetShadowQuality(int value){
		switch (value) {
	        case(0):
		        QualitySettings.shadowResolution = ShadowResolution.Low;
		        break;
	        case(1):
		        QualitySettings.shadowResolution = ShadowResolution.Medium;
		        break;
	        case(2):
		        QualitySettings.shadowResolution = ShadowResolution.High;
		        break;
	        case(3):
		        QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
		        break;
		}
	}

	public void GetVsync(int value){
        QualitySettings.vSyncCount = value;
	}

	public void GetAntiAliasing(int value){
		if (value == 0)
			QualitySettings.antiAliasing = value;
		else
			QualitySettings.antiAliasing = (int)Math.Pow (2,value);	// 0,2,4,8.
	}

	public void GetAniso(int value){
		switch (value) {
		    case(0):
			    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			    break;
		    case(1):
			    QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
			    break;
		    case(2):
			    QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
			    break;
		}
	}
    // Создает drop down лист для объекта resolutions.
    public void CreateResolutionArray(){	
		List < Dropdown.OptionData> options = new List<Dropdown.OptionData> (0);
		for (int i = 10; i < UnityEngine.Screen.resolutions.Length; i++)
			options.Add (new Dropdown.OptionData (UnityEngine.Screen.resolutions [i].width.ToString () + " x " + UnityEngine.Screen.resolutions [i].height.ToString ()));
		optionsMenu.transform.Find ("video_pnl/resolution_drop").GetComponent<Dropdown> ().AddOptions (options);
	}
    // Устанавливает разрешение экрана равным значению в drop down объекте.
    public void SetResolution(GameObject dropDown){	
		int value = dropDown.GetComponent<Dropdown> ().value;
		Screen.SetResolution (UnityEngine.Screen.resolutions [10 + value].width, UnityEngine.Screen.resolutions [10 + value].height, true);
	}
}
