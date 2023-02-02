using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some useful functions we may use
/// </summary>
public static class HelperFunctions
{
    public static void DestroyChildren(this Transform parent)
    {
        foreach(Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
