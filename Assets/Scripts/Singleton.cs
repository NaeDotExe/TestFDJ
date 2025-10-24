using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this as T;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (_instance != null)
            _instance = null;

    }
}
