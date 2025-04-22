using System.Collections;
using UnityEngine;

public class SetPlayerPos : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.Ins.Player.transform.position = transform.position;
        UIManager.Ins.Player.Model.rotation = Quaternion.Euler(-90f, 0f, -90f);
    }
}
