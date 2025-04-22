using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSettingDialogMenu : MonoBehaviour
{
    public Button BtnResume;
    public Slider SliderMusic;
    public Slider SliderSounds;

    protected virtual void Start()
    {
        SliderMusic.value = AudioManager.Ins.MusicVolume;
        SliderSounds.value = AudioManager.Ins.SfxVolume;
        BtnResume.onClick.AddListener(() => HandleBtnResume());
    }

    protected virtual void Update()
    {
        SliderMusic.onValueChanged.AddListener(value => AudioManager.Ins.HandleSliderValue(value, true));
        SliderSounds.onValueChanged.AddListener(value => AudioManager.Ins.HandleSliderValue(value, false));
    }

    private void HandleBtnResume()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        gameObject.SetActive(false);
    }
}
