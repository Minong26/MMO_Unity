using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _object = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        ScoreUp_btn
    }
    enum Texts
    {
        ScoreUpBtn_txt,
        Score_txt,
    }

    [SerializeField] private TextMeshProUGUI _text;
    private int _score = 0;

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        _text.text = $"Score : {_score}";
    }

    private void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _object.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    public void OnButtonClicked()
    {
        _score++;
        Debug.Log("Button Clicked");
        _text.text = $"Score : {_score}";
    }
}
