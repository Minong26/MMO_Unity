using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    static Managers Instance { get { Init(); return s_Instance; } }

    private InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    private ResourceManager _resouce = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resouce; } }

    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    private SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    private SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }

    private PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    private DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    public static Managers GetInstance()
    {
        Init();
        return Instance;
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
            s_Instance._sound.Init();
            s_Instance._pool.Init();
            s_Instance._data.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
