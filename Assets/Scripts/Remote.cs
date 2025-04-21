using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{
    [SerializeField] private GameObject BGTop22;

    private void OnEnable()
    {
        BGTop22.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player) || other.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            BGTop22.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player) || other.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            BGTop22.SetActive(true);
        }
    }
}
