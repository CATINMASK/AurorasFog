using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Вешается на кнопку и позволяет ей проигрывать звук при наведении или нажатии на неё. Пока реализовано только для MENU.
public class ButtonSound : MonoBehaviour, IPointerEnterHandler {

    private AudioClip overSound;
    private AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        overSound = Resources.Load("Sounds/buttonOnEnter") as AudioClip;
        clickSound = Resources.Load("Sounds/buttonOnClick") as AudioClip;
        audioSource = GameObject.Find("soundButton").GetComponent<AudioSource>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayOverSound();
    }

    public void PlayOverSound()
    {
        audioSource.Stop();
        audioSource.clip = overSound;
        audioSource.Play();
    }

    public void PlayClickSound()
    {
        audioSource.Stop();
        audioSource.clip = clickSound;
        audioSource.Play();
    }
}
