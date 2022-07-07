using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamesAudioScript : MonoBehaviour
{
    public AudioSource combatMusic;
    public AudioSource ambientMusic;
    public AudioSource deathMusic;
    public AudioSource lowHeathAlert;
    public AudioSource playerLoosesAnnouncement;
    public AudioSource swordUnsheathe;
    public AudioSource levelUpGeneric;
    public AudioSource levelHealth;
    public AudioSource levelEnergy;
    public AudioSource levelDamage;

    /*
    public AudioClip combatMusic;
    public AudioClip ambientMusic;
    public AudioClip deathMusic;
    public AudioClip lowHeathAlert;
    public AudioClip playerloosesAnnouncement;
    public AudioClip swordUnsheathe;
    public AudioClip levelUpGeneric;
    public AudioClip levelHealth;
    public AudioClip levelEnergy;
    public AudioClip levelDamage;
    */
    
    //private bool playAudio = false;

    public void StopAmbientMusic()
    {
        ambientMusic.Stop();
    }
    public void PlayAmbientMusic()
    {
        ambientMusic.Play();
    }
    public void PlayCombatMusic()
    {
        combatMusic.Play();
    }
    public void StopCombatMusic()
    {
        combatMusic.Stop();
    }
    public void PlayDeathSounds()
    {
        deathMusic.Play();
        playerLoosesAnnouncement.Play();
    }
    public void PlayerLevelUpAudio()
    {
        levelUpGeneric.Play();
    }
    public void PlayerLevelUpHealthAudio()
    {
        levelHealth.Play();
    }
    public void PlayerLevelUpEnergyAudio()
    {
        levelEnergy.Play();
    }
    public void PlayerLevelUpDamageAudio()
    {
        levelDamage.Play();
    }
    public void PlayerSelectsAttackAudio()
    {
        swordUnsheathe.Play();
    }
    public void PlayLowHelthAlert()
    {
        lowHeathAlert.Play();
    }

}
