using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Prefabs/UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Sound.Play("Sounds/BGM/Crescent-Moon", Define.Sound.Bgm);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Prefabs/Knight");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
