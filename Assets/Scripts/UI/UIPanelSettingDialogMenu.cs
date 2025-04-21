using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSettingDialogMenu : MonoBehaviour
{
    public Button BtnResume;
    public Slider SliderMusic;
    public Slider SliderSounds;
    public float zzMusic;
    public float zzSounds;

    protected virtual void Start()
    {
        SliderMusic.value = 1;
        SliderSounds.value = 1;
        BtnResume.onClick.AddListener(() => HandleBtnResume());
    }

    protected virtual void Update()
    {
        SliderMusic.onValueChanged.AddListener(HandleSliderMusic);
        SliderSounds.onValueChanged.AddListener(HandleSliderSounds);
    }

    private void HandleSliderMusic(float volume)
    {
        zzMusic = volume;
    }

    private void HandleSliderSounds(float volume)
    {
        zzSounds = volume;
    }

    private void HandleBtnResume()
    {
        gameObject.SetActive(false);
    }
}
