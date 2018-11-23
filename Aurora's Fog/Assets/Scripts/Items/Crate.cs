using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Вешать на объект, который должен быть ящиком.
public class Crate : MonoBehaviour {

    public bool opened;         // Is it opened? For playing open/close animations and to show diffirent UI.
    public List<Item> items;    // List of the current crate items.

    public void OpenCrate()
    {
        if (!opened)
        {
            opened = true;
            GetComponent<Animator>().Play("open");
            GetComponent<AudioSource>().Play();
        }
    }
    public void CloseCrate()
    {
        if (opened)
        {
            opened = false;
            GetComponent<Animator>().Play("close");
            GetComponent<AudioSource>().Play();
        }
    }

}
