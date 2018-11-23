using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Main script of the game menu. Loads levels, set settings, closes panels and so on...
public class MainMenu : MonoBehaviour {

    public bool inMenu;
    static byte menuState;

    void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //if(gameObject.name != "optionsMenu" && inMenu)
        //	OpenMainMenu (GameObject.FindWithTag("Canvas").transform.Find ("main_pnl").gameObject);
    }

    void Update() {
        if (inMenu) {
            /*
			if (menuState == 0 && Input.GetKeyDown (KeyCode.Escape) && !GameObject.FindWithTag("Canvas").transform.Find ("main_pnl/quit_pnl").gameObject.activeSelf)
				OpenQuitPanel (GameObject.FindWithTag("Canvas").transform.Find ("main_pnl/quit_pnl").gameObject);
			else if (Input.GetKeyDown (KeyCode.Escape) && GameObject.FindWithTag("Canvas").transform.Find ("main_pnl/quit_pnl").gameObject.activeSelf)
				CloseQuitPanel (GameObject.FindWithTag("Canvas").transform.Find ("main_pnl/quit_pnl").gameObject);
            */
            if (menuState == 1 && Input.GetKeyDown(KeyCode.Escape))
                OpenMenuOrSavePanel(GameObject.FindWithTag("Canvas").transform.Find("main_pnl").gameObject);
        }
    }

    public void SetAllSettingsSaved() {
        GameSettings gameSettings = GameObject.FindWithTag("Options").GetComponent<GameSettings>();
        gameSettings.generalSaved = true;
        gameSettings.optionsSaved = true;
        gameSettings.videoSaved = true;
    }

    public void SetGeneralSaved(bool saved) {
        GameSettings gameSettings = GameObject.FindWithTag("Options").GetComponent<GameSettings>();
        gameSettings.generalSaved = saved;
    }
    public void SetControlsSaved(bool saved) {
        GameSettings gameSettings = GameObject.FindWithTag("Options").GetComponent<GameSettings>();
        gameSettings.optionsSaved = saved;
    }
    public void SetVideoSaved(bool saved) {
        GameSettings gameSettings = GameObject.FindWithTag("Options").GetComponent<GameSettings>();
        gameSettings.videoSaved = saved;
    }

    // Main menu methods:

    public void NewGame() {
        SceneManager.LoadScene(1);
    }
    //  Starts new game or loads bonus panel (based on "isLogged")
    public void NewGameOrBonus() {
        if (PlayerPrefs.GetInt("isLogged") == 1)
        {
            GameObject.Find("Canvas").transform.Find("main_pnl").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("bonuses_pnl").gameObject.SetActive(true);
        }
        else
            LoadLevel(1);
        //SceneManager.LoadScene(1);
    }
    public void LoadGame() {
        print("this should load game");
    }
    public void LoadMenu() {
        SceneManager.LoadScene(0);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        GameObject.Find("Canvas").transform.Find("main_pnl").gameObject.SetActive(false);           // Deactivates main_pnl
        GameObject.Find("Canvas").transform.Find("loadingScreen_pnl").gameObject.SetActive(true);   // Activates the loadingScreen_pnl

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            GameObject.Find("Canvas").transform.Find("loadingScreen_pnl").Find("loading_slider").GetComponent<Slider>().value = progress;
            yield return null;
        }
    }

    public void OpenOptionsMenu(GameObject optionsMenu) {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
        menuState = 1;
    }
    public void OpenQuitPanel(GameObject quitPanel) {
        quitPanel.SetActive(true);
        quitPanel.transform.Find("exit_btn").GetComponent<Button>().Select();
    }
    public void CloseQuitPanel(GameObject quitPanel) {
        quitPanel.SetActive(false);
        GameObject.FindWithTag("Canvas").transform.Find("mainMenu/buttons_pnl/newGame_btn").GetComponent<Button>().Select();
    }
    public void QuitGame() {
        print("Quited the game");
        Application.Quit();
    }

    // Options menu methods:

    public void OpenMenuOrSavePanel(GameObject mainMenu) {
        GameSettings gameSettings = GameObject.FindWithTag("Options").GetComponent<GameSettings>();
        if (gameSettings.generalSaved && gameSettings.optionsSaved && gameSettings.videoSaved) {
            OpenMainMenu(mainMenu);
        }
        else
            OpenSavePanel(transform.Find("saveAll_pnl").gameObject);
    }

    public void OpenMainMenu(GameObject mainMenu) {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
        menuState = 0;
        //mainMenu.transform.Find("buttons_pnl").Find("newGame_btn").GetComponent<Button> ().Select ();
    }
    public void OpenSettingsPanel(int panelNumber) {
        if (panelNumber == 0) {
            transform.Find("general_pnl").gameObject.SetActive(true);
            transform.Find("controls_pnl").gameObject.SetActive(false);
            transform.Find("video_pnl").gameObject.SetActive(false);
        }
        if (panelNumber == 1) {
            transform.Find("general_pnl").gameObject.SetActive(false);
            transform.Find("controls_pnl").gameObject.SetActive(true);
            transform.Find("video_pnl").gameObject.SetActive(false);
        }
        if (panelNumber == 2) {
            transform.Find("general_pnl").gameObject.SetActive(false);
            transform.Find("controls_pnl").gameObject.SetActive(false);
            transform.Find("video_pnl").gameObject.SetActive(true);
        }
        if (panelNumber == 3) {
            transform.Find("general_pnl").gameObject.SetActive(false);
            transform.Find("controls_pnl").gameObject.SetActive(false);
            transform.Find("video_pnl").gameObject.SetActive(false);
        }
    }

    public void OpenSavePanel(GameObject savePanel) {
        savePanel.SetActive(true);
    }

    public void CloseSavePanel(GameObject savePanel) {
        savePanel.SetActive(false);
    }
}

    /*      MMain.cs code (LEGACY)
     * 
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
    */