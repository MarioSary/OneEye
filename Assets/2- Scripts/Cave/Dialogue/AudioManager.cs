using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource gunSound;

    public void SwordSound()
    {
        swordSound.Play();
    }

    public void GunSound()
    {
        gunSound.Play();
    }


    /*public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public AudioSource audioSource;

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }*/
}
