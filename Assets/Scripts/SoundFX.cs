using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] sfx = new AudioClip[3];

    public void PlaySFX(int index)
    {
        src.clip = sfx[index];
        src.Play();
    }

}
