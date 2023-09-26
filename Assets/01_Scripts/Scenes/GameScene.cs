using System.Collections.Generic;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inventory>();

        Dictionary<int, Stat> dic = Managers.Data.StatDict;
        Stat stat = dic[1];
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
