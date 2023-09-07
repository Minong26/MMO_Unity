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
    enum GameObjects
    {
        TestObject
    }

    private int _score = 0;

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        Get<TextMeshProUGUI>((int)Texts.Score_txt).text = $"Score : {_score}";
        //Get<GameObject>((int)GameObjects.TestObject).transform.position += new Vector3(0, 5, 0);
    }

    private void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _object.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (!_object.TryGetValue(typeof(T), out objects))
            return null;

        return objects[idx] as T;
    }

    public void OnButtonClicked()
    {
        _score++;
        Debug.Log("Button Clicked");
        Get<TextMeshProUGUI>((int)Texts.Score_txt).text = $"Score : {_score}";
    }
}
