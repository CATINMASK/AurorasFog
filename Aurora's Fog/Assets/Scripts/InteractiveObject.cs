using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {

    public string type;
    public string objectName;
    public bool used;

    public void UseObject(string objectType)
    {
        if (objectType == "")
            objectType = type;
        switch (type)
        {
            case "crate":   // take item from the crate
                if (!used)
                {
                    GetComponent<Crate>().OpenCrate();

                    /*
                    GetComponent<Animator>().Play("open");
                    GetComponent<AudioSource>().Play();
                    used = true;
                    //type = "closeCrate";
                    */
                }
                break;
            case "closeCrate":
                if (used)
                {
                    GetComponent<Animator>().Play("close");
                    GetComponent<AudioSource>().Play();
                    used = false;
                    type = "crate";
                }
                break;
            case "customization":
                GetComponent<Customization>().StartCustomization();
                //GameObject.Find("GameStateMachine").GetComponent<GameStateMachine>().SetState("customization");
                //playerCamera.GetComponent<Camera>().enabled = false;
                //mirrorCamera.GetComponent<Camera>().enabled = true;
                //GameObject.Find("interaction_txt").GetComponent<Text>().text = "";
                break;
        }

    }
}
