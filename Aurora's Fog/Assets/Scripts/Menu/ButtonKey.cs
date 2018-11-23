using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// Updating key values in controls settings.
public class ButtonKey : MonoBehaviour, IPointerClickHandler {

	private bool selected;

	void OnEnable() {
		InvokeRepeating ("UpdateButtonText", 0f, 0.25f);
	}

	void OnDisable() {
		CancelInvoke ("UpdateButtonText");
	}

	void Update(){
		if(selected)
			DetectKeyPress ();
	}
		
	public void OnPointerClick( PointerEventData eventData ){
		selected = true;
		GetComponent<Image> ().color = new Color (0.7f, 0.7f, 0.7f);
	}

	void UpdateButtonText(){
		InputKey[] keys = GameObject.FindWithTag ("Options").GetComponent<InputManager> ().gameKeys;
		for (int i = 0; i < keys.Length; i++)
			if (transform.GetChild (0).name == keys[i].inputName)
				transform.GetChild (0).GetComponent<Text> ().text = keys [i].inputButton.ToString ();
	}

	void CheckForKey(KeyCode key){
		InputKey[] keys = GameObject.FindWithTag ("Options").GetComponent<InputManager> ().gameKeys;
		for (int i = 0; i < keys.Length; i++) {
			if (key == keys [i].inputButton)
				keys [i].inputButton = KeyCode.None;
		}
	}

	void DetectKeyPress(){
		InputKey[] keys = GameObject.FindWithTag ("Options").GetComponent<InputManager> ().gameKeys;
		foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
			if (Input.GetKeyDown (kcode)) {
				selected = false;
				CheckForKey (kcode);
				for (int i = 0; i < keys.Length; i++)
					if (transform.GetChild (0).name == keys [i].inputName)
						keys [i].inputButton = kcode;
				gameObject.GetComponent<Image> ().color = new Color (1, 1, 1);
			}
	}
}
