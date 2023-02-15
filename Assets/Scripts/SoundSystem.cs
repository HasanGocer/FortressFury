using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [Header("Sound_Field")]
    [Space(10)]

    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioClip mainMusic, hit, finish, upgrade, coin;

    public void MainMusicPlay()
    {
        mainSource.clip = mainMusic;
        mainSource.Play();
        mainSource.volume = 70;
        mainSource.mute = false;
    }

    public void MainMusicStop()
    {
        mainSource.Stop();
        mainSource.volume = 0;
        mainSource.mute = true;
    }

    public void CallHitSound()
    {
        mainSource.PlayOneShot(hit);
    }
    public void CallFinishSound()
    {
        mainSource.PlayOneShot(finish);
    }
    public void CallUpgradeSound()
    {
        mainSource.PlayOneShot(upgrade);
    }
    public void CallCoinSound()
    {
        mainSource.PlayOneShot(coin);
    }
}
