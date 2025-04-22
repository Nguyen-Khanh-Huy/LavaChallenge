using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("AudioSource: ")]
    public AudioSource AusMusic;
    public AudioSource AusSFX;

    [Header("Volume Settings:")]
    [Range(0f, 1f)] public float MusicVolume = 1f;
    [Range(0f, 1f)] public float SfxVolume = 1f;

    [Header("AudioClip MUSIC: ")]
    public AudioClip Music;

    [Header("AudioClip SFX: ")]
    public AudioClip SfxCollect;
    public AudioClip SfxBtnClick;

    private void Start()
    {
        PlayMusic(Music);
    }

    public void PlayMusic(AudioClip music)
    {
        AusMusic.clip = music;
        AusMusic.loop = true;
        AusMusic.Play();
    }
    public void PlaySFX(AudioClip sfx)
    {
        AusSFX.clip = sfx;
        AusSFX.loop = false;
        AusSFX.Play();
    }

    public void HandleSliderValue(float volumeSlider, bool isMusic)
    {
        if (isMusic)
        {
            MusicVolume = volumeSlider;
            AusMusic.volume = MusicVolume;
        }
        else
        {
            SfxVolume = volumeSlider;
            AusSFX.volume = SfxVolume;
        }
    }
}