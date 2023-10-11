using System.Collections.Generic;

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
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
