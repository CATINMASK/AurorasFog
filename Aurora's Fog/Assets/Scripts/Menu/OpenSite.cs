using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSite : MonoBehaviour {

    public void OpenPatreon()
    {
        Application.OpenURL("https://www.patreon.com/aurorasfog");
    }
    public void OpenBlogger()
    {
        Application.OpenURL("https://aurorasfog.blogspot.com/");
    }
    public void OpenYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC3vfn-OqQ6HGsHC8oVkERrQ");
    }
    public void OpenVK()
    {
        Application.OpenURL("https://vk.com/aurorasfog");
    }
}