using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(100f * Time.deltaTime * Vector3.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            LevelManager.Ins.ListLevels[LevelManager.Ins.IdLevel].gameObject.SetActive(false);
            if (LevelManager.Ins.IdLevel < LevelManager.Ins.ListLevels.Count - 1)
            {
                LevelManager.Ins.IdLevel++;
                LevelManager.Ins.ListLevelUnlock[LevelManager.Ins.IdLevel] = true;
            }

            UIManager.Ins.UIPanelMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
