using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
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

        float ratio = _stat.HP / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
