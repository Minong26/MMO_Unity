using System;
using System.Collections.Generic;
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

    private int _score = 0;

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetText((int)Texts.Score_txt).text = $"Score : {_score}";

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OndragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
    }

    public void OnButtonClicked()
    {
        _score++;
        Debug.Log("Button Clicked");
        Get<TextMeshProUGUI>((int)Texts.Score_txt).text = $"Score : {_score}";
    }
}
