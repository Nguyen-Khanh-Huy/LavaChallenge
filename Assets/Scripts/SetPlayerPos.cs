using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetPlayerPos : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.Ins.Player.gameObject.SetActive(true);
        UIManager.Ins.Player.transform.position = transform.position;
        UIManager.Ins.Player.Model.rotation = Quaternion.Euler(-90f, 0f, -90f);
    }
}
