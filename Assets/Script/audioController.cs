using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip[] hurtArray;
    public AudioClip[] dyingArray;
    public AudioClip[] defenceHitArray;
    public AudioClip[] playerAttackingArray;
    public AudioClip[] playerEndArray;
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

    public void PlayerAttackingSound()
    {
        playedSound.PlayOneShot(playerAttackingSound());
    }

    AudioClip playerAttackingSound()
    {
        return playerAttackingArray[Random.Range(0, playerAttackingArray.Length)];
    }

    public void PlayerEndSound()
    {
        playedSound.PlayOneShot(playerEndSound());
    }

    AudioClip playerEndSound()
    {
        return playerEndArray[Random.Range(0, playerEndArray.Length)];
    }

      public void DefenceHit()
    {
        playedSound.PlayOneShot(playerDefenceHitSound());
    }

    AudioClip playerDefenceHitSound()
    {
        return defenceHitArray[Random.Range(0, defenceHitArray.Length)];
    }

}
