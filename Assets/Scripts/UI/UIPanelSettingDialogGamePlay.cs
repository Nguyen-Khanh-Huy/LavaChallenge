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
        Debug.Log("zzzzzz");
    }
    
    private void HandleBtnHome()
    {
        gameObject.SetActive(false);
    }
}
