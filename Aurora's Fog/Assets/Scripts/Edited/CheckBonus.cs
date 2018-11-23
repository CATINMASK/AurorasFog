using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Checks if player has any bonuses (вешается на объект который является бонусным).
public class CheckBonus : MonoBehaviour
{
    public bool checkAlice;         // Is Alice available?
    public bool checkPoses;         // Is additional poses available?
    public Sprite lockSprite;       // Lock sprite for the locked poses.

    private void Update()
    {
        if (checkAlice && PlayerPrefs.GetInt("addAlice") != 1)
            gameObject.SetActive(false);
        if (checkPoses && PlayerPrefs.GetInt("additionalPoses") != 1)
            GetComponent<Image>().sprite = lockSprite;
    }
}
