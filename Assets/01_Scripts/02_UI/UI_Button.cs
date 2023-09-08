using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        ScoreUp_btn
    }
    enum Texts
    {
        ScoreUpBtn_txt,
        Score_txt,
    }
    enum Images
    {
        ItemIcon
    }
    enum GameObjects
    {
        TestObject
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        //Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.Score_txt).text = $"Score : {_score}";

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        GetButton((int)Buttons.ScoreUp_btn).gameObject.AddUIEvent(OnButtonClicked);


    }

    private int _score = 0;
    public void OnButtonClicked(PointerEventData pEventData)
    {
        _score++;
        GetText((int)Texts.Score_txt).text = $"Score : {_score}";
    }
}
