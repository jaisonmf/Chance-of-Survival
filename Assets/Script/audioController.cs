using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip[] hurtArray;
    public AudioClip[] dyingArray;
    public AudioSource playedSound;
    public void Hurt()
    {
        playedSound.PlayOneShot(HurtSound());

    }

    AudioClip HurtSound()
    {
        return hurtArray[Random.Range(0, hurtArray.Length)];
    }

    public void Dying()
    {
        playedSound.PlayOneShot(DyingSound());

    }
    AudioClip DyingSound()
    {
        return dyingArray[Random.Range(0, dyingArray.Length)];
    }
}
