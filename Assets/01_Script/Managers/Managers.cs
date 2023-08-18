using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    static Managers Instance { get { Init(); return s_Instance; } }

    private InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    private ResourceManager _resouce = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resouce; } }

    public static Managers GetInstance()
    {
        Init();
        return Instance;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
        }
    }
}