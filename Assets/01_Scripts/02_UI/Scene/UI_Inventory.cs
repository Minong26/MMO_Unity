using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("Prefabs/UI/Scene/UI_Inven_Item");
            item.transform.SetParent(gridPanel.transform);
        }
    }

    private void Start()
    {
        Init();
    }
}
