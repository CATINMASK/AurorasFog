using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour {

    public string currentState;
    private GameObject player;
    private GameObject playerCamera;
    private string previousState;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerCamera = GameObject.FindWithTag("MainCamera");

        SetState("game");
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("additionalAttributes") == 1)
            GameObject.FindWithTag("Player").GetComponent<PlayerData>().points += 6;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(1);
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == "game")
                SetState("pause");
            else if (currentState == "pause")
                SetState("game");
            else if (currentState == "sex")
                SetState("poses");
            else if (currentState == "poses")
                SetState("sex");
            else if (currentState == "sexLilith")
                SetState("lilithPoses");
            else if (currentState == "lilithPoses")
                SetState("sexLilith");
        }
    }

    public void SetState(string state)
    {
        player.GetComponent<CharacterController>().enabled = true;
        GameObject.Find("pauseUI").transform.Find("buttons_pnl").gameObject.SetActive(false);
        GameObject.Find("poses_pnl").transform.Find("alice_pnl").gameObject.SetActive(false);
        GameObject.Find("poses_pnl").transform.Find("lilith_pnl").gameObject.SetActive(false);
        player.GetComponent<FPController>().canControl = true;
        playerCamera.GetComponent<FPInterface>().CursorState(true);
        Time.timeScale = 1f;

        switch (state)
        {
            case "game":
                currentState = "game";
                player.GetComponent<FPController>().canControl = true;
                player.transform.Find("Male").GetComponent<Animator>().enabled = true;
                player.transform.Find("Male").GetComponent<Animator>().SetBool("customization", false);
                playerCamera.GetComponent<FPInterface>().CursorState(true);
                playerCamera.GetComponent<FPInterface>().enable = true;

                break;
            case "pause":
                currentState = "pause";
                Time.timeScale = 0f;

                player.GetComponent<FPController>().canControl = false;
                //player.transform.Find("Male").GetComponent<Animator>().enabled = false;
                playerCamera.GetComponent<FPInterface>().CursorState(false);

                GameObject.Find("pauseUI").transform.Find("buttons_pnl").gameObject.SetActive(true);

                break;
            case "cutscene":
                currentState = "cutscene";

                //player.GetComponent<CharacterController>().enabled = false;
                player.GetComponent<FPController>().canControl = false;
                playerCamera.GetComponent<FPInterface>().range = 0;
                //player.transform.Find("Male").GetComponent<Animator>().enabled = true;

                break;
            case "customization":

                currentState = "customization";

                player.GetComponent<FPController>().canControl = false;
                player.transform.Find("Male").GetComponent<Animator>().SetBool("customization", true);
                playerCamera.GetComponent<FPInterface>().CursorState(false);
                playerCamera.GetComponent<FPInterface>().enable = false;

                player.transform.position = new Vector3(4f, 0f, 0f);
                player.transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));

                break;
            case "dialogue":

                currentState = "dialogue";

                break;
            case "sex":

                currentState = "sex";
                //player.GetComponent<FPController>().canControl = false;
                player.GetComponent<CharacterController>().enabled = false;
                playerCamera.GetComponent<FPCamera>().lockX = true;

                break;
            case "sexLilith":

                currentState = "sexLilith";
                //player.GetComponent<FPController>().canControl = false;
                player.GetComponent<CharacterController>().enabled = false;
                playerCamera.GetComponent<FPCamera>().lockX = true;

                break;
            case "poses":

                currentState = "poses";
                player.GetComponent<CharacterController>().enabled = false;
                playerCamera.GetComponent<FPInterface>().CursorState(false);
                player.GetComponent<FPController>().canControl = false;
                GameObject.Find("poses_pnl").transform.Find("alice_pnl").gameObject.SetActive(true);

                break;
            case "lilithPoses":

                currentState = "lilithPoses";
                player.GetComponent<CharacterController>().enabled = false;
                playerCamera.GetComponent<FPInterface>().CursorState(false);
                player.GetComponent<FPController>().canControl = false;
                GameObject.Find("poses_pnl").transform.Find("lilith_pnl").gameObject.SetActive(true);

                break;
        }
    }

    public IEnumerator SetStateWithDelay(float delay, string state)
    {
        yield return new WaitForSeconds(delay);
        SetState(state);
    }

    public void SetLevel(int value)
    {
        SceneManager.LoadScene(value);
        Time.timeScale = 1.0f;
    }
    IEnumerator SetGame()
    {
        yield return new WaitForSeconds(11.4f);
        SetState("game");
    }

}
