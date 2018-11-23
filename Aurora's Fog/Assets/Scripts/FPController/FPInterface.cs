using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPInterface : MonoBehaviour {

    public GameObject allUI;
    public float range = 2.0f;                      // range from player to the object.
    public bool enable = true;

    private void Awake() {
    }

    private void Update() {
        if (enable)
            Interact();
    }

    void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        int ignoreLayer = 1 << 2;
        ignoreLayer = ~ignoreLayer;

        if (Physics.Raycast(ray, out hit, range, ignoreLayer))
        {
            if (hit.collider.tag == "NPC")
            {
                ShowText("[" + ((KeyCode)PlayerPrefs.GetInt("use")).ToString() + "] TALK");

                if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("use")))
                {
                    // L E G A C Y
                    if (hit.collider.tag == "NPC" && hit.collider.name == "Alice")
                        GameObject.Find("dialogueManager").GetComponent<Dialogue>().PrintText(0);
                    else
                        GameObject.Find("dialogueManager").GetComponent<Dialogue>().PrintText(1);
                }
            }
            else if (hit.collider.tag == "InteractiveObject")
            {
                InteractiveObject interactiveObjST = hit.collider.GetComponent<InteractiveObject>();
                if (interactiveObjST.type == "crate")
                {
                    if (hit.collider.GetComponent<Crate>().opened)
                    {
                        //ShowObjectPanel(true, interactiveObjST.objectName);
                        ShowObjectPanel(hit.collider.transform);
                        ShowText("");
                        if (Input.GetKeyDown(KeyCode.Q))    // For closing crate
                        {
                            hit.collider.GetComponent<Crate>().CloseCrate();
                        }
                    }
                    else
                    {
                        ShowObjectPanel(null);
                        ShowText("[" + ((KeyCode)PlayerPrefs.GetInt("use")).ToString() + "] OPEN");
                        if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("use")))
                        {
                            hit.collider.GetComponent<Crate>().OpenCrate();
                        }
                    }
                }
                else
                {
                    ShowText("[" + ((KeyCode)PlayerPrefs.GetInt("use")).ToString() + "] USE");
                    if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("use")))
                        hit.collider.GetComponent<InteractiveObject>().UseObject(null);
                }

                /*
                if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("use")))
                {
                    hit.collider.GetComponent<InteractiveObject>().UseObject();

                }
                */
            }
            else
            {
                ShowObjectPanel(null);
                ShowText("");
            }
        }
        else
        {
            allUI.transform.Find("gameUI").Find("object_pnl").gameObject.SetActive(false);
            ShowText("");
        }
    }

    public void ShowText(string value)
    {
        GameObject.Find("interaction_txt").GetComponent<Text>().text = value;
    }

    public void CursorState(bool locked)
    {
        bool cursorIsLocked = locked;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorIsLocked = true;
        }

        if (cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ShowObjectPanel(Transform crate)   // Когда наводишь курсором на ящик (объект с предметами)
    {
        if (crate != null)
        {
            GameObject objectPanel = allUI.transform.Find("gameUI").Find("object_pnl").gameObject;
            Crate crateST = crate.GetComponent<Crate>();
            InteractiveObject interactiveObjectST = crate.GetComponent<InteractiveObject>();
            bool show = crateST.opened;
            if (show)
            {
                //objectPanel.SetActive(show);
                objectPanel.transform.Find("name").GetComponent<Text>().text = interactiveObjectST.objectName;
                objectPanel.transform.Find("use1").GetComponent<Text>().text = "[" + ((KeyCode)PlayerPrefs.GetInt("use")).ToString() + "] USE";
                if (crateST.items.Count > 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        objectPanel.transform.GetChild(i).GetComponent<Text>().text = crateST.items[i].itemName;
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        objectPanel.transform.GetChild(i).GetComponent<Text>().text = "";
                    }
                }
            }
            objectPanel.SetActive(show);
        }

    }
}
