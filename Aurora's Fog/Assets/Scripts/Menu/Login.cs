using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Allows supported player to get logined and get ingame bonuses.
public class Login : MonoBehaviour {

    public string loginSite;
    public InputField mailField;
    public InputField passwordField;
    public Button submitButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("isLogged") == 1)
            GameObject.Find("show_btn").GetComponent<Text>().text = PlayerPrefs.GetString("userRank");
    }

    private void Update()
    {
        VerifyInputs();
    }

    public void LoginMe()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("mail", mailField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW( loginSite, form);
        yield return www;

        Debug.Log(www.text);
        if (www.text != "")
        {
            PlayerPrefs.SetInt("isLogged", 1);
            PlayerPrefs.SetString("userRank", www.text);
            HideLoginPanel();
        }
        else
            GameObject.Find("show_btn").GetComponent<Text>().text = "Wrong email or password!";
    }

    public void VerifyInputs()
    {
        submitButton.interactable = (mailField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

    public void HideLoginPanel()
    {
        if (PlayerPrefs.GetString("userRank") != "")
        {
            GameObject.Find("show_btn").GetComponent<Text>().text = PlayerPrefs.GetString("userRank");
            GameObject.Find("mail_field").GetComponent<InputField>().text = "";
            GameObject.Find("password_field").GetComponent<InputField>().text = "";
            GameObject.Find("Canvas").GetComponent<Animator>().Play("HideLoginPanel");
        }
    } 
}
