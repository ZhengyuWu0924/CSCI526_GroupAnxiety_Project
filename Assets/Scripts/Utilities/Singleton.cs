using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ensure the object is unique and persistent through scenes
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}