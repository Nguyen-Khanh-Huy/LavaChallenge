using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelSettingDialogGamePlay : UIPanelSettingDialogMenu
{
    public Button BtnRestart;
    public Button BtnHome;
    
    protected override void Start()
    {
        base.Start();
        BtnRestart.onClick.AddListener(() => HandleBtnRestart());
        BtnHome.onClick.AddListener(() => HandleBtnHome());
    }

    private void HandleBtnRestart()
    {
        gameObject.SetActive(false);
        LevelManager.Ins.ListLevels[LevelManager.Ins.IdLevel].gameObject.SetActive(false);
        LevelManager.Ins.ListLevels[LevelManager.Ins.IdLevel].gameObject.SetActive(true);
    }
    
    private void HandleBtnHome()
    {
        gameObject.SetActive(false);
        LevelManager.Ins.ListLevels[LevelManager.Ins.IdLevel].gameObject.SetActive(false);
        UIManager.Ins.UIPanelGamePlay.gameObject.SetActive(false);
        UIManager.Ins.UIPanelMenu.gameObject.SetActive(true);
    }
}
