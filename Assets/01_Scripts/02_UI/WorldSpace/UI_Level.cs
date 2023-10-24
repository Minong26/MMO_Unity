using TMPro;
using UnityEngine;

public class UI_Level : UI_Base
{
    enum GameObjects
    {
        LevelText
    }
    private Stat _stat;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y) * 1.2f;
        transform.rotation = Camera.main.transform.rotation;

        UpdateLevelText();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void UpdateLevelText()
    {
        GetObject((int)GameObjects.LevelText).GetComponent<TextMeshProUGUI>().text = $"Level <{_stat.Level}>";
    }
}
