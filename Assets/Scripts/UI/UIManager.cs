using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Player Player;
    public UIPanelMenu UIPanelMenu;
    public UIPanelGamePlay UIPanelGamePlay;

    private void Start()
    {
        Player.gameObject.SetActive(false);
        UIPanelMenu.gameObject.SetActive(true);
        UIPanelGamePlay.gameObject.SetActive(false);
    }
}
