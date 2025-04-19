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
            gameObject.SetActive(false);
        }
    }
}
