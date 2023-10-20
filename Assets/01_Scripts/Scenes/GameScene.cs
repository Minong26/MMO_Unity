using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inventory>();

        Dictionary<int, Data.Stat> dic = Managers.Data.StatDict;
        Data.Stat stat = dic[1];

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Prefabs/UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.Spawn(Define.WorldObject.Monster, "Prefabs/Knight");
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
