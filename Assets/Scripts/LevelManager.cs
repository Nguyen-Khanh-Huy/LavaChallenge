using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public int IdLevel = 0;
    public List<Transform> ListLevels = new();

    public List<bool> ListLevelUnlock = new();
    public List<int> ListLevelNumble = new();

    public override void Awake()
    {
        base.Awake();
        IdLevel = 0;
        ListLevels.Clear();
        foreach (Transform child in transform)
        {
            if (child.name != "BGDown")
            {
                ListLevels.Add(child);
                ListLevelUnlock.Add(false);
                ListLevelNumble.Add(ListLevels.Count);
            }
        }

        if (ListLevels.Count > 0)
            ListLevelUnlock[0] = true;
    }

    public void OnEnableGem(int idx)
    {
        //ListLevels[idx].transform.Find("Maps/Gem").gameObject.SetActive(true);
        GameObject gem = ListLevels[idx].transform.Find("Maps/Gem").gameObject;
        if (gem.activeSelf) return;
        gem.SetActive(true);
    }

    public void OnOffBGDown(bool onoff)
    {
        //transform.Find("BGDown").gameObject.SetActive(onoff);
        GameObject bgDown = transform.Find("BGDown").gameObject;
        if (bgDown.activeSelf == onoff) return;
        bgDown.SetActive(onoff);
    }
}
