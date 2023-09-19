using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemName_txt
    }

    private string _name;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemName_txt).GetComponent<TextMeshProUGUI>().text = _name;
        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData data) => { Debug.Log($"{_name} has Clicked"); });
    }

    private void Start()
    {
        Init();
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
