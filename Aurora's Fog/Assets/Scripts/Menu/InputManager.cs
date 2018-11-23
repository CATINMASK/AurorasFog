using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Stores all keys and work with them.
public class InputManager : MonoBehaviour {

	public GameObject optionsMenu;
	public float mouseSensitivity = 120f;
	public InputKey[] gameKeys = new InputKey[10];

	public void SetKeysDefault(){
        GameObject.Find("Canvas").transform.Find("options_pnl").Find("controls_pnl").Find("mouseSensitivity_sli").gameObject.GetComponent<Slider>().value = mouseSensitivity;
        gameKeys [0] = new InputKey (KeyCode.W, "forward");
		gameKeys [1] = new InputKey (KeyCode.S, "backward");
		gameKeys [2] = new InputKey (KeyCode.A, "left");
		gameKeys [3] = new InputKey (KeyCode.D, "right");
        gameKeys [4] = new InputKey (KeyCode.CapsLock, "walkJogging");
        gameKeys [5] = new InputKey (KeyCode.LeftShift, "sprint");
        gameKeys [6] = new InputKey (KeyCode.Space, "jump");
		gameKeys [7] = new InputKey (KeyCode.LeftControl, "crouch");
		gameKeys [8] = new InputKey (KeyCode.E, "use");
		gameKeys [9] = new InputKey (KeyCode.Tab, "inventory");

        mouseSensitivity = 120f;
        optionsMenu.transform.Find("controls_pnl/mouseSensitivity_sli").GetComponent<Slider>().value = mouseSensitivity;
    }

	public void SaveKeys() {
		PlayerPrefs.SetInt("forward", (int)gameKeys[0].inputButton);
		PlayerPrefs.SetInt("backward", (int)gameKeys[1].inputButton);
		PlayerPrefs.SetInt("left", (int)gameKeys[2].inputButton);
		PlayerPrefs.SetInt("right", (int)gameKeys[3].inputButton);

        PlayerPrefs.SetInt("walkJogging", (int)gameKeys[4].inputButton);
        PlayerPrefs.SetInt("sprint", (int)gameKeys[5].inputButton);
        PlayerPrefs.SetInt("jump", (int)gameKeys[6].inputButton);
		PlayerPrefs.SetInt("crouch", (int)gameKeys[7].inputButton);

		PlayerPrefs.SetInt("use", (int)gameKeys[8].inputButton);
		PlayerPrefs.SetInt("inventory", (int)gameKeys[9].inputButton);

		// Save sensitivity
		PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
	}

	public void LoadKeys() {
		gameKeys[0].inputButton = (KeyCode)PlayerPrefs.GetInt ("forward");
		gameKeys[1].inputButton = (KeyCode)PlayerPrefs.GetInt ("backward");
		gameKeys[2].inputButton = (KeyCode)PlayerPrefs.GetInt ("left");
		gameKeys[3].inputButton = (KeyCode)PlayerPrefs.GetInt ("right");

        gameKeys[4].inputButton = (KeyCode)PlayerPrefs.GetInt("walkJogging");
        gameKeys[5].inputButton = (KeyCode)PlayerPrefs.GetInt("sprint");
        gameKeys[6].inputButton = (KeyCode)PlayerPrefs.GetInt ("jump");
		gameKeys[7].inputButton = (KeyCode)PlayerPrefs.GetInt ("crouch");

		gameKeys[8].inputButton = (KeyCode)PlayerPrefs.GetInt ("use");
		gameKeys[9].inputButton = (KeyCode)PlayerPrefs.GetInt ("inventory");

		mouseSensitivity = PlayerPrefs.GetFloat ("mouseSensitivity");
        GameObject.Find("Canvas").transform.Find("options_pnl").Find("controls_pnl").Find("mouseSensitivity_sli").gameObject.GetComponent<Slider>().value = mouseSensitivity;
    }

	public void GetmouseSensitivity(){
		mouseSensitivity = optionsMenu.transform.Find ("controls_pnl/mouseSensevity_sli").GetComponent<Slider> ().value;
	}

	public void SetmouseSensitivity(float value){
        //optionsMenu.transform.Find("controls_pnl/mouseSensitivity_sli").GetComponent<Slider>().value = value;
        mouseSensitivity = value;
        PlayerPrefs.SetFloat("mouseSensitivity", value);
    }
}

[Serializable]
public class InputKey{
	public KeyCode inputButton;
	public string inputName;
	public InputKey(KeyCode key, string name){
		inputButton = key;
		inputName = name;
	}
}