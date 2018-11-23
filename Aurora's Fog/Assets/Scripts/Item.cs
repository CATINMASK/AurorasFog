using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public bool canGet = true;  // If we can pick up the Item.
    public Sprite itemIcon;     // Shown in UI.
    public string itemName;
    public string itemInfo;     // Shown in UI.
    public byte itemNumber;      // 0 - food, 1 - key
    public float weight;
    public string type;
}
