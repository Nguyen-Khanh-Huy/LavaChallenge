using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelMenu : MonoBehaviour
{
    public Transform Content;
    public Button BtnSettingMenu;
    public Button BtnMusicOff;
    public Button BtnMusicOn;
    public UIPanelSettingDialogMenu UIPanelSettingDialogMenu;
    public LevelPrefab LevelPrefab;

    private void OnEnable()
    {
        UIManager.Ins.Player.gameObject.SetActive(false);
        StartCoroutine(WaitForLevelManager());
    }

    private void Start()
    {
        BtnSettingMenu.onClick.AddListener(() => HandleBtnSettingMenu());
        BtnMusicOff.onClick.AddListener(() => HandleBtnMusicOff());
        BtnMusicOn.onClick.AddListener(() => HandleBtnMusicOn());
    }

    private void HandleBtnSettingMenu()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        UIPanelSettingDialogMenu.gameObject.SetActive(true);
    }
    private void HandleBtnMusicOn()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        BtnMusicOn.gameObject.SetActive(false);

        if (AudioManager.Ins.AusMusic.isPlaying)
        {
            AudioManager.Ins.AusMusic.Pause();
            AudioManager.Ins.AusSFX.volume = 0f;
        }
        else
        {
            AudioManager.Ins.AusMusic.UnPause();
            AudioManager.Ins.AusSFX.volume = 1f;
        }
    }
    
    private void HandleBtnMusicOff()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        BtnMusicOn.gameObject.SetActive(true);

        if (AudioManager.Ins.AusMusic.isPlaying)
        {
            AudioManager.Ins.AusMusic.Pause();
            AudioManager.Ins.AusSFX.volume = 0f;
        }
        else
        {
            AudioManager.Ins.AusMusic.UnPause();
            AudioManager.Ins.AusSFX.volume = 1f;
        }
    }

    private IEnumerator WaitForLevelManager()
    {
        while (LevelManager.Ins == null || LevelManager.Ins.ListLevels == null)
        {
            yield return null;
        }

        foreach (Transform child in Content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < LevelManager.Ins.ListLevels.Count; i++)
        {
            int idx = i;
            LevelPrefab levelPb = Instantiate(LevelPrefab, Content.transform);
            levelPb.TxtNumbleLevel.text = LevelManager.Ins.ListLevelNumble[idx].ToString();
            levelPb.LockLevel.SetActive(!LevelManager.Ins.ListLevelUnlock[idx]);
            levelPb.BtnLevel.onClick.AddListener(() => HandleBtnLevel(idx, levelPb));
        }
    }

    private void HandleBtnLevel(int idx, LevelPrefab levelPb)
    {
        LevelManager.Ins.IdLevel = idx;
        if (levelPb.LockLevel.activeSelf) return;
        AudioManager.Ins.PlaySFX(AudioManager.Ins.SfxBtnClick);
        UIManager.Ins.UIPanelGamePlay.gameObject.SetActive(true);
        UIManager.Ins.Player.gameObject.SetActive(true);
        LevelManager.Ins.ListLevels[LevelManager.Ins.IdLevel].gameObject.SetActive(true);
        LevelManager.Ins.OnEnableGem(LevelManager.Ins.IdLevel);
        LevelManager.Ins.OnOffBGDown(true);
        gameObject.SetActive(false);
    }
}
