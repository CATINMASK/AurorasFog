using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributePrinter : MonoBehaviour {

    PlayerData PlayerDataST;                    // Player Data script.
    public string attributeVal;                 // Value of the attribute (name of the object).

    private void Awake()
    {
        PlayerDataST = GameObject.Find("Player").GetComponent<PlayerData>();
        attributeVal = gameObject.name;         // Attribute value = name of the gameobject.
        UpdateText(attributeVal);
    }

    private void Update()
    {
        if (attributeVal == "STA")              // If it's Stamina attribute then update it every frame.
            UpdateText();
    }

    public void UpdateText()                    // Use this for UI buttons (w/o any arguments).
    {
        UpdateText(attributeVal);
    }
    void UpdateText(string attribute)
    {
        Text uiText = gameObject.GetComponent<Text>();

        switch (attribute)
        {
            case "STR":
                uiText.text = PlayerDataST.GetStrength().ToString();
                break;
            case "END":
                uiText.text = PlayerDataST.GetEndurance().ToString();
                break;
            case "AGI":
                uiText.text = PlayerDataST.GetAgility().ToString();
                break;
            case "PER":
                uiText.text = PlayerDataST.GetPerception().ToString();
                break;
            case "INT":
                uiText.text = PlayerDataST.GetIntelligence().ToString();
                break;
            case "CHA":
                uiText.text = PlayerDataST.GetCharisma().ToString();
                break;
            case "HP":
                uiText.text = PlayerDataST.health.ToString();
                break;
            case "MP":
                uiText.text = PlayerDataST.mana.ToString();
                break;
            case "STA":
                gameObject.GetComponent<Image>().fillAmount = PlayerDataST.stamina / 60;
                break;
            case "points":
                uiText.text = PlayerDataST.points.ToString();
                break;
        }
    }
}
