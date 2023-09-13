using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path)
    {
        GameObject prefab = Load<GameObject>($"{path}");

        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        else
        {
            return Object.Instantiate(prefab);
        }
    }

    public void Destroy(GameObject go, float t = 0)
    {
        if (go == null)
            return;

        GameObject.Destroy(go, t);
    }
}
