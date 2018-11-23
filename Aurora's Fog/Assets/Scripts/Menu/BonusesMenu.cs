using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Скрипт предназначен для распределения бонусов, если игрок залогинился.
public class BonusesMenu : MonoBehaviour {

    private int points = 0;                 // The amount of bonus points left
    public bool additionalAttributes;
    private bool addAlice;
    public bool additionalPoses;

    private void Awake() {
        points = SetPoints(PlayerPrefs.GetString("userRank"));  
    }

    private void Start() {
        CheckPoints();
    }

    public int SetPoints(string rank) {

        int points = 0;

        if (rank == "Lost soul")
            points = 1;
        else if (rank == "Guest")
            points = 2;
        else if (rank == "Citizen")
            points = 3;
        else if (rank == "Magician")
            points = 4;
        else if (rank == "Baron")
            points = 5;
        else if (rank == "King")
            points = 6;
        else if (rank == "Divine")
            points = 7;
        else if (rank == "Admin")
            points = 8;

        return points;
    }

    public void AdditionalAttributes(bool value)
    {
        if (value)
            points -= 1;
        else
            points += 1;
        additionalAttributes = value;
        CheckPoints();
    }
    public void AddAlice(bool value)
    {
        if (value)
            points -= 1;
        else
            points += 1;
        addAlice = value;
        CheckPoints();
    }
    public void AdditionalPoses(bool value)
    {
        if (value)
            points -= 2;
        else
            points += 2;
        additionalPoses = value;
        CheckPoints();
    }
    void CheckPoints()
    {
        if (!transform.Find("attributes_toggle").GetComponent<Toggle>().isOn)
            transform.Find("attributes_toggle").GetComponent<Toggle>().interactable = points >= 1;
        if (!transform.Find("alice_toggle").GetComponent<Toggle>().isOn)
            transform.Find("alice_toggle").GetComponent<Toggle>().interactable = points >= 1;
        if (!transform.Find("poses_toggle").GetComponent<Toggle>().isOn)
            transform.Find("poses_toggle").GetComponent<Toggle>().interactable = points >= 2;

        GameObject.Find("points_txt").GetComponent<Text>().text = points.ToString();
    }
    public void CloseBonusMenu()        // Закончить выбор бонусов и начать игру
    {
        PlayerPrefs.DeleteKey("additionalAttributes");
        PlayerPrefs.DeleteKey("addAlice");
        PlayerPrefs.DeleteKey("additionalPoses");

        if (additionalAttributes)
            PlayerPrefs.SetInt("additionalAttributes", 1);
        if (addAlice)
            PlayerPrefs.SetInt("addAlice", 1);
        if(additionalPoses)
            PlayerPrefs.SetInt("additionalPoses", 1);

        gameObject.SetActive(false);
    }
}
