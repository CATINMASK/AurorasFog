using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexSounds : MonoBehaviour {

    public AudioClip[] moans;
    public AudioClip lastMoan;
    public AudioClip[] slaps;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "dick")
        {
            audioSource.Stop();
            audioSource.clip = slaps[Random.Range(0, slaps.Length)];
            audioSource.Play();
        }
    }

    public void StartMoan()
    {
        StartCoroutine(startMoaning());
    }

    public void LastMoan()
    {
        StopMoan();
        AudioSource myAudio = GetComponent<AudioSource>();
        myAudio.Stop();
        myAudio.clip = lastMoan;
        myAudio.Play();
    }

    public void StopMoan()
    {
        StopAllCoroutines();
    }

    IEnumerator startMoaning()
    {
        AudioSource myAudio = GetComponent<AudioSource>();
        yield return new WaitForSeconds(Random.Range(2f, 4f));
        myAudio.Stop();
        myAudio.clip = moans[Random.Range(0, moans.Length)];
        myAudio.Play();
        myAudio.loop = false;
        StartCoroutine(startMoaning());
    }
}
