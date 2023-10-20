using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;
    private GameManager _game = new GameManager();
    private InputManager _input = new InputManager();
    private ResourceManager _resouce = new ResourceManager();
    private UIManager _ui = new UIManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private PoolManager _pool = new PoolManager();
    private DataManager _data = new DataManager();

    static Managers Instance { get { Init(); return s_Instance; } }
    public static GameManager Game { get { return Instance._game; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resouce; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PoolManager Pool { get { return Instance._pool; } }
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
