using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _ins;
    public static T Ins { get { return _ins; } }
    public virtual void Awake()
    {
        DontDestroy(true);
    }
    public void DontDestroy(bool dontDestroyOnLoad)
    {
        if (_ins == null)
        {
            _ins = this as T;
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}