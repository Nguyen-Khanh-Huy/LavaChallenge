using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelGamePlay : MonoBehaviour
{
    public Player Player;
    public Button BtnUL;
    public Button BtnUR;
    public Button BtnDL;
    public Button BtnDR;

    public Button BtnSettingGamePlay;
    public Text TxtCurLevel;
    public Text TxtCurMovies;

    public UIPanelSettingDialogGamePlay UIPanelSettingDialog;

    private void Start()
    {
        BtnUL.onClick.AddListener(() => Player.MoveFromUIButton(Vector3.left, 0f));
        BtnUR.onClick.AddListener(() => Player.MoveFromUIButton(Vector3.forward, 90f));
        BtnDL.onClick.AddListener(() => Player.MoveFromUIButton(Vector3.back, -90f));
        BtnDR.onClick.AddListener(() => Player.MoveFromUIButton(Vector3.right, 180f));
    }
}
