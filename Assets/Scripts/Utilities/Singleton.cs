using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ReplaceableSingleton overrides the current instance.
/// </summary>
public abstract class ReplaceableSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        Instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// Basic Singleton.
/// </summary>
public abstract class Singleton<T> : ReplaceableSingleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        base.Awake();
    }
}

/// <summary>
/// PersistentSingleton will stay across very levels.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}